using System.Data;
using System.Data.Odbc;

namespace DataServer.Connections
{
    public class VirtuosoSqlConnection : IDisposable
    {
        OdbcConnection conn;
        //private readonly IConfiguration _config;

        /// <summary>
        /// Constructor to initialize the connection string and open the connection to the virtuoso instance
        /// </summary>
        public VirtuosoSqlConnection(IConfiguration config)
        {
            // create connection by getting the connection string from the appSettings file
            string connString = "Driver={Virtuoso (Open Source)};Host=app.approovia.net:1111;Database=DB;Uid=iam;Pwd=iam;"; // config.GetConnectionString("ConnectionString");
            conn = new OdbcConnection(connString);

            // open the connection to the virtuoso instance
            conn.Open();
        }

        /// <summary>
        /// Execute queries that do not need to return items such as Insert, Update, Detele
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task ExecuteNonQuery(string query)
        {
            try
            {
                var cmd = GetCommand(query);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Execute queries that require items to be returned - select statements
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<OdbcDataReader> ExecuteReader(string query)
        {
            try
            {
                var cmd = GetCommand(query);
                var reader = cmd.ExecuteReader();
                return reader;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Close the connection after object is destroyed
        /// </summary>
        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// Creates and returns the sql command with the query and connection provided
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private OdbcCommand GetCommand(string query)
        {
            OdbcCommand cmd = new OdbcCommand(query, conn);
            return cmd;
        }

        public void Dispose()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
