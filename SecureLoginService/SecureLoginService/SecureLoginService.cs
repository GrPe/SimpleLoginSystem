using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureLoginService.Model;

namespace SecureLoginService
{
    
    public class SecureLoginService : ISecureLoginService
    {
        string connectionString = "Data Source=GREGORY;initial catalog = Secure; Integrated Security = SSPI;";

        public Response AddUser(User user)
        {
            string selectCommandText = "SELECT * FROM Users WHERE Login = @Login;";
            string insertCommandText = "INSERT INTO Users VALUES (@Login,@Password);";

            Response result = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(selectCommandText, sqlConnection);
                    command.Parameters.Add("@Login", SqlDbType.NVarChar).Value = user.Login;
                    sqlConnection.Open();

                    reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        SqlCommand insertCommand = new SqlCommand(insertCommandText, sqlConnection);
                        insertCommand.Parameters.Add("@Login",SqlDbType.NVarChar).Value = user.Login;
                        insertCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                        insertCommand.ExecuteNonQuery();
                        result = new Response(Types.AccountCreated);
                    }
                    else
                    {
                        result = new Response(Types.AccountExist);
                    }
                }
                catch(Exception ex)
                {
                    result = new Response(Types.Error,"Problem with connection with db");
                }
                finally
                {
                    if(reader != null)
                        reader.Close();
                    if(sqlConnection != null)
                        sqlConnection.Close();
                }
                return result;
            }
        }

        public Response Login(User user)
        {
            Response result = null;
            User sqlCommandResult = null;

            string selectCommandText = "SELECT * FROM Users WHERE Login = @Login;";

            using (SqlConnection sqlConnnection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(selectCommandText, sqlConnnection);
                    command.Parameters.Add("@Login", SqlDbType.NVarChar).Value = user.Login;
                    
                    sqlConnnection.Open();

                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        sqlCommandResult = new User(reader[0].ToString(), reader[1].ToString());
                        if(Modules.Crypto.Validate(user.Password, sqlCommandResult.Password))
                        {
                            result = new Response(Types.CorrectLogin);
                        }
                        else
                        {
                            result = new Response(Types.IncorrectLogin);
                        }

                    }
                    else
                    {
                        result = new Response(Types.IncorrectLogin);
                    }
                }
                catch (Exception ex)
                {
                    result = new Response(Types.Error,"Problem with connection with db");
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    if (sqlConnnection != null)
                        sqlConnnection.Close();
                }
                return result;
            }
        }
    }
}