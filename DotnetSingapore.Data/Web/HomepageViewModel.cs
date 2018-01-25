using DotnetSingapore.Data.Facebook;
using DotnetSingapore.Data.Meetup;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetSingapore.Data.Web
{
    public class HomepageViewModel
    {
        public bool IsHighlightedEventUpcoming { get; set; }

        public Event HighlightedEvent { get; set; }

        public IEnumerable<Feed> FacebookGroupFeeds { get; set; }
    }
}
