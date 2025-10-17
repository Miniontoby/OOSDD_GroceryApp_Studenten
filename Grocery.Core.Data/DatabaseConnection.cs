
using Grocery.Core.Data.Helpers;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data
{
    public abstract class DatabaseConnection : IDisposable
    {
        protected SqliteConnection Connection { get; }
        private string DatabaseName { get; }

        public DatabaseConnection()
        {
            DatabaseName = ConnectionHelper.ConnectionStringValue("GroceryAppDb") ?? "";
            //string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dbpath = "Data Source="+ Path.Combine(projectDirectory + DatabaseName);
            Connection = new SqliteConnection(dbpath);
        }

        protected void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open) Connection.Open();
        }

        protected void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed) Connection.Close();
        }

        public void CreateTable(string commandText)
        {
            OpenConnection();
            using SqliteCommand command = Connection.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }

        public void InsertMultipleWithTransaction(List<string> linesToInsert)
        {
            OpenConnection();
            var transaction = Connection.BeginTransaction();

            try
            {
                linesToInsert.ForEach(l => Connection.ExecuteNonQuery(l));
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Run multiple insert queries with the same command but different values with ease!
        /// <code>
        /// InsertMultipleWithTransactionAndParameters(
        ///     "INSERT INTO Users(Name, Email) VALUES (@Name, @Email)",
        ///     [
        ///         [new("Name", "John Doe"), new("Email", "john@doe.com")],
        ///         [new("Name", "Charlie Kirk"), new("Email", "charlie@krik.com")]
        ///     ]
        /// );
        /// </code>
        /// </summary>
        /// <param name="query">The insert query to be executed</param>
        /// <param name="parametersToInsert">The list containing a list of <see cref="SqliteParameter"/>'s</param>
        public void InsertMultipleWithTransactionAndParameters(string query, List<SqliteParameter[]> parametersToInsert)
        {
            OpenConnection();
            var transaction = Connection.BeginTransaction();

            try
            {
                foreach (SqliteParameter[] l in parametersToInsert)
                {
                    using SqliteCommand command = new(query, Connection, transaction);
                    command.Parameters.AddRange(l);
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void Dispose()
        {
            CloseConnection();
            GC.SuppressFinalize(this);
        }
    }
}
