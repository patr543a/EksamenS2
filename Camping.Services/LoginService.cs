using System.Collections;

namespace Camping.Services
{
    /// <summary>
    /// Handles Login information
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// List of all valid logins
        /// </summary>
        private static readonly Hashtable _logins = new()
        {
            { "", "" },
        };

        /// <summary>
        /// Verifies that the login is correct
        /// </summary>
        /// <param name="username">Username to check</param>
        /// <param name="password">Password to check</param>
        /// <returns>Returns true if valid otherwise false</returns>
        public static bool VerifyLogin(string username, string password)
        {
            // If username is not in Hastable return false
            if (!_logins.ContainsKey(username)) 
                return false;

            // Get HashCode of password and HashCode from _logins
            var login = _logins[username]!.GetHashCode();
            var pass = password.GetHashCode();

            // Checks if HashCodes are valid if so return true 
            if (login == pass)
                return true;

            // Return false by default
            return false;
        }

        /// <summary>
        /// Throws if login was invalid and doesn't if it is valid
        /// </summary>
        /// <param name="username">Username to check</param>
        /// <param name="password">Password to check</param>
        /// <exception cref="Exception"></exception>
        public static void Verify(string username, string password)
        {
            // If username is not in Hastable throw
            if (!_logins.ContainsKey(username))
                throw new Exception("Ugyldig login");

            // Get HashCode of password and HashCode from _logins
            var login = _logins[username]!.GetHashCode();
            var pass = password.GetHashCode();

            // If HashCodes doesn't match throw
            if (login != pass)
                throw new Exception("Ugyldig login");
        }
    }
}
