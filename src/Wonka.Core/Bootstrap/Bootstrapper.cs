using System.Data.SQLite;
using Dapper;
namespace Wonka.Core.Bootstrap
{
    public class Bootstrapper
    {
        private readonly Configuration _configuration;

        public Bootstrapper(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void CreateDb()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query = @"CREATE TABLE Init (Id INT NOT NULL)";

                connection.Open();
                connection.Execute(query);
            }
        }

        protected string ConnectionString { get { return string.Format("Data Source={0}", _configuration.Database);  } }
    }
}