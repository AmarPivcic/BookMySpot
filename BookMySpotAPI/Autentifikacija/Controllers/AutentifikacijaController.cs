﻿using BookMySpotAPI.Autentifikacija.Helper;
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

namespace BookMySpotAPI.Autentifikacija.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;
        public AutentifikacijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher();
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
        public ActionResult<LoginInformacije> Registracija([FromBody] RegistracijaVM x)
        {
            var newKorisnickiNalog = new Korisnik
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                Email = x.Email,
                korisnickoIme = x.korisnickoIme,
                Slika = null
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
