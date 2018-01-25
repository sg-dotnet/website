using DotnetSingapore.Data.Facebook;
using DotnetSingapore.Data.Meetup;
using DotnetSingapore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSingaporeWeb.Services
{
    public class SocialFeedService : ISocialFeedService
    {
        private readonly IHttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _remoteServiceBaseUrl;

        public SocialFeedService(IConfiguration configuration, IHttpClient apiClient)
        {
            _configuration = configuration;
            _apiClient = apiClient;

            _remoteServiceBaseUrl = $"{ _configuration["AppSettings:MeetupUrl"] }/api";
        }

        public async Task<IEnumerable<Event>> GetMeetupsAsync()
        {
            var allCampaignItemsUri = $"{ _remoteServiceBaseUrl }/Meetup/";

            var dataString = await _apiClient.GetStringAsync(allCampaignItemsUri);

            var response = JsonConvert.DeserializeObject<IEnumerable<Event>>(dataString);

            return response;
        }

        public async Task<IEnumerable<Feed>> GetFecebookGroupFeedsAsync()
        {
            var allCampaignItemsUri = $"{ _remoteServiceBaseUrl }/FacebookGroup/";

            var dataString = await _apiClient.GetStringAsync(allCampaignItemsUri);

            var response = JsonConvert.DeserializeObject<IEnumerable<Feed>>(dataString);

            return response;
        }
    }
}
