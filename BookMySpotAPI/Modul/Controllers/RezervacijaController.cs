using BookMySpotAPI.Data;
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
        public RezervacijaController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetListaSlobodnihTermina(int usluzniObjektID, DateTime odabraniDatum, int trajanje)
        {
            var trenutneRezervacije = await _dbContext.Rezervacije.Where(r => r.usluzniObjektID == usluzniObjektID && r.datumRezervacije == odabraniDatum).ToListAsync();
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
                usluzniObjektID = x.usluzniObjektID
            };

            korisnik.brojRezervacija++;
            _dbContext.Korisnici.Update(korisnik);
            await _dbContext.Rezervacije.AddAsync(novaRezervacija);
            await _dbContext.SaveChangesAsync();

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
                usluzniObjektID = request.usluzniObjektID
            };
            korisnik.brojRezervacija++;
            _dbContext.Korisnici.Update(korisnik);
            await _dbContext.Rezervacije.AddAsync(novaRezervacija);
            await _dbContext.SaveChangesAsync();
            return Ok(novaRezervacija);
        }

        [HttpGet]
        public async Task<ActionResult<DateTime?>> VratiNajudaljenijiDatum(int uslugaId)
        {
            var rezervacijeSaUslugaId = await _dbContext.Rezervacije
                .Where(r => r.uslugaID == uslugaId)
                .ToListAsync();

            var najdaljiDatumRezervacijaKraj = rezervacijeSaUslugaId
                .Select(r => DateTime.TryParse(r.rezervacijaKraj, out DateTime datumKraj) ? datumKraj : (DateTime?)null)
                .Where(datumKraj => datumKraj > DateTime.Now)
                .Max();

            return Ok(najdaljiDatumRezervacijaKraj);
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
                .Include(r => r.usluzniObjekt).Include(r => r.usluga).OrderBy(r => r.datumRezervacije).ThenBy(r => r.rezervacijaPocetak).ToListAsync();
            return Ok(listaRezervacija);
        }

        [HttpGet]
        public async Task<ActionResult<List<Rezervacija>>> GetListaPrethodnihKorisnik (int id)
        {
            var korisnik = await _dbContext.Korisnici.FindAsync(id);
            if (korisnik == null)
                return NotFound("Korisnik nije pronađen!");

            var listaRezervacija = await _dbContext.Rezervacije.Where(r => r.korisnikID == korisnik.osobaID && (r.otkazano==true || r.zavrseno==true))
                .Include(r => r.usluzniObjekt).Include(r => r.usluga).OrderByDescending(r => r.datumRezervacije).ThenByDescending(r => r.rezervacijaPocetak).ToListAsync();
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

            return Ok(rezervacija);
        }
    }
}
