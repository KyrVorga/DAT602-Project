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
        public List<string> GetAllPlayers()
        {

            try
            {
                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetAllPlayers();");

                var player_list = new List<string>();
                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                    player_list.Add(row[0].ToString());
                }

                return player_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        MySqlDbType = MySqlDbType.Int32,
                        Value = Game.CurrentPlayer.AccountId
                    }

                };

                MySqlHelper.ExecuteDataset(MySqlConnection, "call DeleteAccount(@accountId)", procedure_params.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
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
                        Size = 50,
                        Value = username
                    }

                };

                MySqlHelper.ExecuteDataset(MySqlConnection, "call MovePlayerHome(@username)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void RegenerateMap()
        {
            try
            {
                MySqlHelper.ExecuteDataset(MySqlConnection, "call RegenerateMap()");
                Game.Mainform.ReloadGame();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
