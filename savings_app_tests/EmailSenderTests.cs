/*using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using Microsoft.EntityFrameworkCore.Diagnostics;
using savings_app_backend.EmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using savings_app_backend.Models.Entities;
using NSubstitute;
using NuGet.Protocol.Plugins;

namespace savings_app_tests
{
    public class EmailSenderTests
    {
        private readonly EmailSender _sut;

        public EmailSenderTests()
        {
            _sut = new EmailSender();
        }

        [Fact]
        public void NotifyRestaurantSoldProduct_ShouldSendEmail()
        {
            //Arrange

            int amount = 1;

            var userAuth = new UserAuth();
            userAuth.Email = "savingsapplicationps@gmail.com";

            var restaurant = new Restaurant();
            restaurant.UserAuth = userAuth;

            var product = new Product();
            product.Name = "product";
            product.AmountType = AmountType.unit;
            product.AmountOfUnits = 2;
            product.Restaurant = restaurant;


            var emailSender = new EmailSender();

            product.ProductSold += emailSender.NotifyRestaurantSoldProduct;
            
            //Act

            product.ReduceAmount(amount);

            //Assert

            List<string> bodies = new List<string>();
            List<string> subjects = new List<string>();

            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                //client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("savingsapplicationps@gmail.com", "xhfelpzkoxwcawvc");


                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                var results = inbox.Search(SearchOptions.All, SearchQuery.NotSeen);
                foreach (var uniqueId in results.UniqueIds)
                {
                    var message = inbox.GetMessage(uniqueId);

                    subjects.Add(message.Subject);
                    bodies.Add(message.HtmlBody);

                    //Mark message as read
                    //inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                }

                client.Disconnect(true);
            }


            Assert.Equal("Product Sold", subjects.Last());
            Assert.Equal("You have sold " + amount + " " + product.AmountType + " of " + product.Name, bodies.Last().Substring(0, bodies.Last().Length - 2));

        }
    }
}
*/