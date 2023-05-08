using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Battlespire
{
    internal class LoginAndRegistrationDAO : DatabaseAccessObject
    {

        public String RegisterUser(String username_param, String email_param, String password_param)
        {
            List<MySqlParameter> procedure_params = new List<MySqlParameter>();

            MySqlParameter username = new("@username", MySqlDbType.VarChar, 50)
            {
                Value = username_param
            };
            procedure_params.Add(username);

            MySqlParameter email = new("@email", MySqlDbType.VarChar, 100)
            {
                Value = email_param
            };
            procedure_params.Add(email);

            MySqlParameter password = new("@password", MySqlDbType.VarChar, 50)
            {
                Value = password_param
            };
            procedure_params.Add(password);

            DataSet register_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call CreateAccount(@username, @email, @password)", procedure_params.ToArray());

            // change to check for error using regex match. 
            return register_result.Tables[0].Rows[0]["message"].ToString();
        }

        public Boolean LoginUser(String username_param, String password_param)
        {
            List<MySqlParameter> procedure_params = new List<MySqlParameter>();

            MySqlParameter username = new("@username", MySqlDbType.VarChar, 50)
            {
                Value = username_param
            };
            procedure_params.Add(username);

            MySqlParameter password = new("@password", MySqlDbType.VarChar, 50)
            {
                Value = password_param
            };
            procedure_params.Add(password);

            DataSet auth_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call LoginAccount(@username, @password)", procedure_params.ToArray());


            DataRow auth_result_row = auth_result.Tables[0].Rows[0];

            string row_value = auth_result_row.ItemArray[0].ToString();
            if (row_value == "Login succesful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
