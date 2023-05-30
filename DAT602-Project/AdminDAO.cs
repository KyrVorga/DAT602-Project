using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        public void ResetGame()
        {
            MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call ResetGame();");
        }

        public void DeleteAccount()
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@accountId",
                        MySqlDbType = MySqlDbType.VarChar,
                        Value = Game.CurrentPlayer.AccountId
                    }

                };

                MySqlHelper.ExecuteDataset(MySqlConnection, "call DeleteAccount(@accountId)", procedure_params.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        internal void MoveToHome(string username)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@username",
                        MySqlDbType = MySqlDbType.VarChar,
                        Value = username
                    }

                };

                MySqlHelper.ExecuteDataset(MySqlConnection, "call MovePlayerHome(@username)", procedure_params.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        internal void RegenerateMap()
        {
            MySqlHelper.ExecuteDataset(MySqlConnection, "call RegenerateMap()");
            Game.Mainform.ReloadGame();
        }
    }
}
