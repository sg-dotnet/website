using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetSingapore.Data.Facebook
{
    public class Profile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
