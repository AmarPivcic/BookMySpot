using BookMySpotAPI.Data;
using BookMySpotAPI.Helper;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMySpotAPI.TestniPodaciModul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;

        public TestniPodaciController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher();
        }

        [HttpPost]
        public async Task <ActionResult> Generate()
        {
            var korisnik = new Korisnik
            {
                ime = "UserIme",
                prezime = "UserPrezime",
                email = "user@gmail.com",
                telefon = "+38763222222",
                korisnickoIme = "user",
                brojRezervacija = 0
            };

            var hashedPassword = _passwordHasher.HashPassword(korisnik, "user");
            korisnik.lozinka = hashedPassword;

            await _dbContext.Korisnici.AddAsync(korisnik);
            await _dbContext.SaveChangesAsync();


            var smjestaj = new Kategorija
            {
                naziv = "Smještaj",
                slika = "https://localhost:7058/Slike/room.jpg"
            };

            await _dbContext.Kategorije.AddAsync(smjestaj);
            await _dbContext.SaveChangesAsync();


            var saloniLjepote = new Kategorija
            {
                naziv = "Saloni Ljepote",
                slika = "https://localhost:7058/Slike/beauty.jpg"
            };

            await _dbContext.Kategorije.AddAsync(saloniLjepote);
            await _dbContext.SaveChangesAsync();


            var ugostiteljstvo = new Kategorija
            {
                naziv = "Ugostiteljstvo",
                slika = "https://localhost:7058/Slike/restoran.jpg"
            };

            await _dbContext.Kategorije.AddAsync(ugostiteljstvo);
            await _dbContext.SaveChangesAsync();


            var ordinacije = new Kategorija
            {
                naziv = "Ordinacije",
                slika = "https://localhost:7058/Slike/ordinacija.jpg"
            };

            await _dbContext.Kategorije.AddAsync(ordinacije);
            await _dbContext.SaveChangesAsync();


            var vozila = new Kategorija
            {
                naziv = "Vozila",
                slika = "https://localhost:7058/Slike/vozila.jpg"
            };

            await _dbContext.Kategorije.AddAsync(vozila);
            await _dbContext.SaveChangesAsync();


            var oNamaSadrzaj = new ONamaSadrzaj
            {
                Tekst = "<center>\n\n<img src=\"/assets/BookMySpot.png\" alt=\"BookMySpot\" width=\"150\" height=\"100\" />\n\n### Dobrodošli u BookMySpot!\n\n<br>\n\nBookMySpot je inovativna platforma koja vam omogućava jednostavno i\n        brzo rezerviranje smještaja, salona ljepote, ugostiteljskih objekata,\n        ordinacija i raznih usluga, sve na jednom mjestu. Naša misija je\n        spojiti vas s najboljim destinacijama i pružateljima usluga,\n        omogućavajući vam da uživate u vrhunskom iskustvu bez nepotrebnog\n        stresa ili čekanja.\n\n<br>\n\n**Šta nas čini posebnima?**\n\n<br>\n\n*➼ Sve na jednom mjestu: BookMySpot vam pruža širok spektar opcija za\n        rezervaciju, bilo da tražite savršen smještaj za odmor, opuštajući\n        tretman u salonu ljepote, ukusan obrok u restoranu ili stručnu\n        medicinsku uslugu u ordinaciji..*\n\n<br>\n\n*➼ Jednostavno i brzo: Naša platforma je dizajnirana s fokusom na\n        korisničko iskustvo. Brza navigacija i jednostavno sučelje omogućavaju\n        vam da rezervirate svoje mjesto u svega nekoliko klikova.*\n\n<br>\n\n*➼ Recenzije i ocjene: Povratne informacije naših korisnika su nam\n        izuzetno važne. Pružamo vam mogućnost čitanja recenzija drugih\n        korisnika kako biste donijeli informiranu odluku i dijelili vlastita\n        iskustva.*\n\n<br>\n\n*➼ Personalizirane preporuke: Na temelju vaših preferencija i povijesti\n        rezervacija, nudimo vam personalizirane preporuke kako biste uvijek\n        pronašli ono što vam najbolje odgovara.*\n\n</center>"
            };

            await _dbContext.SadrzajiONama.AddAsync(oNamaSadrzaj);
            await _dbContext.SaveChangesAsync();

            var mostar = new Grad
            {
                naziv = "Mostar"
            };

            await _dbContext.Gradovi.AddAsync(mostar);
            await _dbContext.SaveChangesAsync();

            var livno = new Grad
            {
                naziv = "Livno"
            };

            await _dbContext.Gradovi.AddAsync(livno);
            await _dbContext.SaveChangesAsync();

            var sarajevo = new Grad
            {
                naziv = "Sarajevo"
            };

            await _dbContext.Gradovi.AddAsync(sarajevo);
            await _dbContext.SaveChangesAsync();

            var manager = new Manager
            {
                ime = "ManagerIme",
                prezime = "ManagerPrezime",
                email = "manager@gmail.com",
                telefon = "+38763135135",
                korisnickoIme = "manager",
                pozicija = "Vlasnik"
            };

            var managerPassword = _passwordHasher.HashPassword(manager, "manager");
            manager.lozinka = managerPassword;

            var manager2 = new Manager
            {
                ime = "KrecoRadnik",
                prezime = "KrecoRadnikPrezime",
                email = "krecoradnik@gmail.com",
                telefon = "+38763135135",
                korisnickoIme = "krecoradnik",
                pozicija = "Radnik"
            };

            var manager2Password = _passwordHasher.HashPassword(manager2, "krecoradnik");
            manager.lozinka = manager2Password;

 

            var krecho = new UsluzniObjekt
            {
                nazivObjekta = "Barbershop Krecho",
                adresa = "Zalik",
                telefon = "+38734123123",
                slika = "https://localhost:7058/Slike/kreco.jpg",
                kategorijaID = 2,
                gradID = 1,
                radnoVrijemePocetak = "08:00",
                radnoVrijemeKraj = "20:00",
            };

            var celeBrico = new Manager
            {
                ime = "Ćele",
                prezime = "Brico",
                email = "celebrico@gmail.com",
                telefon = "+38763135135",
                korisnickoIme = "cele",
                pozicija = "Vlasnik"
            };

            var celePassword = _passwordHasher.HashPassword(celeBrico, "cele");
            manager.lozinka = celePassword;

            var celeSalon = new UsluzniObjekt
            {
                nazivObjekta = "Frizerski salon 'Ćele'",
                adresa = "Obrtnička 3",
                telefon = "+38734123123",
                slika = "https://localhost:7058/Slike/celeBrico.jpg",
                kategorijaID = 2,
                gradID = 2,
                radnoVrijemePocetak = "08:00",
                radnoVrijemeKraj = "14:00",
                latitude = 43.8272,
                longitude = 17.0052
            };

            var managerUsluzniObjekt = new ManagerUsluzniObjekt
            {
                manager = manager,
                usluzniObjekt = krecho
            };

            var managerUsluzniObjekt2 = new ManagerUsluzniObjekt
            {
                manager = manager2,
                usluzniObjekt = krecho
            };

            var celeManagerSalon = new ManagerUsluzniObjekt
            {
                manager = celeBrico,
                usluzniObjekt = celeSalon
            };

            await _dbContext.Manageri.AddAsync(manager);
            await _dbContext.Manageri.AddAsync(manager2);
            await _dbContext.Manageri.AddAsync(celeBrico);
            await _dbContext.UsluzniObjekti.AddAsync(krecho);
            await _dbContext.UsluzniObjekti.AddAsync(celeSalon);
            await _dbContext.ManagerUsluzniObjekti.AddAsync(managerUsluzniObjekt);
            await _dbContext.ManagerUsluzniObjekti.AddAsync(managerUsluzniObjekt2);
            await _dbContext.ManagerUsluzniObjekti.AddAsync(celeManagerSalon);
            await _dbContext.SaveChangesAsync();

            var usluga1 = new Usluga
            {
                naziv = "Moderno šišanje (Fade)",
                trajanje = 30,
                cijena = "15KM",
                usluzniObjektID = krecho.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(usluga1);
            await _dbContext.SaveChangesAsync();

            var usluga2 = new Usluga
            {
                naziv = "Klasično šišanje",
                trajanje = 30,
                cijena = "10KM",
                usluzniObjektID = krecho.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(usluga2);
            await _dbContext.SaveChangesAsync();

            var usluga3 = new Usluga
            {
                naziv = "Uređivanje brade",
                trajanje = 15,
                cijena = "5KM",
                usluzniObjektID = krecho.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(usluga3);
            await _dbContext.SaveChangesAsync();

            var usluga4 = new Usluga
            {
                naziv = "Šišanje duge kose",
                trajanje = 45,
                cijena = "15KM",
                usluzniObjektID = celeSalon.usluzniObjektID,
            };

            await _dbContext.Usluge.AddAsync(usluga4);
            await _dbContext.SaveChangesAsync();

            var celeusluga1 = new Usluga
            {
                naziv = "Moderno šišanje (Fade)",
                trajanje = 30,
                cijena = "15KM",
                usluzniObjektID = celeSalon.usluzniObjektID,
            };

            await _dbContext.Usluge.AddAsync(celeusluga1);
            await _dbContext.SaveChangesAsync();

            var celeusluga2 = new Usluga
            {
                naziv = "Klasično šišanje",
                trajanje = 30,
                cijena = "10KM",
                usluzniObjektID = celeSalon.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(celeusluga2);
            await _dbContext.SaveChangesAsync();

            var celeusluga3 = new Usluga
            {
                naziv = "Uređivanje brade",
                trajanje = 15,
                cijena = "5KM",
                usluzniObjektID = celeSalon.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(celeusluga3);
            await _dbContext.SaveChangesAsync();

            var celeusluga4 = new Usluga
            {
                naziv = "Šišanje na nulu",
                trajanje = 15,
                cijena = "7KM",
                usluzniObjektID = celeSalon.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(celeusluga4);
            await _dbContext.SaveChangesAsync();

            var managerSmjestaja = new Manager
            {
                ime = "ManagerSmjestaj",
                prezime = "ManagerP",
                email = "manager@gmail.com",
                telefon = "+38763135135",
                korisnickoIme = "managerSmjestaja",
                pozicija = "Vlasnik"
            };

            var managerSmjestajaPassword = _passwordHasher.HashPassword(manager, "manager");
            managerSmjestaja.lozinka = managerSmjestajaPassword;

            var vilaBojcic = new UsluzniObjekt
            {
                nazivObjekta = "Vila Bojčić",
                adresa = "Zalik",
                telefon = "+38734123123",
                slika = "https://localhost:7058/Slike/restoran.jpg",
                kategorijaID = 1,
                gradID = 1,
                radnoVrijemePocetak = "08:00",
                radnoVrijemeKraj = "00:00",
                isSmjestaj = true,
               latitude = 43.35432,
               longitude = 17.81242
            };

            var managerSmjestajUsluzniObjekt = new ManagerUsluzniObjekt
            {
                manager = managerSmjestaja,
                usluzniObjekt = vilaBojcic
            };

            await _dbContext.Manageri.AddAsync(managerSmjestaja);
            await _dbContext.UsluzniObjekti.AddAsync(vilaBojcic);
            await _dbContext.ManagerUsluzniObjekti.AddAsync(managerSmjestajUsluzniObjekt);
            await _dbContext.SaveChangesAsync();

            var uslugaSmjestaja1 = new Usluga
            {
                naziv = "Apartman (veći)",
                cijena = "35KM",
                usluzniObjektID = vilaBojcic.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaSmjestaja1);
            await _dbContext.SaveChangesAsync();

            var uslugaSmjestaja2 = new Usluga
            {
                naziv = "Apartman (manji)",
                cijena = "30KM",
                usluzniObjektID = vilaBojcic.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaSmjestaja2);
            await _dbContext.SaveChangesAsync();

            var uslugaSmjestaja3 = new Usluga
            {
                naziv = "Dvokrevetna soba",
                cijena = "25KM",
                usluzniObjektID = vilaBojcic.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaSmjestaja3);
            await _dbContext.SaveChangesAsync();

            var uslugaSmjestaja4 = new Usluga
            {
                naziv = "Jednokrevetna soba",
                cijena = "20KM",
                usluzniObjektID = vilaBojcic.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaSmjestaja4);
            await _dbContext.SaveChangesAsync();

            var novitasManager = new Manager
            {
                ime = "Novitas",
                prezime = "Manager",
                email = "novitasmanager@gmail.com",
                telefon = "+38763135135",
                korisnickoIme = "novitas",
                pozicija = "Vlasnik"
            };

            var novitasManagerPW = _passwordHasher.HashPassword(manager, "novitas");
            novitasManager.lozinka = novitasManagerPW;

            var novitas = new UsluzniObjekt
            {
                nazivObjekta = "Hotel 'Novitas'",
                adresa = "Industrijska zona - jug",
                telefon = "+38734123123",
                slika = "https://localhost:7058/Slike/novitas.jpg",
                kategorijaID = 1,
                gradID = 2,
                radnoVrijemePocetak = "08:00",
                radnoVrijemeKraj = "00:00",
                isSmjestaj = true,
                latitude = 43.8217,
                longitude = 16.9964
            };

            var novitasUsluzniObjekt = new ManagerUsluzniObjekt
            {
                manager = novitasManager,
                usluzniObjekt = novitas
            };

            await _dbContext.Manageri.AddAsync(novitasManager);
            await _dbContext.UsluzniObjekti.AddAsync(novitas);
            await _dbContext.ManagerUsluzniObjekti.AddAsync(novitasUsluzniObjekt);
            await _dbContext.SaveChangesAsync();

            var uslugaNovitas1 = new Usluga
            {
                naziv = "Apartman (veći)",
                cijena = "180KM",
                usluzniObjektID = novitas.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaNovitas1);
            await _dbContext.SaveChangesAsync();

            var uslugaNovitas2 = new Usluga
            {
                naziv = "Apartman (manji)",
                cijena = "140KM",
                usluzniObjektID = novitas.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaNovitas2);
            await _dbContext.SaveChangesAsync();

            var uslugaNovitas3 = new Usluga
            {
                naziv = "Dvokrevetna soba",
                cijena = "100KM",
                usluzniObjektID = novitas.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaNovitas3);
            await _dbContext.SaveChangesAsync();

            var uslugaNovitas4 = new Usluga
            {
                naziv = "Jednokrevetna soba",
                cijena = "75KM",
                usluzniObjektID = novitas.usluzniObjektID
            };

            await _dbContext.Usluge.AddAsync(uslugaNovitas4);
            await _dbContext.SaveChangesAsync();

            var favorit = new Favorit
            {
                KorisnickiNalogId = korisnik.osobaID,
                usluzniObjektID = krecho.usluzniObjektID
            };

            await _dbContext.Favoriti.AddAsync(favorit);
            await _dbContext.SaveChangesAsync();

            var recenzija = new Recenzija
            {
                recenzijaOcjena = 5,
                recenzijaTekst = "Ovo je recenzija 1",
                usluzniObjektID = 1,
                KorisnickiNalogId = manager.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzija);
            await _dbContext.SaveChangesAsync();

            var recenzija2 = new Recenzija
            {
                recenzijaOcjena = 4,
                recenzijaTekst = "Ovo je recenzija 2",
                usluzniObjektID = 1,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzija2);
            await _dbContext.SaveChangesAsync();

            var recenzija3 = new Recenzija
            {
                recenzijaOcjena = 5,
                recenzijaTekst = "Ovo je recenzija 3",
                usluzniObjektID = 1,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzija3);
            await _dbContext.SaveChangesAsync();

            var recenzijanovitas = new Recenzija
            {
                recenzijaOcjena = 5,
                recenzijaTekst = "Ovo je recenzija 1",
                usluzniObjektID = novitas.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijanovitas);
            await _dbContext.SaveChangesAsync();

            var recenzijanovitas2 = new Recenzija
            {
                recenzijaOcjena = 4,
                recenzijaTekst = "Ovo je recenzija 2",
                usluzniObjektID = novitas.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijanovitas2);
            await _dbContext.SaveChangesAsync();

            var recenzijanovitas3 = new Recenzija
            {
                recenzijaOcjena = 5,
                recenzijaTekst = "Ovo je recenzija 3",
                usluzniObjektID = novitas.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijanovitas3);
            await _dbContext.SaveChangesAsync();

            var recenzijanovitas4 = new Recenzija
            {
                recenzijaOcjena = 4,
                recenzijaTekst = "Ovo je recenzija 4",
                usluzniObjektID = novitas.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijanovitas4);
            await _dbContext.SaveChangesAsync();

            var recenzijanovitas5 = new Recenzija
            {
                recenzijaOcjena = 4,
                recenzijaTekst = "Ovo je recenzija 5",
                usluzniObjektID = novitas.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijanovitas5);
            await _dbContext.SaveChangesAsync();


            var recenzijacele = new Recenzija
            {
                recenzijaOcjena = 5,
                recenzijaTekst = "Ovo je recenzija 1",
                usluzniObjektID = celeSalon.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijacele);
            await _dbContext.SaveChangesAsync();

            var recenzijacele2 = new Recenzija
            {
                recenzijaOcjena = 4,
                recenzijaTekst = "Ovo je recenzija 2",
                usluzniObjektID = celeSalon.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijacele2);
            await _dbContext.SaveChangesAsync();

            var recenzijacele3 = new Recenzija
            {
                recenzijaOcjena = 4,
                recenzijaTekst = "Ovo je recenzija 3",
                usluzniObjektID = celeSalon.usluzniObjektID,
                KorisnickiNalogId = korisnik.osobaID
            };

            await _dbContext.Recenzije.AddAsync(recenzijacele3);
            await _dbContext.SaveChangesAsync();



            var admin = new Administrator
            {
                ime = "AdminIme",
                prezime = "AdminPrezime",
                email = "admin@gmail.com",
                telefon = "+38763135135",
                korisnickoIme = "admin",
                PIN = "123"
            };

            var adminPassword = _passwordHasher.HashPassword(admin, "admin");
            admin.lozinka = adminPassword;

            _dbContext.Administratori.Add(admin);
            await _dbContext.SaveChangesAsync();


            var pitanjeOdgvor1 = new PitanjeOdgovor
            {
                DatumKreiranja = DateTime.Now.AddDays(-78),
                KorisnickiNalogId = 1,
                Pitanje = "Kako rezervisati smještaj putem aplikacije?",
                Odgovor = "Nakon odabira željenog smještaja, pritisnite opciju \"Rezerviši\". Slijedite upute za unos datuma useljenja, datuma iseljenja i eventualnih dodatnih zahtjeva. Na kraju postupka, potvrdite rezervaciju."
            };

            _dbContext.PitanjaOdgovori.Add(pitanjeOdgvor1);
            await _dbContext.SaveChangesAsync();

            var pitanjeOdgvor2 = new PitanjeOdgovor
            {
                DatumKreiranja = DateTime.Now.AddDays(-55),
                KorisnickiNalogId = 2,
                Pitanje = "Koje mjere sigurnosti poduzimate kako biste osigurali sigurnost podataka korisnika?",
                Odgovor = "Aplikacija koristi napredne sigurnosne mjere kako bi zaštitila osobne podatke korisnika. Sva komunikacija i transakcije enkriptirane su radi sigurnosti korisnika."
            };

            _dbContext.PitanjaOdgovori.Add(pitanjeOdgvor2);
            await _dbContext.SaveChangesAsync();

            var pitanjeOdgvor3 = new PitanjeOdgovor
            {
                DatumKreiranja = DateTime.Now.AddDays(-47),
                KorisnickiNalogId = 3,
                Pitanje = "Kako ocjenjujete kvalitetu smještaja i usluga salona ljepote na aplikaciji?",
                Odgovor = "Korisnici mogu ocjenjivati i ostavljati recenzije nakon svake rezervacije. Ove ocjene i recenzije pomažu drugim korisnicima u donošenju informirane odluke."
            };

            _dbContext.PitanjaOdgovori.Add(pitanjeOdgvor3);
            await _dbContext.SaveChangesAsync();

            var pitanjeOdgvor4 = new PitanjeOdgovor
            {
                DatumKreiranja = DateTime.Now.AddDays(-20),
                KorisnickiNalogId = 1,
                Pitanje = "Kako mogu pratiti svoje prethodne rezervacije i buduće termine u aplikaciji?",
                Odgovor = "Sve vaše rezervacije dostupne su u sekciji \"Historija\". Ovdje možete pregledati aktivne rezervacije, otkazane rezervacije, kao i mogućnost obnavljanja rezervacije."
            };

            _dbContext.PitanjaOdgovori.Add(pitanjeOdgvor4);
            await _dbContext.SaveChangesAsync();

            var pitanjeOdgvor5 = new PitanjeOdgovor
            {
                DatumKreiranja = DateTime.Now.AddDays(-3),
                KorisnickiNalogId = 4,
                Pitanje = "Mogu li pogledati fotografiju smještaja ili salona ljepote prije rezervacije?",
                Odgovor = "Da, aplikacija pruža pregled fotografije smještaja ili salona ljepote."
            };

            _dbContext.PitanjaOdgovori.Add(pitanjeOdgvor5);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
