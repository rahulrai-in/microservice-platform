using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservices.TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HookBinController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly ILogger<HookBinController> _logger;

        public HookBinController(IHttpClientFactory httpClientFactory, ILogger<HookBinController> logger)
        {
            _client = httpClientFactory.CreateClient();
            _logger = logger;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            Activity.Current?.AddEvent(new ActivityEvent("Sending request to HookBin"));
            return await _client.PostAsync("https://hookb.in/ggp0VpGyLYTG7Voo7Veg", new StringContent("Hello"));
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("GET endpoint invoked");
            return Ok();
        }
    }
}