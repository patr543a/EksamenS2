using Camping.Entities.DataClasses;
using Camping.Entities.Interfaces;
using System.Data.SqlClient;

namespace Camping.Entities.Utility
{
    /// <summary>
    /// Methods for Repository class
    /// </summary>
    public static class HelperClass
    {
        /// <summary>
        /// Reads and returns a new object of type T from the database
        /// </summary>
        /// <typeparam name="T">Type to read</typeparam>
        /// <param name="r">SqlDataReader used to read from the database</param>
        /// <param name="type">The type ot get</param>
        /// <returns>Object of type T, returns null if T was not found</returns>
        public static IDataClass? GetDataClass<T>(SqlDataReader r, T type)
            where T : IDataClass
            => type switch
            {
                Pitch => GetPitch(r),
                _ => null
            };

        /// <summary>
        /// Get object
        /// </summary>
        /// <param name="r">SqlDataReader used to read from the database</param>
        /// <returns>Object</returns>
        public static Pitch GetPitch(SqlDataReader r)
        {
            // Get data
            var id = r.GetInt32(0);
            var name = r.GetString(1);

            // Return new object
            return new(id, name);
        }

        /// <summary>
        /// Gets a SqlCommandWrapper with the given command and connection
        /// </summary>
        /// <param name="query">Command to use</param>
        /// <param name="sqlConnection">Connection to use</param>
        /// <returns>New SqlCommandWrapper</returns>
        public static SqlCommandWrapper GetSqlCommand(string query, SqlConnection sqlConnection)
            => new(query, sqlConnection);
    }
}
