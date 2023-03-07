using Camping.Entities.DataClasses;
using Camping.Entities.Interfaces;
using Camping.Services;
using System.Data.SqlClient;

namespace Camping.Repository
{
    public class Repository
    {
        private static readonly string _connectionString = 
            @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CampingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly SqlConnection _sqlConnection;

        public Repository(string username, string password)
        {
            LoginService.Verify(username, password);

            _sqlConnection = new SqlConnection(_connectionString);
        }

        public List<IDataClass> GetList<T>(string username, string password, T type)
            where T : IDataClass
        {
            LoginService.Verify(username, password);

            var query = type switch
            {
                Pitch => "select * from Pitches",
                _ => ""
            };

            var cmd = GetSqlCommand(query);
            var r = cmd.Command.ExecuteReader();
            List<IDataClass> list = new();

            while (r.Read())
            {
                var data = GetDataClass(r, type);

                if (data is null)
                    throw new Exception("Noget gik galt");

                list.Add(data);
            }

            return list;
        }

        private static IDataClass? GetDataClass<T>(SqlDataReader r, T type)
            where T : IDataClass
            => type switch
            {
                Pitch => GetPitch(r),
                _ => null
            };

        private static Pitch GetPitch(SqlDataReader r)
        {
            var id = r.GetInt32(0);
            var name = r.GetString(1);

            return new(id, name);
        }

        private SqlCommandWrapper GetSqlCommand(string query) 
            => new(query, _sqlConnection);

        ~Repository()
        {
            _sqlConnection.Close();
            _sqlConnection.Dispose();
        }
    }

    internal class SqlCommandWrapper
    {
        private readonly SqlCommand _command;

        public SqlCommand Command => _command;

        public SqlCommandWrapper(string query, SqlConnection connection)
        {
            _command = new SqlCommand(query, connection);

            _command.Connection.Open();
        }

        ~SqlCommandWrapper() 
            => _command.Connection.Close();
    }
}
