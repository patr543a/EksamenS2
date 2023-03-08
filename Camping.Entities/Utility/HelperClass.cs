using Camping.Entities.DataClasses;
using Camping.Entities.Interfaces;
using System.Data;
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
        /// <param name="type">The type to get</param>
        /// <returns>Object of type T, returns null if T was not found</returns>
        public static IDataClass? GetDataClass<T>(SqlDataReader r, T type)
            where T : IDataClass
            => type switch
            {
                Booking => GetBooking(r),
                Customer => GetCustomer(r),
                Pitch => GetPitch(r),
                _ => null
            };

        /// <summary>
        /// Adds an object of type T to the database
        /// </summary>
        /// <typeparam name="T">Type to add</typeparam>
        /// <param name="sqlCommandWrapper">SqlCommandWrapper used ot access database</param>
        /// <param name="type">The type to add</param>
        /// <param name="o">Object to add to the database</param>
        public static void AddDataClass<T>(SqlCommandWrapper sqlCommandWrapper, T type, object o)
            where T : IDataClass
        {
            switch(type)
            {
                case Booking:
                    AddBooking(sqlCommandWrapper, (Booking)o);
                    break;
                case Customer:
                    AddBooking(sqlCommandWrapper, (Booking)o);
                    break;
                case Pitch:
                    AddBooking(sqlCommandWrapper, (Booking)o);
                    break;
                default:
                    throw new ArgumentException("Ugyldig type", nameof(type));
            }            
        }

        /// <summary>
        /// Get object
        /// </summary>
        /// <param name="r">SqlDataReader used to read from the database</param>
        /// <returns>Object</returns>
        public static Booking GetBooking(SqlDataReader r)
        {
            // Get data
            var c1 = r.GetInt32(0);
            var c2 = r.GetDateTime(1);
            var c3 = r.GetDateTime(2);
            var c4 = r.GetInt32(3);
            var c5 = r.GetInt32(4);
            var c6 = r.GetInt32(5);
            var c7 = r.GetInt32(6);

            // Return new object
            return new(c1, c2, c3, c4, c5, c6, c7);
        }

        /// <summary>
        /// Get object
        /// </summary>
        /// <param name="r">SqlDataReader used to read from the database</param>
        /// <returns>Object</returns>
        public static Customer GetCustomer(SqlDataReader r)
        {
            // Get data
            var c1 = r.GetInt32(0);
            var c2 = r.GetString(1);
            var c3 = r.GetString(2);
            var c4 = r.GetString(3);

            // Return new object
            return new(c1, c2, c3, c4);
        }

        /// <summary>
        /// Get object
        /// </summary>
        /// <param name="r">SqlDataReader used to read from the database</param>
        /// <returns>Object</returns>
        public static Pitch GetPitch(SqlDataReader r)
        {
            // Get data
            var c1 = r.GetInt32(0);
            var c2 = r.GetString(1);

            // Return new object
            return new(c1, c2);
        }

        /// <summary>
        /// Adds object to the database
        /// </summary>
        /// <param name="c">SqlCommandWrapper used to access the database</param>
        /// <param name="booking">Object to add</param>
        public static void AddBooking(SqlCommandWrapper c, Booking v)
        {
            c.Command.Parameters.Add("@C1", SqlDbType.DateTime);
            c.Command.Parameters.Add("@C2", SqlDbType.DateTime);
            c.Command.Parameters.Add("@C3", SqlDbType.Int);
            c.Command.Parameters.Add("@C4", SqlDbType.Int);
            c.Command.Parameters.Add("@C5", SqlDbType.Int);
            c.Command.Parameters.Add("@C6", SqlDbType.Int);

            c.Command.Parameters["@C1"].Value = v.StartDate;
            c.Command.Parameters["@C2"].Value = v.EndDate;
            c.Command.Parameters["@C3"].Value = v.Adults;
            c.Command.Parameters["@C4"].Value = v.Children;
            c.Command.Parameters["@C5"].Value = v.PitchId;
            c.Command.Parameters["@C6"].Value = v.CustomerId;

            c.Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds object to the database
        /// </summary>
        /// <param name="c">SqlCommandWrapper used to access the database</param>
        /// <param name="booking">Object to add</param>
        public static void AddCustomer(SqlCommandWrapper c, Customer v)
        {
            c.Command.Parameters.Add("@C1", SqlDbType.NVarChar);
            c.Command.Parameters.Add("@C2", SqlDbType.NVarChar);
            c.Command.Parameters.Add("@C3", SqlDbType.NVarChar);

            c.Command.Parameters["@C1"].Value = v.FullName;
            c.Command.Parameters["@C2"].Value = v.EmailAddress;
            c.Command.Parameters["@C3"].Value = v.PhoneNumber;

            c.Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds object to the database
        /// </summary>
        /// <param name="c">SqlCommandWrapper used to access the database</param>
        /// <param name="booking">Object to add</param>
        public static void AddPitch(SqlCommandWrapper c, Pitch v)
        {
            c.Command.Parameters.Add("@C1", SqlDbType.NVarChar);

            c.Command.Parameters["@C1"].Value = v.PitchName;

            c.Command.ExecuteNonQuery();
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
