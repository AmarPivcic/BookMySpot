using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using BookMySpotAPI.Helper;
using Microsoft.AspNetCore.Authorization;


namespace BookMySpotAPI.Modul.Controllers

{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KategorijaController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KategorijaController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task <ActionResult> Get(int id) 
        { 
            return Ok(await _dbContext.Kategorije.FirstOrDefaultAsync(x=>x.KategorijaID==id));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] KategorijaAddVM x)
        {
            var slike = new Slike(_webHostEnvironment);
            var slikaKategorije = slike.dodajSliku(x.Slika);
            var newKategorija = new Kategorija
            {
                Naziv = x.Naziv,
                Slika = slikaKategorije,
            };
            await _dbContext.Kategorije.AddAsync(newKategorija);
            await _dbContext.SaveChangesAsync();
            return Ok(newKategorija);
        }

        [HttpDelete]
        public async Task <ActionResult> Delete(int id)
        {
            var data = await _dbContext.Kategorije.FirstOrDefaultAsync(x=>x.KategorijaID==id);
            if (data == null)
            {
                return NotFound();
            }
            _dbContext.Kategorije.Remove(data);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task <ActionResult<List<Kategorija>>> GetListaKategorija()
        {
            var listaKategorija = await _dbContext.Kategorije.ToListAsync();
            return Ok(listaKategorija);
        }
    }
}
