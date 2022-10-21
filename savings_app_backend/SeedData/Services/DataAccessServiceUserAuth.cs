using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceUserAuth
    {
        public DataAccessServiceUserAuth(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "data" + "\\" +  "users.json"; }
        }

        public IEnumerable<UserAuth> GetUserAuth()
        {
            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuth[]>(jsonFile);
        }

        /*public bool DoesUserAlreadyExists(User userToRegister)
        {
            var users = GetUsers();

            foreach (User user in users)
            {
                if (user.Email == userToRegister.Email)
                {
                    return true;
                }
            }
            return false;
        }
        public bool RegisterUser(User userToRegister)
        {

            userToRegister.Id = Guid.NewGuid();


            IEnumerable<User> users = GetUsers();
            users = users.Concat(new[] { userToRegister });

            var usersJson = Newtonsoft.Json.JsonConvert.SerializeObject(users, Formatting.Indented);

            File.WriteAllText(JsonFileName, usersJson.ToString());

            return true;
        }*/

        /*public User GetById(Guid id)
        {
            var users = GetUsers();

            return users.SingleOrDefault(r => r.Id == id);
        }*/
    }
}