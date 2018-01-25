using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DotnetSingapore.Data.Meetup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SocialFeed.Controllers
{
    [Produces("application/json")]
    [Route("api/Meetup")]
    public class MeetupController : Controller
    {
        private readonly IConfiguration _configuration;

        public MeetupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Meetup
        [HttpGet]
        public async Task<IEnumerable<Event>> Get()
        {
            string meetupEventsApiUrl = "https://api.meetup.com/NET-Developers-SG/events?key=" + _configuration["AppSettings:MeetupWebApiKey"] + "&scroll=future_or_past";
            using (var client = new HttpClient())
            {
                try
                {
                    var meetupResponse = await client.GetAsync(meetupEventsApiUrl);
                    meetupResponse.EnsureSuccessStatusCode();

                    string stringResponse = await meetupResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Event>>(stringResponse);
                }
                catch (HttpRequestException)
                {

                }
            }

            return new List<Event>();
        }
    }
}
