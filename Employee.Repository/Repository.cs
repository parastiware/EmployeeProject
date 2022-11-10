
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Employee.Shared;
using System.Data.SqlClient;
namespace Employee.Repository
{
    public class Repository:CommonRepository
    {
        SqlConnection connection;
        ICommonRepository commonRepository;
        public Repository()
        {
           
            commonRepository = new CommonRepository();
            Init();

        }
        private void Init()
        {
            
           
            connection = new SqlConnection(GetConnectionString());


        }
        private void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
            connection.Open();
        }
        private void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        private string GetConnectionString()
        {
            string connectionString = "";
            try
            {
                connectionString = commonRepository.GetConnectionstring();
            }
            catch (Exception ex)
            {
                connectionString = "Data Source=LAPTOP-J78AR7TI\\SQLEXPRESS;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=sa1234";
            }
            finally
            {
                if (string.IsNullOrEmpty(connectionString))
                    connectionString = "Data Source=LAPTOP-J78AR7TI\\SQLEXPRESS;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=sa1234";

            }

            return connectionString;
        }

        public DataTable ExecuteDataTable(string procName, List<SqlParameters> parameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand(procName, connection);
                if (parameters != null)
                    foreach (SqlParameters param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.ParameterValue);
                    }

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            catch (Exception x)
            {
                dt.Rows.Add(9, x.Message);

            }
            finally
            {
                cmd.Dispose();
                closeConnection();

            }

            return dt;
        }

        public CommonDbResponse ParseCommonDbResponse(System.Data.DataTable dt)
        {
            var res = new CommonDbResponse();
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    string Code = dt.Rows[0][0].ToString();
                    string Message = dt.Rows[0][1].ToString();
                    string Extra1 = "", Extra2 = "", Extra3 = "", Extra4 = "", Extra5 = "";
                    if (dt.Columns.Count > 2)
                    {
                        Extra1 = dt.Rows[0][2].ToString();
                    }
                    if (dt.Columns.Count > 3)
                    {
                        Extra2 = dt.Rows[0][3].ToString();
                    }
                    if (dt.Columns.Count > 4)
                    {
                        Extra3 = dt.Rows[0][4].ToString();
                    }
                    if (dt.Columns.Count > 5)
                    {
                        Extra4 = dt.Rows[0][5].ToString();
                    }
                    if (dt.Columns.Count > 6)
                    {
                        Extra5 = dt.Rows[0][6].ToString();
                    }
                    res.SetMessage(Code, Message, Extra1, Extra2, Extra3, Extra4, Extra5, dt);
                    if (dt.Columns.Contains("id"))
                    {
                        res.Id = dt.Rows[0]["id"].ToString();
                    }
                }
            return res;
        }
    }
}
