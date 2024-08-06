using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using BookMySpotAPI.Autentifikacija.Models;
using BookMySpotAPI.Data;
using BookMySpotAPI.Modul.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMySpotAPI.Autentifikacija.Helper
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            [JsonIgnore]
            public KorisnickiNalog korisnickiNalog => autentifikacijaToken?.KorisnickiNalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            public bool isLogiran => korisnickiNalog != null;

            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }

        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            AutentifikacijaToken KorisnickiNalog = db.AutentifikacijaTokeni.Include(x => x.KorisnickiNalog).SingleOrDefault(x => token != null && x.vrijednost == token);

            return KorisnickiNalog;
        }

        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }

    }
}
