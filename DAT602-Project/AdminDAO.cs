using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class AdminDAO : DatabaseAccessObject
    {
        public List<String> GetAllPlayers()
        {

            DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetAllPlayers();");

            var player_list = new List<String>();
            foreach (DataRow row in query_result.Tables[0].Rows)
            {
                player_list.Add(row[0].ToString());
            }

            return player_list;
        }
    }
}
