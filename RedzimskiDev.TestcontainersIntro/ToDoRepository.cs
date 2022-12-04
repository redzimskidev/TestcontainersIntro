
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;

namespace RedzimskiDev.TestcontainersIntro
{
    public class ToDoRepository
    {
        private readonly string _connectionString;

        public ToDoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(ToDo toDo)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            connection.Insert(toDo);
        }

        public ToDo Get(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection.Get<ToDo>(id);
        }
    }
}
