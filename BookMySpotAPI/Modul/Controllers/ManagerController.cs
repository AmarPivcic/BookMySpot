using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Manager>>> GetListaManager(int usluzniObjektID)
        {
            var usluzniObjekt = _dbContext.UsluzniObjekti.Find(usluzniObjektID);

            if (usluzniObjekt == null)
                return NotFound();

            var listaManager = await _dbContext.ManagerUsluzniObjekti.Where(m => m.usluzniObjektID == usluzniObjekt.usluzniObjektID)
                .Select(m => m.manager)
                .ToListAsync();

            return Ok(listaManager);
        }
    }
}
