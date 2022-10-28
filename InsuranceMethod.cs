using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using InTakeProData.Models;
using InTakeProData;

namespace InTakePro
{
    public class InsuranceMethod : ControllerBase
    {

        private readonly IConfiguration _configuration;
        InsuranceModel inm = new InsuranceModel();
        SqlConnection con;



        public InsuranceMethod(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:IntakeDB"));
        }

        [FunctionName("GetInsuranceDetails")]
        public async Task<object> GetInsuranceDetails(
     [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Get), Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                InsuranceData fhd = new InsuranceData(_configuration);
                string dt = fhd.GetInsurance();
                return Ok(dt);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkResult();
        }
        [FunctionName("PostInsuranceDetails")]
        public async Task<IActionResult> PostInsurancedetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Post), Route = null)] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<InsuranceModel>(requestBody);
            try
            {
                InsuranceData pcd = new InsuranceData(_configuration);
                string dt = pcd.PostInsurance(input);
                return Ok(dt);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkResult();

        }
        [FunctionName("UpdateInsuranceDetails")]
        public async Task<IActionResult> UpdateInsuranceDetails(
          [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Put), Route = null)] HttpRequest req, ILogger log)
        {
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<InsuranceModel>(requestBody);

                try
                {
                    InsuranceData pda = new InsuranceData(_configuration);
                    string dt = pda.PutInsurance(input);
                    return Ok(dt);
                }
                catch (Exception e)
                {
                    log.LogError(e.ToString());
                    return new BadRequestResult();
                }
                return new OkResult();



            }
        }
        [FunctionName("DeleteInsuranceDetails")]
        public async Task<IActionResult> DeleteDetails(
           [HttpTrigger(AuthorizationLevel.Anonymous, nameof(HttpMethods.Delete), Route = null)] HttpRequest req, ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<InsuranceModel>(requestBody);

            try
            {

                InsuranceData pda = new InsuranceData(_configuration);
                string dt = pda.DeleteInsurance(input);
                return Ok(dt);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();

        }
    }
}
