using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using savings_app_backend.Events;
using savings_app_backend.Models.Entities;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;

namespace savings_app_backend.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void NotifyRestaurantSoldProduct(Object sender, ProductSoldEventArgs args)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            email.To.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            email.Subject = "Product Sold";
            email.Body = new TextPart(TextFormat.Html) { Text =
                "You have sold " + args.Amount + " " + ((Product)sender).AmountType
                + " of " + ((Product)sender).Name
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("savingsapplicationps@gmail.com",
                "xhfelpzkoxwcawvc");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void NotifyRestaurantSoldOutProduct(Product sender, string sellerEmail)
        {

        }
    }
}
