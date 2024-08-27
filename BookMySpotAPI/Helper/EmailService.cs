using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookMySpotAPI.Helper
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("bookmyspotapp@gmail.com", "BookMySpot App");
            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                var error = await response.Body.ReadAsStringAsync();
                Console.WriteLine($"Failed to send email. Status Code: {response.StatusCode}, Error: {error}");
            }
        }
    }
}
