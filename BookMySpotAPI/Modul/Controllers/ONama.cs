using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ONama : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ONama(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> DobaviSadrzaj()
        {
            var sadrzaj = await dbContext.SadrzajiONama
                                        .OrderByDescending(s => s.Id)
                                        .FirstOrDefaultAsync();

            if (sadrzaj == null)
            {
                return NotFound();
            }
            else
            {
                var zaKorisnika = new ONamaSadrzajVM
                {
                    Tekst = sadrzaj.Tekst
                };
                return Ok(zaKorisnika);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostaviSadrzaj([FromBody] ONamaSadrzajVM request)
        {
            if(request == null || string.IsNullOrWhiteSpace(request.Tekst))
            {
                return BadRequest("Unos nije validan, tekst je obavezan!");
            }
            var sadrzaj = new ONamaSadrzaj
            {
                Tekst = request.Tekst
            };
            dbContext.SadrzajiONama.Add(sadrzaj);
            await dbContext.SaveChangesAsync();

            return Ok(sadrzaj);
        }
    }
}
