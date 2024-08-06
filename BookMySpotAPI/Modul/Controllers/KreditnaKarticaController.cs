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
        public async Task <ActionResult> Get(int id)
        {
            return Ok(await _dbContext.KreditneKartice.FirstOrDefaultAsync(x => x.KarticaID == id));
        }

        [HttpGet("korisnikId")]
        public async Task <ActionResult> GetByKorisnik(int korisnikId)
        {
            var data = await _dbContext.KreditneKartice.Include(k=>k.KorisnikID).FirstOrDefaultAsync(x=>x.KorisnikID == korisnikId);
            return Ok(data);
        }

        [HttpPost]
        public async Task <ActionResult> Add([FromBody] KreditnaKarticaAddVM x)
        {
            var newKreditnaKartica = new KreditnaKartica
            {
                KorisnikID = x.KorisnikID,
                BrojKartice = x.BrojKartice,
                DatumIsteka = x.DatumIsteka,
                SigurnosniBroj = x.SigurnosniBroj
            };
            await _dbContext.KreditneKartice.AddAsync(newKreditnaKartica);
            await _dbContext.SaveChangesAsync();
            return await Get(newKreditnaKartica.KorisnikID);
        }
    }
}
