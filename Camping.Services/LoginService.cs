using System.Collections;

namespace Camping.Services
{
    /// <summary>
    /// Handles Login information
    /// </summary>
    public static class LoginService
    {
        private static readonly Hashtable _logins = new()
        {
            { "", "" },
        };

        public static bool VerifyLogin(string username, string password)
        {
            if (!_logins.ContainsKey(username)) return false;

            var login = _logins[username]!.GetHashCode();
            var pass = password.GetHashCode();

            if (login == pass)
                return true;

            return false;
        }

        public static void Verify(string username, string password)
        {
            if (!_logins.ContainsKey(username))
                throw new Exception("Ugyldig login");

            var login = _logins[username]!.GetHashCode();
            var pass = password.GetHashCode();

            if (login != pass)
                throw new Exception("Ugyldig login");
        }
    }
}
