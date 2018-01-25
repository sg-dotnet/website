using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Facebook
{
    public class GroupFeed
    {
        [JsonProperty(PropertyName = "data")]
        public List<Feed> Feeds { get; set; }
    }
}
