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
            //string htmlContent = $"<h1>Poštovani/a {x.ime + " " + x.prezime},\n\nDobrodošli u BookMySpot!</h1> <h2>Hvala Vam što ste se registrirali na našu " +
            //$"aplikaciju za pretragu i rezervaciju usluga i smještaja.</h2> <strong>Ovim e-mailom potvrđujemo " +
            //$"Vašu uspješnu registraciju.\r\n\r\n" +
            //$"Vaši korisnički podaci:\r\n" +
            //$"Korisničko ime: {x.korisnickoIme}\r\n" +
            //$"E-mail adresa: {x.email}\r\n" +
            //$"Za početak, prijavite se u aplikaciju koristeći svoje korisničke podatke i istražite sve mogućnosti koje " +
            //$"Vam pruža BookMySpot.\r\n\r\nAko imate bilo kakva pitanja ili trebate dodatnu pomoć, slobodno nas " +
            //$"kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije. Naš tim će Vam rado pomoći i osigurati da imate što " +
            //$"ugodnije iskustvo korištenja aplikacije.\r\n\r\nJoš jednom, dobrodošli u BookMySpot! Veselimo se što " +
            //$"ćemo Vam pomoći u planiranju vaših obaveza i osigurati Vam udoban smještaj.\r\n\r\nS " +
            //$"poštovanjem,\r\nBookMySpot Team</strong>";
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
            //string htmlContent = $"<h1>Poštovani/a {x.korisnik.ime + " " + x.korisnik.prezime},</h1>\n\n<h3>Hvala Vam što ste koristili našu " +
            //$"aplikaciju za rezervaciju usluge.\n\nOvim e-mailom potvrđujemo detalje vaše rezervacije:</h3>\n" +
            //$"<strong>Datum rezervacije: {x.datumRezervacije.ToString("dd.mm.yyyy")}\n" +
            //$"Početak rezervacije: {x.rezervacijaPocetak}\n" +
            //$"Kraj rezervacije: {x.rezervacijaKraj}\n" +
            //$"Uslužni objekt: {x.usluzniObjekt.nazivObjekta}\n" +
            //$"Usluga: {x.usluga.naziv}\n" +
            //$"Radnik: {x.manager.ime + " " + x.manager.prezime}\n" +
            //$"Cijena: {x.usluga.cijena}\nKM" +
            //$"Trajanje: {x.usluga.trajanje} min\n" +
            //$"Vaša rezervacija je uspješno evidentrana i možete ju pregledati u historiji rezervacija. Također ju možete " +
            //$"otkazati u historiji rezervacija. Molimo Vas da otkazivanje vaših rezervacija ne činite tik pred početak iste. " +
            //$"Također, nemojte praviti rezervacije ako se nećete pojaviti. \n\nAko imate bilo kakva pitanja ili trebate dodatne " +
            //$"informacije, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije.\n\nHvala Vam još jednom " +
            //$"na korištenju naše aplikacije!\n\nS poštovanjem, BookMySpot Team</strong>";
            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

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
            $"dodatne informacije, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije.\n\n" +
            $"Hvala Vam još jednom na korištenju naše aplikacije!\n\n" +
            $"S poštovanjem, BookMySpot Team.";
            //string htmlContent = $"<h1>Poštovani/a {x.korisnik.ime + " " + x.korisnik.prezime},</h1>\n\n<h3>Hvala Vam što ste koristili našu " +
            //$"aplikaciju za rezervaciju usluge/smještaja.\n\nOvim e-mailom potvrđujemo detalje vaše rezervacije:</h3>\n" +
            //$"<strong>Datum kreiranja rezervacije: {x.datumRezervacije.ToString("dd.mm.yyyy")}\n" +
            //$"Početak rezervacije: {x.rezervacijaPocetak}\n" +
            //$"Kraj rezervacije: {x.rezervacijaKraj}\n" +
            //$"Uslužni objekt: {x.usluzniObjekt.nazivObjekta}\n" +
            //$"Usluga: {x.usluga.naziv}\n" +
            //$"Cijena: {x.usluga.cijena}\nKM" +
            //$"Vaša rezervacija je uspješno evidentrana i možete ju pregledati u historiji rezervacija. Također ju možete " +
            //$"otkazati u historiji rezervacija. Molimo Vas da otkazivanje vaših rezervacija ne činite tik pred početak iste. " +
            //$"Također, nemojte praviti rezervacije ako se nećete pojaviti. \n\nAko imate bilo kakva pitanja ili trebate dodatne " +
            //$"informacije, slobodno nas kontaktirajte putem našeg korisničkog servisa koji je stalno dostupan u gornjem desnom ćošku aplikacije.\n\nHvala Vam još jednom " +
            //$"na korištenju naše aplikacije!\n\nS poštovanjem, BookMySpot Team</strong>";
            string htmlContent = "";


            await _emailService.SendEmailAsync(toEmail, subject, plainTextContent, htmlContent);

            return;
        }

    }
}
