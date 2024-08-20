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
        public async Task <ActionResult<Kategorija>> Get(int id) 
        { 
            var kategorija = await _dbContext.Kategorije.FindAsync(id);
            if (kategorija == null)
                return NotFound("Kategorija nije pronađena!");

            return Ok(kategorija);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] KategorijaAddVM x)
        {
            var slike = new Slike(_webHostEnvironment);
            var slikaKategorije = slike.dodajSliku(x.slika);
            var newKategorija = new Kategorija
            {
                naziv = x.naziv,
                slika = slikaKategorije,
            };
            await _dbContext.Kategorije.AddAsync(newKategorija);
            await _dbContext.SaveChangesAsync();
            return Ok(newKategorija);
        }

        [HttpDelete]
        public async Task <ActionResult> Delete(int id)
        {
            var data = await _dbContext.Kategorije.FirstOrDefaultAsync(x=>x.kategorijaID==id);
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
            var listaKategorija = await _dbContext.Kategorije.Select(k => new Kategorija
            {
                kategorijaID = k.kategorijaID,
                naziv = k.naziv,
                slika = k.slika,
                brojOpcija = _dbContext.UsluzniObjekti.Count(u => u.kategorijaID == k.kategorijaID)
            }).ToListAsync();
            return Ok(listaKategorija);
        }
    }
}
