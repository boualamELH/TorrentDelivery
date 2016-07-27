using System.Data.Common;
using System.Threading.Tasks;

namespace Basics.SQLITE
{
    public class Connection
    {
        #region fileds & ctors
        public readonly System.Data.SQLite.SQLiteConnection sqliteConnection;
        private const string DB_FILE = "tasks.sqlite";
        private const string PASSWORD = "r@nd0m_Pa55w0rd";
        private static Connection cnx;

        private Connection()
        {
            if (!System.IO.File.Exists(DB_FILE))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(DB_FILE);
            }
            
            sqliteConnection = new System.Data.SQLite.SQLiteConnection($"Data Source={DB_FILE};Version=3;Password={PASSWORD};");
            sqliteConnection.Open();
            var sql = "CREATE TABLE torrents (tag VARCHAR(20), url VARCHAR(255), name VARCHAR(255), added DATETIME)";
            var command = new System.Data.SQLite.SQLiteCommand(sql, sqliteConnection);
            command.ExecuteNonQuery();
        }
        ~Connection()
        {
            sqliteConnection.Close();
            sqliteConnection.Dispose();
        }
        #endregion

        #region public methods
        public static Connection CreateInstance()
        {
            return cnx ?? (cnx = new Connection());
        }
        public async Task<int> ExecuteNonQueryAsync(string sql)
        {
            var sqlCmd = new System.Data.SQLite.SQLiteCommand(sql, sqliteConnection);
            return await sqlCmd.ExecuteNonQueryAsync();
        }
        public int ExecuteNonQuery(string sql)
        {
            var sqlCmd = new System.Data.SQLite.SQLiteCommand(sql, sqliteConnection);
            return sqlCmd.ExecuteNonQuery();
        }
        public async Task<DbDataReader> ExecuteReaderAsync(string sql)
        {
            var sqlCmd = new System.Data.SQLite.SQLiteCommand(sql, sqliteConnection);
            return await sqlCmd.ExecuteReaderAsync();
        }
        #endregion
    }
}