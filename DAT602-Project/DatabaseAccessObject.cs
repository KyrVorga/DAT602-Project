using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Battlespire
{
    internal class DatabaseAccessObject
    {
        private static string ConnectionString
        {
            get { return "Server=localhost;Port=3306;Database=battlespire;Uid=root;password=Alicization44;"; }

        }

        private static MySqlConnection? _mySqlConnection = null;
        protected static MySqlConnection MySqlConnection
        {
            get
            {
                if (_mySqlConnection == null)
                {
                    _mySqlConnection = new MySqlConnection(ConnectionString);
                }

                return _mySqlConnection;

            }
        }
    }
}
