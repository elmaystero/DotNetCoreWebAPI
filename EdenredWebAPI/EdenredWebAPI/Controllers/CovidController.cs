using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EdenredWebAPI.Models;

namespace EdenredWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CovidController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetGlobalSummary")]
        public async Task<ActionResult<CovidData>> GetGlobalSummary()
        {

            var serviceUrl = string.Concat(this._configuration.GetValue<string>("CovidService"), "/summary");

            CovidData covidData = new CovidData();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(serviceUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    covidData = JsonConvert.DeserializeObject<CovidData>(apiResponse);
                }
            }
            return covidData;
        }
        [HttpGet("GetUaeSummary")]
        public async Task<ActionResult<List<CountryHistory>>> GetUaeSummary()
        {

            var serviceUrl = string.Concat(this._configuration.GetValue<string>("CovidService"), "/country/united-arab-emirates");

            List<CountryHistory> covidData = new List<CountryHistory>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(serviceUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    covidData = JsonConvert.DeserializeObject<List<CountryHistory>>(apiResponse);
                }
            }
            return covidData;
        }
    }
}
