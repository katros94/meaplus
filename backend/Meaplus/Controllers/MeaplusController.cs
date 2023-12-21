using Meaplus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meaplus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeaplusController : ControllerBase
    {
        const string API = "https://test-meaplus.sefos.se/server/rest/api/secure";
        
        [HttpPost]
        public async void Post([FromBody] Message message)
        {
            
            var sefosParticipants = new List<SefosParticipant>();
            sefosParticipants.Add(new SefosParticipant { Email = "funktion2@fatg.se" });

            message.functionbox_uuid = "u95yt3zx933p:09pf5h9o";
            message.sefos_participants = sefosParticipants;
            message.settings = new Settings() { loa_level = 0, require_response = 0 };

            var json = JsonConvert.SerializeObject(message);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var content = await client.PostAsync(API, data);

            Console.WriteLine(content);

        }

    }
}
