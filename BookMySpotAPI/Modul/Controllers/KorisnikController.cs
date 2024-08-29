using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using BookMySpotAPI.Helper;
using Microsoft.AspNetCore.Identity;
using static BookMySpotAPI.Autentifikacija.Helper.MyAuthTokenExtension;
using BookMySpotAPI.Autentifikacija.Controllers;
using BookMySpotAPI.Autentifikacija.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EmailService emailService;
        private readonly PasswordHasher _passwordHasher;

        public KorisnikController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment, EmailService emailService)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            this.emailService = emailService;
            _passwordHasher = new PasswordHasher();
        }

        [HttpGet]
        public async Task <ActionResult> Get(int id)
        {
            var korisnikIzBaze = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(x => x.osobaID == id);

            if(korisnikIzBaze != null)
            {
                var korisnikResponse = new KorisnikInformacijeResponseVM
                {
                    korisnickoIme = korisnikIzBaze.korisnickoIme,
                    email = korisnikIzBaze.email,
                    ime = korisnikIzBaze.ime,
                    prezime = korisnikIzBaze.prezime,
                    slika = korisnikIzBaze.slika,
                    telefon = korisnikIzBaze.telefon
                };

                return Ok(korisnikResponse);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetKorisnickeRacune(int pageNumber = 1, int pageSize = 10, bool obrisan = false, bool suspendovan = false)
        {
            var totalRecords = await _dbContext.KorisnickiNalog
                .CountAsync(k => k.obrisan == obrisan && k.suspendovan == suspendovan);

            var korisniciIzBaze = await _dbContext.KorisnickiNalog
                .Where(k => k.obrisan == obrisan && k.suspendovan == suspendovan)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var listaKorisnikaResponse = new List<PostojeciRacuniResponseVM>();

            foreach (var korisnik in korisniciIzBaze)
            {
                listaKorisnikaResponse.Add(new PostojeciRacuniResponseVM
                {
                    osobaID = korisnik.osobaID,
                    ime = korisnik.ime,
                    prezime = korisnik.prezime,
                    slika = korisnik.slika,
                    korisnickoIme = korisnik.korisnickoIme,
                    email = korisnik.email,
                    telefon = korisnik.telefon,
                    isKorisnik = korisnik.isKorisnik,
                    isAdministrator = korisnik.isAdministrator,
                    isManager = korisnik.isManager
                });
            }

            var response = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = listaKorisnikaResponse
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> EditKorisnickiRacun([FromRoute] int id, [FromForm] KorisnikInformacijeRequestVM request)
        {
            var korisnikIzBaze = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(x => x.osobaID == id);

            if (korisnikIzBaze == null)
            {
                return NotFound();
            }

            // Provjera korisničkog imena
            if (!string.IsNullOrEmpty(request.korisnickoIme))
            {
                var korisnikSaIstimKorisnickimImenom = await _dbContext.KorisnickiNalog
                    .FirstOrDefaultAsync(x => x.korisnickoIme == request.korisnickoIme && x.osobaID != id);

                if (korisnikSaIstimKorisnickimImenom != null)
                {
                    return Conflict("Korisničko ime već postoji.");
                }

                korisnikIzBaze.korisnickoIme = request.korisnickoIme;
            }

            // Ažuriranje drugih podataka
            if (request.slika != null)
            {
                var slike = new Slike(_webHostEnvironment);
                var slikaKorisnika = slike.dodajSliku(request.slika);
                korisnikIzBaze.slika = slikaKorisnika;
            }

            korisnikIzBaze.ime = request.ime;
            korisnikIzBaze.prezime = request.prezime;
            korisnikIzBaze.email = request.email;
            korisnikIzBaze.telefon = request.telefon;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditLozinkuZaKorisnickiNalog([FromBody] StaraNovaLozinkaRequestVM zahtjev)
        {
            var logiranaOsoba = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.korisnickoIme != null && k.korisnickoIme == zahtjev.korisnickoIme);

            if (logiranaOsoba == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            if (_passwordHasher.VerifyHashedPassword(logiranaOsoba, logiranaOsoba.lozinka, zahtjev.staraLozinka) != PasswordVerificationResult.Success)
            {
                return BadRequest("Stara lozinka nije ispravna.");
            }

            var hashedPassword = _passwordHasher.HashPassword(logiranaOsoba, zahtjev.novaLozinka);
            logiranaOsoba.lozinka = hashedPassword;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> ObrisiKorisnickiRacun([FromBody] ObrisiLozinkuRequestVM zahtjev)
        {
            var logiranaOsoba = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.korisnickoIme != null && k.korisnickoIme == zahtjev.korisnickoIme);

            if (logiranaOsoba == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            if (_passwordHasher.VerifyHashedPassword(logiranaOsoba, logiranaOsoba.lozinka, zahtjev.lozinka) != PasswordVerificationResult.Success)
            {
                return BadRequest("Lozinka nije ispravna.");
            }

            logiranaOsoba.obrisan = true;
            await _dbContext.SaveChangesAsync();

            var email = new EmailTemplates(emailService);
            await email.ObrisiRacunMail(logiranaOsoba);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> ObrisiKorisnickiRacunAdmin([FromRoute] int id)
        {
            var korisnik = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.osobaID == id);

            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            korisnik.obrisan = true;
            await _dbContext.SaveChangesAsync();

            var mail = new EmailTemplates(emailService);
            await mail.AdminBrisanjeKorisnickogNaloga(korisnik);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> SuspendujKorisnickiRacun([FromRoute] int id, [FromQuery] int brojDana, [FromQuery] string razlogSuspenzije)
        {
            var korisnik = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.osobaID == id);

            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            korisnik.suspendovan = true;
            korisnik.datumSuspenzijeDo = DateTime.Now.AddDays(brojDana); 
            korisnik.razlogSuspenzije = razlogSuspenzije; 
            await _dbContext.SaveChangesAsync();

            var mail = new EmailTemplates(emailService);
            await mail.SuspendovanjeRacunaMail(korisnik);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> AktivirajObrisanRacun([FromRoute] int id)
        {
            var korisnik = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.osobaID == id);

            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            korisnik.obrisan = false;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> AktivirajSuspendovanRacun([FromRoute] int id)
        {
            var korisnik = await _dbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k => k.osobaID == id);

            if (korisnik == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            korisnik.suspendovan = false;
            korisnik.datumSuspenzijeDo = null;
            korisnik.razlogSuspenzije = null;
            await _dbContext.SaveChangesAsync();

            var mail = new EmailTemplates(emailService);
            await mail.AktiviranSuspendovanRacunMail(korisnik);

            return Ok();
        }

        [HttpPost]
        public async Task <ActionResult> PromijeniIme([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if(osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.ime = x.novoIme;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async Task <ActionResult> PromijenPrezime([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.prezime = x.novoPrezime;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async Task <ActionResult> PromijeniEmail([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.email = x.noviEmail;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async Task<ActionResult> PromijeniTelefon([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba =await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.telefon = x.noviTelefon;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]
        public async  Task<ActionResult> PromijeniKorisnickoIme([FromBody] KorisnickiNalogEditVM x)
        {
            KorisnickiNalog osoba = await _dbContext.KorisnickiNalog.FirstOrDefaultAsync(k => k.osobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.korisnickoIme = x.novoKorisnickoIme;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
