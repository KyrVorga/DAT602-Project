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

        public string RegisterUser(string username_param, string email_param, string password_param)
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
                        Value = username_param
                    },
                    new()
                    {
                        ParameterName = "@email",
                        MySqlDbType = MySqlDbType.VarChar,
                        Size = 100,
                        Value = email_param
                    },
                    new()
                    {
                        ParameterName = "@password",
                        MySqlDbType = MySqlDbType.VarChar,
                        Size = 50,
                        Value = password_param
                    }

                };

                foreach ( var param in procedure_params.ToArray() )
                {
                    Console.WriteLine(param.Value);
                }

                DataSet register_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call CreateAccount(@username, @email, @password)", procedure_params.ToArray());


                return register_result.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(string username_param, string password_param)
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
                        Value = username_param
                    },
                    new()
                    {
                        ParameterName = "@password",
                        MySqlDbType = MySqlDbType.VarChar,
                        Size = 50,
                        Value = password_param
                    }

                };

                DataSet auth_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call LoginAccount(@username, @password)", procedure_params.ToArray());


                DataRow auth_result_row = auth_result.Tables[0].Rows[0];

                return auth_result_row.ItemArray[0].ToString();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
