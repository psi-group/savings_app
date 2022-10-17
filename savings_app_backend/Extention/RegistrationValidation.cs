using System.Text.RegularExpressions;

namespace savings_app_backend.Extention
{
    public static class RegistrationValidation
    {
        public static bool IsValidEmail(this string Email)
        {
            /* regex */
            return Regex.IsMatch(Email, @"^[A-Za-z0-9.]+@[A-Za-z]+\.[a-z]{3}$");

        }

        public static bool IsValidPassword(this string Password)
        {
            /* regex */
            return Regex.IsMatch(Password, @"\.*$");
        }
    }
}
