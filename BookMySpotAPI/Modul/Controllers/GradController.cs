using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using BookMySpotAPI.Modul.ViewModels;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GradController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public GradController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("id")]
        public async Task <ActionResult> Get(int id)
        {
            return Ok(await _dbContext.Gradovi.FirstOrDefaultAsync(x => x.GradID==id));
        }

        [HttpPost]
        public async Task <ActionResult> Add([FromBody] GradAddVM x)
        {
            var newGrad = new Grad
            {
                Naziv = x.Naziv
            };
            await _dbContext.Gradovi.AddAsync(newGrad);
            await _dbContext.SaveChangesAsync();
            return await Get(newGrad.GradID);
        }

    }
}
