using System.Data.SqlClient;

namespace Camping.Entities.Utility
{
    public class SqlCommandWrapper
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
