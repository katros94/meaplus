using Meaplus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meaplus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeaplusController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MeaplusController(IConfiguration configuration, IHttpClientFactory httpClientFactory) {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async void Post([FromBody] Message message)
        {
            string accessToken = _configuration["Sefos:BearerToken"];

            var sefosParticipants = new List<SefosParticipant>();
            sefosParticipants.Add(new SefosParticipant { Email = _configuration["Sefos:Email"] });
            
            var externalParticipants = new List<ExternalParticipant>();
            externalParticipants.Add(new ExternalParticipant { Email = message.External_participants?.First().Email, Language = null, AuthenticationIdentifier = null, AuthenticationMethod = null, Configured = true });

            message.functionbox_uuid = _configuration["Sefos:Functionbox_uuid"];
            message.sefos_participants = sefosParticipants;
            message.settings = new Settings() { loa_level = 0, require_response = 0 };
            message.External_participants = externalParticipants;

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_configuration["Sefos:API"], jsonContent);

            Console.WriteLine(response);

        }

    }
}
