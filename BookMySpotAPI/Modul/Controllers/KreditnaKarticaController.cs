using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KreditnaKarticaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public KreditnaKarticaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("id")]
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.KreditneKartice.FirstOrDefault(x => x.KarticaID == id));
        }

        [HttpGet("korisnikId")]
        public ActionResult GetByKorisnik(int korisnikId)
        {
            var data = _dbContext.KreditneKartice.Include(k=>k.KorisnikID).FirstOrDefault(x=>x.KorisnikID == korisnikId);
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Add([FromBody] KreditnaKarticaAddVM x)
        {
            var newKreditnaKartica = new KreditnaKartica
            {
                KorisnikID = x.KorisnikID,
                BrojKartice = x.BrojKartice,
                DatumIsteka = x.DatumIsteka,
                SigurnosniBroj = x.SigurnosniBroj
            };
            _dbContext.Add(newKreditnaKartica);
            _dbContext.SaveChanges();
            return Get(newKreditnaKartica.KorisnikID);
        }
    }
}
