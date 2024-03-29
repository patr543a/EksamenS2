﻿using Camping.Entities.DataClasses;
using Camping.Entities.Interfaces;
using Camping.Entities.Utility;
using Camping.Services;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Camping.Repository
{
    /// <summary>
    /// Handles SQL database
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// String used to connect to the database
        /// </summary>
        private static readonly string _connectionString = 
            @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CampingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Connection to the database
        /// </summary>
        private readonly SqlConnection _sqlConnection;

        /// <summary>
        /// Creates a new Repository, throws exception if username and or password is invalid
        /// </summary>
        /// <param name="username">Username used to validate access</param>
        /// <param name="password">Password used to validate access</param>
        /// <exception cref="Exception"></exception>
        public Repository(string username, string password)
        {
            // Validate login
            LoginService.Verify(username, password);

            // Setup connection
            _sqlConnection = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Gets a list of the given type from the database
        /// </summary>
        /// <typeparam name="T">IDataClass type to get a list of</typeparam>
        /// <param name="username">Username used to validate access</param>
        /// <param name="password">Password used to validate access</param>
        /// <param name="type">The type of IDataClass to get from the database</param>
        /// <returns>List of the given type from the database</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public List<T> GetList<T>(string username, string password, T type)
            where T : IDataClass
        {
            // Validate login
            LoginService.Verify(username, password);
            
            // Get sql command string to use
            var query = type switch
            {
                Booking => "select * from Bookings",
                Customer => "select * from Customers",
                Pitch => "select * from Pitches",
                _ => string.Empty
            };

            // Throw if command string is empty
            if (query == string.Empty)
                throw new ArgumentException("The given type was no valid", nameof(type));

            // Get SqlCommand and get SqlDataReader
            var cmd = HelperClass.GetSqlCommand(query, _sqlConnection);
            var r = cmd.Command.ExecuteReader();

            // Output list
            List<T> list = new();

            // Loop over data
            while (r.Read())
            {
                // Get IDataClass
                var data = HelperClass.GetDataClass(r, type);

                // If data was null throw
                if (data is null)
                    throw new Exception("Noget gik galt");

                // Add data to the list
                list.Add((T)data);
            }

            // Dispose cmd
            cmd.Dispose();

            // Return list
            return list;
        }

        /// <summary>
        /// Adds the given object of type T to the database
        /// </summary>
        /// <typeparam name="T">Type to add</typeparam>
        /// <param name="username">Username used to validate access</param>
        /// <param name="password">Password used to validate access</param>
        /// <param name="type">The type of IDataClass to add to the database</param>
        /// <param name="o">Object to add to the datatbase</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddDataClass<T>(string username, string password, T type, object o)
            where T : IDataClass
        {
            // Validate login
            LoginService.Verify(username, password);

            // Get sql command string to use
            var query = type switch
            {
                Booking => "insert into Bookings values (@C1, @C2, @C3, @C4, @C5, @C6, @C7)",
                Customer => "insert into Customers values (@C1, @C2, @C3, @C4)",
                Pitch => "insert into Pitches values (@C1, @C2)",
                _ => string.Empty
            };

            // Throw if command string is empty
            if (query == string.Empty)
                throw new ArgumentException("The given type was no valid", nameof(type));

            // Get SqlCommand and get SqlDataReader
            var cmd = HelperClass.GetSqlCommand(query, _sqlConnection);

            HelperClass.AddDataClass(cmd, type, o);

            // Dispose cmd
            cmd.Dispose();
        }

        /// <summary>
        /// Releases resources used by this class when object is removed
        /// </summary>
        ~Repository()
        {
            // Close and dispose of connection when object is removed
            _sqlConnection.Close();
            _sqlConnection.Dispose();
        }
    }
}
