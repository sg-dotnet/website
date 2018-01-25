using Data.Facebook;
using Data.Meetup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Web
{
    public class HomepageViewModel
    {
        public Event SoonestIncomingMeetupEvent { get; set; }

        public IEnumerable<Feed> FacebookGroupFeeds { get; set; }
    }
}
