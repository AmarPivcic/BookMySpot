using Microsoft.EntityFrameworkCore;
using BookMySpotAPI.Modul.Models;

namespace BookMySpotAPI.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Osoba> Osobe { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
}