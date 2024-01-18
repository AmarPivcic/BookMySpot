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
        public ActionResult Get(int id)
        {
            return Ok(_dbContext.Gradovi.FirstOrDefault(x => x.GradID==id));
        }

        [HttpPost]
        public ActionResult Add([FromBody] GradAddVM x)
        {
            var newGrad = new Grad
            {
                Naziv = x.Naziv
            };
            _dbContext.Add(newGrad);
            _dbContext.SaveChanges();
            return Get(newGrad.GradID);
        }

    }
}
