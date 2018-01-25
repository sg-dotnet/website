using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DotnetSingapore.Data.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SocialFeed.Controllers
{
    [Produces("application/json")]
    [Route("api/FacebookGroup")]
    public class FacebookGroupController : Controller
    {
        private const string SG_DOT_NET_COMMUNITY_FB_GROUP_ID = "1504549153159226";
        private const string SG_AZURE_COMMUNITY_FB_GROUP_ID = "774384765970833";

        private readonly IConfiguration _configuration;

        public FacebookGroupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/FacebookGroup
        [HttpGet]
        public async Task<IEnumerable<Feed>> Get()
        {
            var newSgDotNetCommunityFacebookGroupFeeds = await GetFacebookGroupFeedsAsJsonAsync(SG_DOT_NET_COMMUNITY_FB_GROUP_ID);

            var newAzureCommunityFacebookGroupFeeds = await GetFacebookGroupFeedsAsJsonAsync(SG_AZURE_COMMUNITY_FB_GROUP_ID);

            return UnionLists(newSgDotNetCommunityFacebookGroupFeeds.Feeds, newAzureCommunityFacebookGroupFeeds.Feeds);
        }

        private async Task<GroupFeed> GetFacebookGroupFeedsAsJsonAsync(string facebookGroupId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(
                    "https://graph.facebook.com/v2.8/" + facebookGroupId + "/feed?" +
                    "fields=story,updated_time,message,description,link,from,name,picture&" +
                    "access_token=" + _configuration["AppSettings:FacebookGraphAccessToken"]
                    );

                var facebookGroupFeeds = JsonConvert.DeserializeObject<GroupFeed>(await response.Content.ReadAsStringAsync());

                string actualImageUrlStartingPointIdentifier = "url=http";

                foreach (var feed in facebookGroupFeeds.Feeds)
                {
                    if (!string.IsNullOrWhiteSpace(feed.PictureUrl))
                    {
                        int startingIndexOfActualImageUrl = feed.PictureUrl.LastIndexOf(actualImageUrlStartingPointIdentifier);

                        if (startingIndexOfActualImageUrl >= 0)
                        {
                            string[] actualImageUrlComponents = feed.PictureUrl.Substring(startingIndexOfActualImageUrl + "url=".Length).Split('&', StringSplitOptions.RemoveEmptyEntries);

                            feed.PictureUrl = WebUtility.UrlDecode(actualImageUrlComponents[0]);
                        }
                        else
                        {
                            feed.PictureUrl = null;
                        }
                    }
                }

                return facebookGroupFeeds;
            }

        }

        private static List<Feed> UnionLists(List<Feed> targetList, List<Feed> sourceList)
        {
            var finalizedFeeds = new List<Feed>(targetList);

            foreach (var newItem in sourceList)
            {
                var isFeedExist = false;

                foreach (var existingItem in targetList)
                {
                    if (existingItem.Id == newItem.Id ||
                    (existingItem.ArticleUrl != null && newItem.ArticleUrl != null && existingItem.ArticleUrl.Trim() == newItem.ArticleUrl.Trim()))
                    {

                        isFeedExist = true;

                        break;
                    }
                }

                if (!isFeedExist)
                {
                    finalizedFeeds.Add(newItem);
                }
            }

            return finalizedFeeds;
        }
    }
}
