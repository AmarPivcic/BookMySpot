using BookMySpotAPI.Autentifikacija.Helper;
using BookMySpotAPI.Autentifikacija.Models;
using BookMySpotAPI.Autentifikacija.ViewModels;
using BookMySpotAPI.Data;
using BookMySpotAPI.Helper;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BookMySpotAPI.Autentifikacija.Helper.MyAuthTokenExtension;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Autentifikacija.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;
        private readonly EmailService _emailService;
        public AutentifikacijaController(ApplicationDbContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher();
            _emailService = emailService;
        }

        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] LoginVM x)
        {
            KorisnickiNalog logiranaOsoba = _dbContext.KorisnickiNalog
            .FirstOrDefault(k => k.korisnickoIme != null && k.korisnickoIme == x.korisnickoIme);

            if (logiranaOsoba == null ||
                _passwordHasher.VerifyHashedPassword(logiranaOsoba, logiranaOsoba.lozinka, x.lozinka) != PasswordVerificationResult.Success)
            {
                return new LoginInformacije(null);
            }

            if(logiranaOsoba.obrisan)
            {
                return StatusCode(410, "Korisnički nalog je obrisan.");
            }

            if (logiranaOsoba.suspendovan)
            {
                if(logiranaOsoba.datumSuspenzijeDo > DateTime.Now)
                {
                    return StatusCode(403, $"Korisnički nalog je suspendovan do dana {logiranaOsoba.datumSuspenzijeDo:dd.MM.yyyy}.\nRazlog suspenzije: {logiranaOsoba.razlogSuspenzije}");
                }
                logiranaOsoba.suspendovan = false;
                logiranaOsoba.datumSuspenzijeDo = null;
                logiranaOsoba.razlogSuspenzije = null;
                _dbContext.SaveChanges();
            }


            string randomString = TokenGenerator.Generate(10);

            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                KorisnickiNalog = logiranaOsoba,
                vrijemeEvidentiranja = DateTime.Now
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            return new LoginInformacije(noviToken);

        }

        [HttpPost]
        public async Task<ActionResult<LoginInformacije>> Registracija([FromBody] RegistracijaVM x)
        {
            var korisnickoime = await _dbContext.KorisnickiNalog.AnyAsync(k => k.korisnickoIme == x.korisnickoIme);
            
            if(korisnickoime)
            {
                return Conflict("Korisničko ime već postoji!");
            }

            var email = await _dbContext.KorisnickiNalog.AnyAsync(k => k.email == x.email);

            if (email)
            {
                return Conflict("Već postoji korisnički nalog sa ovim Emailom!");
            }

            var newKorisnickiNalog = new Korisnik
            {
                ime = x.ime,
                prezime = x.prezime,
                email = x.email,
                korisnickoIme = x.korisnickoIme,
                slika = null
            };

            var hashedPassword = _passwordHasher.HashPassword(newKorisnickiNalog, x.lozinka);
            newKorisnickiNalog.lozinka = hashedPassword;

            _dbContext.Add(newKorisnickiNalog);
            _dbContext.SaveChanges();

            var login = new LoginVM
            {
                korisnickoIme = x.korisnickoIme,
                lozinka = x.lozinka
            };

            var mail = new EmailTemplates(_emailService);
            await mail.RegistracijaMail(newKorisnickiNalog);

            return Login(login);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            if(autentifikacijaToken == null)
            {
                return Ok();
            }

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}
