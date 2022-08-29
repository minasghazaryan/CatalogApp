using CatalogShared;
using EmailApi.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace EmailApi.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(CatalogEventMessage content)
        {


            using var message = new MimeMessage();

            //my personal sendgrid account
            message.From.Add(new MailboxAddress(
                "Catalog-App",
                "minasghazaryan@gmail.com"
            ));

            message.To.Add(new MailboxAddress(
                "Catalog User",
                "minasghazaryan@mail.ru"
            ));
            message.Subject = "New Catalog";
            var bodyBuilder = new BodyBuilder
            {
                TextBody = "Catalog Added",
                HtmlBody = string.Format("The catalog with name -{0} has been added : price is - {1}", content.Name, content.Price)
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.sendgrid.net", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                userName: "apikey", 
                password: "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" 
            );
            try
            {
                await client.SendAsync(message);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            await client.DisconnectAsync(true);

        }
    }
}
