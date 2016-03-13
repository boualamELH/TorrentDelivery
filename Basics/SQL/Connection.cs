using System.Data.SQLite;
using System.Threading.Tasks;
using System.Data.Common;

namespace Basics.SQL
{
    public class Connection
    {
        #region fileds & ctors
        private const string dbFile = "tasksDB.sqlite";
        private const string password = "r@nd0m_Pa55w0rd";
        private readonly SQLiteConnection dbConnection;
        private static Connection cnx = null;
        private Connection()
        {
            if (!System.IO.File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }
            dbConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;Password={1}", dbFile, password));
            dbConnection.Open();
        }
        ~Connection()
        {
            dbConnection.Close();
        }
        #endregion

        #region public methods
        public static Connection GetConnection()
        {
            return cnx ?? (cnx = new Connection());
        }
        public async Task<int> ExecuteNonQueryAsync(string sql)
        {
            var sqlCmd = new SQLiteCommand(sql, dbConnection);
            return await sqlCmd.ExecuteNonQueryAsync();
        }
        public int ExecuteNonQuery(string sql)
        {
            var sqlCmd = new SQLiteCommand(sql, dbConnection);
            return sqlCmd.ExecuteNonQuery();
        }
        public async Task<DbDataReader> ExecuteReaderAsync(string sql)
        {
            var sqlCmd = new SQLiteCommand(sql, dbConnection);
            return await sqlCmd.ExecuteReaderAsync();
        }
        #endregion
    }
}