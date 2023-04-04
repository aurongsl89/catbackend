using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class catController : ControllerBase
    {

        private readonly ILogger<catController> _logger;
        private readonly HttpClient _httpClient;
        public catController(ILogger<catController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://catfact.ninja/");

        }

        [HttpGet(Name = "GetRandCatFact")]
        public async Task<cat> Get()
        {
            var response = new cat();
            var result = await this._httpClient.GetAsync("fact");
            if (!result.IsSuccessStatusCode) {
                throw new
                     Exception("fail to get data from third party api");
            }
            response = JsonConvert.DeserializeObject<cat>(await result.Content.ReadAsStringAsync());
            return response; 
        }
    }
}