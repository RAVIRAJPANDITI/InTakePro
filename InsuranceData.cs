
using InTakeProData.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTakeProData
{
    public class InsuranceData
    {
        private readonly IConfiguration _configuration;
        public string _connectionString = null;
        SqlConnection con;
        public InsuranceData(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionStrings:InTakeDB").Value;
        }
        public string GetInsurance()
        {
            DataTable dt = new DataTable();
            string query = ("spGetInsuranceDetails");
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }
            return JsonConvert.SerializeObject(dt);
        }
        public string PostInsurance(InsuranceModel input)
        {
            string msg = "";
            if (input != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spInsertInsuranceDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@InsuranceCompany", input.InsuranceCompany));
                    cmd.Parameters.Add(new SqlParameter("@Memberid", input.Memberid));
                    cmd.Parameters.Add(new SqlParameter("@Group_Number", input.Group_Number));
                    cmd.Parameters.Add(new SqlParameter("@Policy_Holder", input.Policy_Holder));
                    cmd.Parameters.Add(new SqlParameter("@Updated_by", input.Updated_by));
                    cmd.Parameters.Add(new SqlParameter("@Created_Date", input.Created_Date));
                    cmd.Parameters.Add(new SqlParameter("@Updated_Date", input.Updated_Date));

                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        msg = "Data has been inserted";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
            }
            return msg;
        }
        public string PutInsurance(InsuranceModel input)
        {

            string msg = "";
            if (input != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UpdateInsuranceDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", input.ID));
                    cmd.Parameters.Add(new SqlParameter("@InsuranceCompany", input.InsuranceCompany));
                    cmd.Parameters.Add(new SqlParameter("@Memberid", input.Memberid));
                    cmd.Parameters.Add(new SqlParameter("@Group_Number", input.Group_Number));
                    cmd.Parameters.Add(new SqlParameter("@Policy_Holder", input.Policy_Holder));
                    cmd.Parameters.Add(new SqlParameter("@Updated_by", input.Updated_by));
                    cmd.Parameters.Add(new SqlParameter("@Created_Date", input.Created_Date));
                    cmd.Parameters.Add(new SqlParameter("@Updated_Date", input.Updated_Date));
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        msg = "Data has been Updated";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
            }
            return msg;
        }

        public string DeleteInsurance(InsuranceModel input)
        {
            string msg = "";

            {


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeteleInsuranceDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", input.ID));
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        msg = "Data has been Deleted";
                    }
                    else
                    {
                        msg = "Error";
                    }
                }
            }

            return msg;
        }
    }
}
