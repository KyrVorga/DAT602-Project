using DAT602_Project;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class GameDAO : DatabaseAccessObject
    {

        public List<String> GetChat()
        {

            DataSet chat_logs = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetChatHistory();");

            //register_result.Tables[0].Rows[0]["message"].ToString();
            var chat_messages = new List<String>();
            foreach (DataRow row in chat_logs.Tables[0].Rows)
            {
                chat_messages.Add(row[0].ToString());
            }

            return chat_messages;
        }

        public List<String> GetLeaderboard()
        {
            DataSet leaderboard_raw = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetLeaderboard();");

            var leaderboard_list = new List<String>();
            foreach (DataRow row in leaderboard_raw.Tables[0].Rows)
            {
                leaderboard_list.Add(row[0].ToString());
            }

            return leaderboard_list;
        }


        public Boolean checkIsAdmin(String username_param)
        {
            List<MySqlParameter> procedure_params = new List<MySqlParameter>();
            MySqlParameter username = new("@username", MySqlDbType.VarChar, 50)
            {
                Value = username_param
            };
            procedure_params.Add(username);

            DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call IsAdminAccount(@username)", procedure_params.ToArray());

            DataRow result_row = query_result.Tables[0].Rows[0];
            string value = result_row.ItemArray[0].ToString();

            if (value == "True")
            {
                return true;
            }
            else return false;
        }

        public List<Tile> GetTilesByPlayer(int player_id)
        {

            List<MySqlParameter> procedure_params = new List<MySqlParameter>();
            MySqlParameter _player_id = new("@player_id", MySqlDbType.Int32)
            {
                Value = player_id
            };
            MySqlParameter _viewport_width = new("@viewport_width", MySqlDbType.Int32)
            {
                Value = 10
            };
            MySqlParameter _viewport_height = new("@viewport_height", MySqlDbType.Int32)
            {
                Value = 10
            };
            procedure_params.Add(_player_id);
            procedure_params.Add(_viewport_width);
            procedure_params.Add(_viewport_height);

            DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetTilesByPlayer(@player_id, @viewport_width, @viewport_height)", procedure_params.ToArray());
            
            var tile_list = new List<Tile>();
            foreach (DataRow row in query_result.Tables[0].Rows)
            {
                var newTile = new Tile((int)row[0], (int)row[1], (int)row[2], (string)row[3]);
                tile_list.Add(newTile);
                //Console.WriteLine(newTile.ToString());
            }

            return tile_list;
        }
    }
}
