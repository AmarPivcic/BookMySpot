using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OsobaController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public OsobaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Osobe.FirstOrDefault(x=> x.OsobaID == id));
        }

        [HttpPost]
        public ActionResult Add([FromBody] OsobaAddVM x)
        {
            var newOsoba = new Osoba
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                Email = x.Email,
                Telefon = x.Telefon,
                Adresa = x.Adresa,
                Slika = null
            };
            _dbContext.Add(newOsoba);
            _dbContext.SaveChanges();
            return Get(newOsoba.OsobaID);
        }

        [HttpPost]
        public ActionResult PromijeniIme([FromBody] OsobaEditVM x)
        {
            Osoba osoba = _dbContext.Osobe.FirstOrDefault(k => k.OsobaID == x.OsobaID);

            if(osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.Ime = x.NovoIme;
                _dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpPost]
        public ActionResult PromijenPrezime([FromBody] OsobaEditVM x)
        {
            Osoba osoba = _dbContext.Osobe.FirstOrDefault(k => k.OsobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.Prezime = x.NovoPrezime;
                _dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpPost]
        public ActionResult PromijeniEmail([FromBody] OsobaEditVM x)
        {
            Osoba osoba = _dbContext.Osobe.FirstOrDefault(k => k.OsobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.Email = x.NoviEmail;
                _dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpPost]
        public ActionResult PromijeniTelefon([FromBody] OsobaEditVM x)
        {
            Osoba osoba = _dbContext.Osobe.FirstOrDefault(k => k.OsobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.Telefon = x.NoviTelefon;
                _dbContext.SaveChanges();
                return Ok();
            }
        }

        [HttpPost]
        public ActionResult PromijeniAdresu([FromBody] OsobaEditVM x)
        {
            Osoba osoba = _dbContext.Osobe.FirstOrDefault(k => k.OsobaID == x.OsobaID);

            if (osoba == null)
            {
                return BadRequest("Pogrešan ID");
            }
            else
            {
                osoba.Adresa = x.NovaAdresa;
                _dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
