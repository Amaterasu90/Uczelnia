using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MuliThreadDatabaseOperator
{
    /// <summary>
    /// Creates and manages connection with mysql database.
    /// </summary>
    public sealed class DatabaseConnectionManager : IDisposable
    {
        #region Fields
        private MySqlConnection connection;
        private string login;
        private string password; // przydało by sie uzyć kryptografii
        private string database; //name of database
        private IPAddress ip_serwer;
        private bool correctly_open;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes new instance of DatabaseConnectionManager class.
        /// </summary>
        /// <param name="login">Name of user in mysql database.</param>
        /// <param name="password">Password using to login on database.</param>
        /// <param name="db_name">Name of datbase on serwer mysql.</param>
        /// <param name="ip">IP adress of mysql serwer.</param>
        public DatabaseConnectionManager( IPAddress ip,string login,string password,string db_name)
        {
            this.login = login;
            this.password = password;
            this.database = db_name;
            this.ip_serwer = ip;
            string s = "Server=" + ip_serwer.ToString() + ";Database=" + db_name + ";Uid=" + login + ";Pwd=" + password + ";";
            connection = new MySqlConnection("Server=" + ip_serwer.ToString() + ";Database=" + db_name + ";Uid=" + login + ";Pwd=" + password + ";");
        }
        #endregion
        #region methods
        /// <summary>
        /// Method open the connection with myslq database.
        /// <exception cref="MySql.Data.MySqlClient.MySqlException">When is problem with connection with database.</exception>
        /// <exception cref="System.InvalidOperationException">When somone try connect with database when connection is open.</exception>
        /// </summary>
        public void Open()
        {
            try
            {
                connection.Open();
                Console.WriteLine("was connected...");
                correctly_open = true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Problem z połączeniem z bazą danych. Log: " + ex.Message);
                connection.Close();
                connection.ConnectionString = "Server=localhost;Database=mysql;Uid=root;Pwd=t7ggpxfy;";
                this.Open();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Połączenie nie zostało zamknięte. Log: " + ex.Message);
                Close();
            }
            correctly_open = false;
        }
        /// <summary>
        /// Method close the connection with mysql database.
        /// </summary>
        public void Close()
        {
            connection.Close();
            Console.WriteLine("was disconnected...");
        }
        #endregion
        #region Accessors
        /// <summary>
        /// Gets whether the connection with database is open correctly.
        /// </summary>
        public bool IsConnectionGood
        {
            get
            {
                return correctly_open;
            }
        }
        #endregion
        #region Interfaces
        /// <summary>
        /// Iplements the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            connection.Close();
            login = null;
            password = null;
            database = null;
            ip_serwer = null;
            correctly_open = false;
        }
        #endregion
        #region Destruktory
        ~DatabaseConnectionManager()
        {
            connection.Close();            login = null;
            password = null;
            database = null;
            ip_serwer = null;
            correctly_open = false;
        }
        #endregion
    }
}
