using BookMySpotAPI.Modul.Controllers;
using BookMySpotAPI.Modul.Models;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail.Model;

namespace BookMySpotAPI.Helper
{
    public class EmailTemplates
    {
        private readonly EmailService _emailService;

        public EmailTemplates(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task RegistracijaMail(Korisnik x)
        {
            string toEmail = x.email;
            string subject = "Dobrodošli u BookMySpot";
            string plainTextContent = $"Poštovani/a {x.ime + " " + x.prezime},\n\nDobrodošli u BookMySpot! Hvala Vam što ste se registrirali na našu " +
            $"aplikaciju za pretragu i rezervaciju usluga i smještaja. Ovim e-mailom potvrđujemo " +
            $"Vašu uspješnu registraciju.\r\n\r\n" +
            $"Vaši korisnički podaci:\r\n" +
            $"Korisničko ime: {x.korisnickoIme}\r\n" +
            $"E-mail adresa: {x.email}\r\n" +
            $"Za početak, prijavite se u aplikaciju koristeći svoje korisničke podatke i istražite sve mogućnosti koje " +
            $"Vam pruža BookMySpot.\r\n\r\nAko imate bilo kakva pitanja ili trebate dodatnu pomoć, slobodno nas " +
            $"kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije. Naš tim će Vam rado pomoći i osigurati da imate što " +
            $"ugodnije iskustvo korištenja aplikacije.\r\n\r\nJoš jednom, dobrodošli u BookMySpot! Veselimo se što " +
            $"ćemo Vam pomoći u planiranju vaših obaveza i osigurati Vam udoban smještaj.\r\n\r\nS " +
            $"poštovanjem,\r\nBookMySpot Team.";
            string htmlContent = "";

            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }

        public async Task RezervacijaMail(Rezervacija x)
        {
            string toEmail = x.korisnik.email;
            string subject = "BookMySpot - Potvrda o rezervaciji";
            string plainTextContent = $"Poštovani/a {x.korisnik.ime + " " + x.korisnik.prezime},\n\n" +
            $"Hvala Vam što ste koristili našu aplikaciju za rezervaciju usluge.\n\n" +
            $"Ovim e-mailom potvrđujemo detalje vaše rezervacije:\n" +
            $"Datum rezervacije: {x.datumRezervacije.ToString("dd.MM.yyyy")}\n" +
            $"Početak rezervacije: {x.rezervacijaPocetak}\n" +
            $"Kraj rezervacije: {x.rezervacijaKraj}\n" +
            $"Uslužni objekt: {x.usluzniObjekt.nazivObjekta}\n" +
            $"Usluga: {x.usluga.naziv}\n" +
            $"Radnik: {x.manager.ime + " " +x.manager.prezime}\n" +
            $"Cijena: {x.usluga.cijena}\n" +
            $"Trajanje: {x.usluga.trajanje} min\n" +
            $"Vaša rezervacija je uspješno evidentrana i možete ju pregledati u historiji rezervacija. Također ju možete \n" +
            $"otkazati u historiji rezervacija. Molimo Vas da otkazivanje vaših rezervacija ne činite tik pred početak iste. \n" +
            $"Također, nemojte praviti rezervacije ako se nećete pojaviti. Ako imate bilo kakva pitanja ili trebate dodatne \n\n" +
            $"informacije, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije.\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";
            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }

        public async Task OtkaziRezervacijaMail(Rezervacija x)
        {
            string toEmail = x.korisnik.email;
            string subject = "BookMySpot - Potvrda o otkazanoj rezervaciji";
            string plainTextContent = $"Poštovani/a {x.korisnik.ime + " " + x.korisnik.prezime},\n\n" +
            $"Uspješno ste otkazali rezervaciju.\n\n" +
            $"Ovim e-mailom potvrđujemo detalje vaše otkazane rezervacije:\n" +
            $"Datum rezervacije: {x.datumRezervacije.ToString("dd.MM.yyyy")}\n" +
            $"Početak rezervacije: {x.rezervacijaPocetak}\n" +
            $"Kraj rezervacije: {x.rezervacijaKraj}\n" +
            $"Uslužni objekt: {x.usluzniObjekt.nazivObjekta}\n" +
            $"Usluga: {x.usluga.naziv}\n" +
            $"Radnik: {x.manager.ime + " " + x.manager.prezime}\n" +
            $"Cijena: {x.usluga.cijena}\n" +
            $"Trajanje: {x.usluga.trajanje} min\n" +
            $"Vaša rezervacija je uspješno otkazana. Možete ju pogledati u historiji rezervacija\n" +
            $" u kartici sa prethodnim rezervacijama. Također ju možete vrlo lako ponoviti iz historije rezervacija, samo odaberite \n" +
            $" novi datum i termin rezervacije, usluga i radnik ostaju isti. Ako imate bilo kakva pitanja ili trebate dodatne \n" +
            $"informacije, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije.\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";
            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            string toManagerEmail = x.manager.email;
            string subjectManager = "BookMySpot - Potvrda o otkazanoj rezervaciji";
            string plainTextContentManager = $"Poštovani/a {x.manager.ime + " " + x.manager.prezime},\n\n" +
            $"Korisnik je otkazao rezervaciju kod Vas.\n\n" +
            $"Ovim e-mailom potvrđujemo detalje otkazane rezervacije:\n" +
            $"Datum rezervacije: {x.datumRezervacije.ToString("dd.MM.yyyy")}\n" +
            $"Početak rezervacije: {x.rezervacijaPocetak}\n" +
            $"Kraj rezervacije: {x.rezervacijaKraj}\n" +
            $"Usluga: {x.usluga.naziv}\n" +
            $"Korisnik: {x.korisnik.ime + " " + x.korisnik.prezime}\n" +
            $"Rezervacija je uspješno otkazana. Možete ju pogledati u historiji rezervacija\n" +
            $" u kartici sa prethodnim rezervacijama. Termin ove rezervacije je ponovno uvršten u slobodne termine. \n" +
            $"Ako imate bilo kakva pitanja ili trebate dodatne informacije, slobodno nas kontaktirajte\n" +
            $" putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije ili putem e-maila: bookmyspotapp@gmail.com.\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";
            string htmlContentManager = "";

            await _emailService.SendEmailAsync(toManagerEmail, subjectManager, plainTextContentManager, htmlContentManager);

            return;
        }

        public async Task RezervacijaSmjestajMail(Rezervacija x)
        {
            string toEmail = x.korisnik.email;
            string subject = "BookMySpot - Potvrda o rezervaciji";
            string plainTextContent = $"Poštovani/a {x.korisnik.ime + " " + x.korisnik.prezime},\n\n" +
            $"Hvala Vam što ste koristili našu aplikaciju za rezervaciju smještaja.\n\n" +
            $"Ovim e-mailom potvrđujemo detalje vaše rezervacije:\n" +
            $"Datum kreiranja rezervacije: {x.datumRezervacije.ToString("dd.mm.yyyy")}\n" +
            $"Početak rezervacije: {x.rezervacijaPocetak}\n" +
            $"Kraj rezervacije: {x.rezervacijaKraj}\n" +
            $"Uslužni objekt: {x.usluzniObjekt.nazivObjekta}\n" +
            $"Usluga: {x.usluga.naziv}\n" +
            $"Cijena: {x.usluga.cijena}/dan\n" +
            $"Vaša rezervacija je uspješno evidentrana i možete ju pregledati u historiji rezervacija. Također ju možete \n" +
            $"otkazati u historiji rezervacija. Molimo Vas da otkazivanje vaših rezervacija ne činite tik pred početak iste.\n " +
            $"Također, nemojte praviti rezervacije ako se nećete pojaviti. Ako imate bilo kakva pitanja ili trebate \n\n" +
            $"dodatne informacije, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije ili putem e-maila: bookmyspotapp@gmail.com.\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";
            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }
        public async Task PostavljenoPitanjeEmail(PitanjeOdgovor y, KorisnickiNalog x)
        {
            string toEmail = x.email;
            string subject = "BookMySpot - Potvrda o postavljenom pitanju";
            string plainTextContent = $"Poštovani/a {x.ime + " " + x.prezime},\n\n" +
            $"Zahvaljujemo se što ste nam postavili pitanje putem naše aplikacije.\n\n" +
            $"Ovim e-mailom potvrđujemo da smo zaprimili Vaše pitanje.\n" +
            $"Pitanje ste postavili dana: {y.DatumKreiranja.ToString("dd.MM.yyyy")}\n" +
            $"Vaše pitanje glasi: {y.Pitanje}\n\n" +
            $"Vaše pitanje je trenutno na čekanju i naše osoblje će Vam odgovoriti u najkraćem mogućem roku. Kada odgovor bude spreman, bit ćete obaviješteni putem e-maila.\n" +
            $"Nakon što Vam odgovorimo, Vaše pitanje i naš odgovor bit će javno dostupni svim korisnicima unutar sekcije za pitanja.\n\n" +
            $"Napominjemo da neprikladan govor ili sadržaj u postavljenim pitanjima može dovesti do sankcionisanja, uključujući privremeno suspendovanje ili trajno brisanje Vašeg naloga.\n\n" +
            $"Ako imate bilo kakva dodatna pitanja ili trebate više informacija, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je dostupan u gornjem desnom ćošku aplikacije ili putem e-maila: bookmyspotapp@gmail.com\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";

            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }
        public async Task PostavljeniOdgovorNaPitanjeEmail(PitanjeOdgovor y, KorisnickiNalog x)
        {
            string toEmail = x.email;
            string subject = "BookMySpot - Obavještenje o postavljenom odgovoru na pitanje";
            string plainTextContent = $"Poštovani/a {x.ime + " " + x.prezime},\n\n" +
            $"Zahvaljujemo se što ste nam postavili pitanje putem naše aplikacije.\n\n" +
            $"Ovim e-mailom Vas obaviještavamo da je osoblje dalo odgovor na Vaše pitanje.\n" +
            $"Pitanje ste postavili dana: {y.DatumKreiranja.ToString("dd.MM.yyyy")}\n" +
            $"Pitanje glasi: {y.Pitanje}\n" +
            $"Odgovor glasi: {y.Odgovor}\n\n" +
            $"Vaš odgovor je sada dostupan javno unutar sekcije za pitanja, gdje svi korisnici mogu pregledati pitanja i odgovore.\n\n" +
            $"Ako imate bilo kakva dodatna pitanja ili trebate više informacija, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je dostupan u gornjem desnom ćošku aplikacije ili putem e-maila: bookmyspotapp@gmail.com\n\n" +
            $"Napominjemo da je Vaše korisničko iskustvo za nas veoma važno. Ukoliko imate povratne informacije o našem servisu, slobodno ih podijelite s nama.\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";

            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }
        public async Task AdminBrisanjeKorisnickogNaloga(KorisnickiNalog k)
        {
            string toEmail = k.email;
            string subject = "BookMySpot - Obavještenje o brisanju korisničkog naloga";
            string plainTextContent = $"Poštovani/a {k.ime + " " + k.prezime},\n\n" +
            $"Obavještavamo Vas da je Vaš korisnički nalog na BookMySpot aplikaciji obrisan od strane našeg osoblja zbog kršenja pravila koje nalaže naša aplikacija.\n\n" +
            $"Razumijemo da ovakva situacija može biti neprijatna. Ukoliko smatrate da je došlo do greške ili imate dodatna pitanja, molimo Vas da nas kontaktirate na naš e-mail: bookmyspotapp@gmail.com\n" +
            $"Vaš slučaj ćemo razmotriti i ukoliko bude moguće, razmotriti vraćanje naloga.\n\n" +
            $"Molimo Vas da se pridržavate pravila i uslova korištenja naše aplikacije kako bismo osigurali sigurno i prijatno iskustvo za sve korisnike.\n\n" +
            $"Hvala na razumijevanju.\n\n" +
            $"S poštovanjem,\n" +
            $"BookMySpot Tim";

            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }
        public async Task SuspendovanjeRacunaMail(KorisnickiNalog k)
        {
            string toEmail = k.email;
            string subject = "BookMySpot - Obavještenje o suspenziji korisničkog naloga";
            string plainTextContent = $"Poštovani/a {k.ime + " " + k.prezime},\n\n" +
            $"Obavještavamo Vas da je Vaš korisnički nalog na BookMySpot aplikaciji suspendovan od strane našeg osoblja zbog kršenja pravila koja su propisana našom aplikacijom.\n\n" +
            $"Razlog suspenzije: {k.razlogSuspenzije}\n\n" +
            $"Vaš nalog će biti suspendovan do: {(k.datumSuspenzijeDo.HasValue ? k.datumSuspenzijeDo.Value.ToString("dd.MM.yyyy") : "N/A")}\n\n" +
            $"Razumijemo da ova situacija može biti neprijatna. Ukoliko smatrate da je došlo do greške ili imate dodatna pitanja, molimo Vas da nas kontaktirate putem e-maila: bookmyspotapp@gmail.com\n" +
            $"Vaš slučaj će biti razmotren, i ukoliko bude moguće, razmotrićemo ponovno aktiviranje naloga.\n\n" +
            $"Molimo Vas da se ubuduće pridržavate pravila i uslova korištenja naše aplikacije kako bismo osigurali sigurno i prijatno iskustvo za sve korisnike.\n\n" +
            $"Hvala na razumijevanju.\n\n" +
            $"S poštovanjem,\n" +
            $"BookMySpot Tim";

            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }
        public async Task AktiviranSuspendovanRacunMail(KorisnickiNalog k)
        {
            string toEmail = k.email;
            string subject = "BookMySpot - Obavještenje o ukidanju suspenzije korisničkog naloga";
            string plainTextContent = $"Poštovani/a {k.ime + " " + k.prezime},\n\n" +
            $"Obavještavamo Vas da je suspenzija Vašeg korisničkog naloga na BookMySpot aplikaciji ukinuta.\n\n" +
            $"Vaš nalog je sada ponovo aktivan, i možete nastaviti koristiti našu aplikaciju bez ograničenja.\n\n" +
            $"Molimo Vas da se ubuduće pridržavate pravila i uslova korištenja naše aplikacije kako bismo osigurali sigurno i prijatno iskustvo za sve korisnike.\n\n" +
            $"Ukoliko imate bilo kakva dodatna pitanja ili trebate dodatne informacije, slobodno nas kontaktirajte putem e-maila: bookmyspotapp@gmail.com\n\n" +
            $"Hvala Vam što ste ponovo dio naše zajednice.\n\n" +
            $"S poštovanjem,\n" +
            $"BookMySpot Tim";

            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }

        public async Task ObrisiRacunMail(KorisnickiNalog k)
        {
            string toEmail = k.email;
            string subject = "BookMySpot - Obavještenje o brisanju korisničkog naloga";
            string plainTextContent = $"Poštovani/a {k.ime + " " + k.prezime},\n\n" +
            $"Obavještavamo Vas da je Vaš korisniči nalog na BookMySpot aplikaciji obrisan.\n\n" +
            $"Vaš nalog je sada nepostojeći, te za ponovno pravljenje rezervacija na našoj aplikaciji morate napraviti novi.\n\n" +
            $"Ukoliko imate bilo kakva dodatna pitanja ili trebate dodatne informacije, slobodno nas kontaktirajte putem e-maila: bookmyspotapp@gmail.com\n\n" +
            $"Hvala Vam što ste ponovo dio naše zajednice.\n\n" +
            $"S poštovanjem,\n" +
            $"BookMySpot Tim";

            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }
    }
}
