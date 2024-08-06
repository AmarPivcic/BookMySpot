using Azure.Core;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookMySpotAPI.Helper
{
    public class Slike
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Slike(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public string dodajSliku(IFormFile slika)
        {
           
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Slike", $"{slika.FileName}");
            var filePath = $"https://localhost:7058/Slike/{slika.FileName}";

            using var stream = new FileStream(localFilePath, FileMode.Create);
            slika.CopyTo(stream);
            return (filePath);
        }
    }
}
