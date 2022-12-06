using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public void NotifyRestaurantSoldProduct(Object? sender, ProductSoldEventArgs args)
        {
            var emailForBuyer = new MimeMessage();
            emailForBuyer.From.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            emailForBuyer.To.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            emailForBuyer.Subject = "Product Sold";
            emailForBuyer.Body = new TextPart(TextFormat.Html) { Text =
                "You have sold " + args.Amount + " " + ((Product)sender).AmountType
                + " of " + ((Product)sender).Name
            };

            /*var emailForSeller = new MimeMessage();
            emailForSeller.From.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            emailForSeller.To.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            emailForSeller.Subject = "Product Sold";
            emailForSeller.Body = new TextPart(TextFormat.Html)
            {
                Text =
                "You have sold " + args.Amount + " " + ((Product)sender).AmountType
                + " of " + ((Product)sender).Name
            };*/

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("savingsapplicationps@gmail.com",
                "xhfelpzkoxwcawvc");
            smtp.Send(emailForBuyer);
            smtp.Disconnect(true);
        }

        public void NotifyRestaurantSoldOutProduct(Object? sender, ProductSoldOutEventArgs args)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            email.To.Add(MailboxAddress.Parse("savingsapplicationps@gmail.com"));
            email.Subject = "Product Sold";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "Your product" + ((Product)sender).Name + " has sold out"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("savingsapplicationps@gmail.com",
                "xhfelpzkoxwcawvc");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
