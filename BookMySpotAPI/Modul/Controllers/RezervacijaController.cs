using BookMySpotAPI.Data;
using BookMySpotAPI.Helper;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly EmailService _emailService;

        public RezervacijaController(ApplicationDbContext dbContext, EmailService emailService) 
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult> GetListaSlobodnihTermina(int usluzniObjektID, DateTime odabraniDatum, int managerID, int trajanje)
        {
            var trenutneRezervacije = await _dbContext.Rezervacije.Where(r => r.usluzniObjektID == usluzniObjektID && r.datumRezervacije == odabraniDatum
            && r.managerID == managerID && r.otkazano == false && r.zavrseno == false).ToListAsync();
            UsluzniObjekt usluzniObjekt = await _dbContext.UsluzniObjekti.FirstOrDefaultAsync(u => u.usluzniObjektID == usluzniObjektID);
            TimeSpan radnoVrijemePocetak = TimeSpan.Parse(usluzniObjekt.radnoVrijemePocetak);
            TimeSpan radnoVrijemeKraj = TimeSpan.Parse(usluzniObjekt.radnoVrijemeKraj);
            TimeSpan slotInterval = new TimeSpan(0, 15, 0);
            TimeSpan trajanjeUsluge = TimeSpan.FromMinutes(trajanje);
            TimeSpan trenutnoVrijeme = DateTime.Now.TimeOfDay;

            if (usluzniObjekt == null)
                return NotFound("Uslužni objekt nije pronađen!");

            var dostupniTermini = new List<string>();
            TimeSpan trenutniTimeSlot;

            if(odabraniDatum != DateTime.Today)
            {
                trenutniTimeSlot = radnoVrijemePocetak;
            }

            else
            {
                if(trenutnoVrijeme.Minutes >= 0 && trenutnoVrijeme.Minutes < 15)
                {
                    trenutniTimeSlot = new TimeSpan(trenutnoVrijeme.Hours, 15, 0);
                }
                else if (trenutnoVrijeme.Minutes >= 15 && trenutnoVrijeme.Minutes < 30)
                {
                    trenutniTimeSlot = new TimeSpan(trenutnoVrijeme.Hours, 30, 0);
                }
                else if (trenutnoVrijeme.Minutes >= 30 && trenutnoVrijeme.Minutes < 45)
                {
                    trenutniTimeSlot = new TimeSpan(trenutnoVrijeme.Hours, 45, 0);
                }
                else
                {
                    trenutniTimeSlot = new TimeSpan(trenutnoVrijeme.Hours + 1, 0, 0);
                }
            }

            while(trenutniTimeSlot.Add(trajanjeUsluge) <= radnoVrijemeKraj)
            {
                var potencijalnoVrijemeKraja = trenutniTimeSlot.Add(trajanjeUsluge);

                bool isTimeSlotConflicting = trenutneRezervacije.Any(r =>
                        (TimeSpan.Parse(r.rezervacijaPocetak) < potencijalnoVrijemeKraja) && (TimeSpan.Parse(r.rezervacijaKraj) > trenutniTimeSlot)
                    );

                if (!isTimeSlotConflicting)
                {
                    dostupniTermini.Add(trenutniTimeSlot.ToString(@"hh\:mm"));
                }

                trenutniTimeSlot = trenutniTimeSlot.Add(slotInterval);
            }
            return Ok(dostupniTermini);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RezervacijaAddVM x)
        {
            string rezervacijaKrajCalc = TimeSpan.Parse(x.rezervacijaPocetak).Add(TimeSpan.FromMinutes(x.trajanje)).ToString(@"hh\:mm");
            Korisnik korisnik = _dbContext.Korisnici.FirstOrDefault(k => k.osobaID == x.osobaID);

            var novaRezervacija = new Rezervacija
            {
                datumRezervacije = x.datumRezervacije,
                rezervacijaPocetak = x.rezervacijaPocetak,
                rezervacijaKraj = rezervacijaKrajCalc,
                korisnikID = x.osobaID,
                uslugaID = x.uslugaID,
                usluzniObjektID = x.usluzniObjektID,
                managerID = x.managerID,
            };

            korisnik.brojRezervacija++;
            _dbContext.Korisnici.Update(korisnik);
            await _dbContext.Rezervacije.AddAsync(novaRezervacija);
            await _dbContext.SaveChangesAsync();

            var loadedRezervacija = await _dbContext.Rezervacije
                .Include(r => r.korisnik)
                .Include(r => r.usluga)
                .Include(r => r.usluzniObjekt)
                .Include(r => r.manager)
                .FirstOrDefaultAsync(r => r.rezervacijaID == novaRezervacija.rezervacijaID);

            var email = new EmailTemplates(_emailService);
            await email.RezervacijaMail(loadedRezervacija);

            return Ok(novaRezervacija);
        }

        [HttpPost]
        public async Task<ActionResult> RezervisiSmjestaj([FromBody] RezervacijaSmjestajRequestVM request)
        {
            Korisnik korisnik = _dbContext.Korisnici.FirstOrDefault(k => k.osobaID == request.osobaID);

            var novaRezervacija = new Rezervacija
            {
                datumRezervacije = DateTime.Now,
                rezervacijaPocetak = request.rezervacijaPocetak,
                rezervacijaKraj = request.rezervacijaKraj,
                korisnikID = request.osobaID,
                uslugaID = request.uslugaID,
                usluzniObjektID = request.usluzniObjektID,
                karticnoPlacanje = request.karticnoPlacanje,
                managerID = request.managerID
            };
            korisnik.brojRezervacija++;
            _dbContext.Korisnici.Update(korisnik);
            await _dbContext.Rezervacije.AddAsync(novaRezervacija);
            await _dbContext.SaveChangesAsync();

            var loadedRezervacija = await _dbContext.Rezervacije
                .Include(r => r.korisnik)
                .Include(r => r.usluga)
                .Include(r => r.usluzniObjekt)
                .Include(r => r.manager)
                .FirstOrDefaultAsync(r => r.rezervacijaID == novaRezervacija.rezervacijaID);

            var email = new EmailTemplates(_emailService);
            await email.RezervacijaSmjestajMail(loadedRezervacija);

            return Ok(novaRezervacija);
        }

        [HttpGet]
        public async Task<ActionResult<DateTime?>> VratiNajudaljenijiDatum(int uslugaId)
        {
            var rezervacijeSaUslugaId = await _dbContext.Rezervacije
                .Where(r => r.uslugaID == uslugaId && r.otkazano == false)
                .ToListAsync();

            var najdaljiDatumRezervacijaKraj = rezervacijeSaUslugaId
                .Select(r => DateTime.TryParse(r.rezervacijaKraj, out DateTime datumKraj) ? datumKraj : (DateTime?)null)
                .Where(datumKraj => datumKraj > DateTime.Now)
                .Max();

            return Ok(najdaljiDatumRezervacijaKraj);
        }

        [HttpGet]
        public IActionResult GetGodine()
        {
            var trenutnaGodina = DateTime.Now.Year;
            var iducaGodina = trenutnaGodina + 1;

            var godine = new List<int>
            {
                trenutnaGodina,
                iducaGodina
            };

            return Ok(godine);
        }

        [HttpGet]
        public IActionResult GetMjeseci(int godina)
        {
            var trenutnaGodina = DateTime.Now.Year;
            var mjeseci = new List<int>();

            if (godina == trenutnaGodina)
            {
                mjeseci.AddRange(new[] { 8, 9, 10, 11, 12 });
            }
            else
            {
                mjeseci.AddRange(Enumerable.Range(1, 12));
            }

            return Ok(mjeseci);
        }

        [HttpGet]
        public IActionResult GetDani(int godina, int mjesec, int uslugaId)
        {
            if (mjesec < 1 || mjesec > 12)
            {
                return BadRequest("Nevalidan mjesec");
            }

            var trenutniDatum = DateTime.Now;
            var brojDana = DateTime.DaysInMonth(godina, mjesec);
            var dani = Enumerable.Range(1, brojDana).ToList();

            if (godina == trenutniDatum.Year && mjesec == trenutniDatum.Month)
            {
                dani = dani.Where(dan => dan >= trenutniDatum.Day).ToList();
            }

            var rezervacije = _dbContext.Rezervacije
                .Where(r => r.uslugaID == uslugaId && !r.otkazano && !r.zavrseno)
                .ToList();

            var zauzetiDani = new HashSet<int>();

            foreach (var rezervacija in rezervacije)
            {
                var pocetak = DateTime.Parse(rezervacija.rezervacijaPocetak);
                var kraj = DateTime.Parse(rezervacija.rezervacijaKraj);

                if (pocetak.Year == godina || kraj.Year == godina || (pocetak.Year < godina && kraj.Year > godina))
                {
                    var pocetakMeseca = new DateTime(godina, mjesec, 1);
                    var krajMeseca = pocetakMeseca.AddMonths(1).AddDays(-1);

                    var rezervacijaPocetak = pocetak > pocetakMeseca ? pocetak : pocetakMeseca;
                    var rezervacijaKraj = kraj < krajMeseca ? kraj : krajMeseca;

                    if (rezervacijaPocetak <= rezervacijaKraj)
                    {
                        var daniRezervacije = Enumerable.Range(1, brojDana)
                            .Where(dan => new DateTime(godina, mjesec, dan) >= rezervacijaPocetak && new DateTime(godina, mjesec, dan) <= rezervacijaKraj)
                            .ToList();

                        foreach (var dan in daniRezervacije)
                        {
                            zauzetiDani.Add(dan);
                        }
                    }
                }
            }

            dani = dani.Where(dan => !zauzetiDani.Contains(dan)).ToList();

            return Ok(dani);
        }

        [HttpGet]
        public async Task<ActionResult<List<Rezervacija>>> GetListaTrenutnihKorisnik (int id)
        {
            var korisnik = await _dbContext.Korisnici.FindAsync(id);
            if (korisnik == null)
                return NotFound("Korisnik nije pronađen!");

            var listaRezervacijaRefresh = await _dbContext.Rezervacije.Where(r => r.korisnikID == korisnik.osobaID).ToListAsync();

            foreach (var rezervacija in listaRezervacijaRefresh)
            {
                if (rezervacija.datumRezervacije <= DateTime.Today && TimeSpan.Parse(rezervacija.rezervacijaPocetak) <= DateTime.Now.TimeOfDay && rezervacija.otkazano == false)
                    { 
                        rezervacija.zavrseno = true;
                        _dbContext.Rezervacije.Update(rezervacija);
                        _dbContext.SaveChanges();
                    }
            }

            var listaRezervacija = await _dbContext.Rezervacije.Where( r => r.korisnikID == korisnik.osobaID && r.otkazano==false && r.zavrseno==false)
                .Include(r => r.usluzniObjekt).Include(r => r.usluga).Include(r => r.manager).OrderBy(r => r.datumRezervacije).ThenBy(r => r.rezervacijaPocetak).ToListAsync();
            return Ok(listaRezervacija);
        }

        [HttpGet]
        public async Task<ActionResult<List<Rezervacija>>> GetListaPrethodnihKorisnik (int id)
        {
            var korisnik = await _dbContext.Korisnici.FindAsync(id);
            if (korisnik == null)
                return NotFound("Korisnik nije pronađen!");

            var listaRezervacija = await _dbContext.Rezervacije.Where(r => r.korisnikID == korisnik.osobaID && (r.otkazano==true || r.zavrseno==true))
                .Include(r => r.usluzniObjekt).Include(r => r.usluga).Include(r => r.manager).OrderByDescending(r => r.datumRezervacije).ThenByDescending(r => r.rezervacijaPocetak).ToListAsync();
            return Ok(listaRezervacija);
        }

        [HttpPut]
        public async Task<ActionResult<Rezervacija>> OtkaziRezervaciju(int id)
        {
            var rezervacija = await _dbContext.Rezervacije.FindAsync(id);

            if (rezervacija == null)
            {
                return NotFound("Ova rezervacija ne postoji!");
            }

            rezervacija.otkazano=true;
            _dbContext.Update(rezervacija);
            await _dbContext.SaveChangesAsync();

            var email = new EmailTemplates(_emailService);
            await email.OtkaziRezervacijaMail(rezervacija);

            return Ok(rezervacija);
        }
    }
}
