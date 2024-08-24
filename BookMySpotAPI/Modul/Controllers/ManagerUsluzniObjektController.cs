using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ManagerUsluzniObjektController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ManagerUsluzniObjektController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> ProvjeriManagera([FromBody] ProvjeriManageraIObjekatVM request)
        {
            if (request == null)
            {
                return BadRequest("Neispravan zahtjev.");
            }

            var izBaze = dbContext.ManagerUsluzniObjekti
                .Where(u => u.manager.osobaID == request.osobaID && u.usluzniObjektID == request.usluzniObjektID)
                .ToList();

            if (izBaze.Any())
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
