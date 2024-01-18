using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KategorijaController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public KategorijaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Get(int id) 
        { 
            return Ok(_dbContext.Kategorije.FirstOrDefault(x=>x.KategorijaID==id));
        }

        [HttpPost]
        public ActionResult Add([FromBody] KategorijaAddVM x)
        {
            var newKategorija = new Kategorija
            {
                Naziv = x.Naziv
            };
            _dbContext.Add(newKategorija);
            _dbContext.SaveChanges();
            return Get(newKategorija.KategorijaID);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var data = _dbContext.Kategorije.FirstOrDefault(x=>x.KategorijaID==id);
            _dbContext.Remove(data);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
