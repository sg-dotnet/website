using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Facebook
{
    public class Feed
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "story")]
        public string Story { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string ArticleName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string ArticleDescription { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string ArticleUrl { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "from")]
        public Profile Author { get; set; }

        [JsonProperty(PropertyName = "updated_time")]
        public DateTimeOffset UpdatedTime { get; set; }
    }
}
