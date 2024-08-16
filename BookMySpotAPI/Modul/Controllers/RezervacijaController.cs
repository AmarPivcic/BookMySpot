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
            Korisnik korisnik = _dbContext.Korisnici.FirstOrDefault(k => k.osobaID == x.korisnikID);

            var novaRezervacija = new Rezervacija
            {
                datumRezervacije = x.datumRezervacije,
                rezervacijaPocetak = x.rezervacijaPocetak,
                rezervacijaKraj = rezervacijaKrajCalc,
                korisnikID = x.korisnikID,
                uslugaID = x.uslugaID,
                usluzniObjektID = x.usluzniObjektID
            };

            korisnik.brojRezervacija++;
            _dbContext.Korisnici.Update(korisnik);
            await _dbContext.Rezervacije.AddAsync(novaRezervacija);
            await _dbContext.SaveChangesAsync();

            return Ok(novaRezervacija);
        }
    }
}
