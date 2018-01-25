using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetSingaporeWeb.Models;
using DotnetSingapore.Data.Web;
using DotnetSingaporeWeb.Services;

namespace DotnetSingaporeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISocialFeedService _socialFeedService;

        public HomeController(ISocialFeedService socialFeedService)
        {
            _socialFeedService = socialFeedService;
        }

        public async Task<IActionResult> Index()
        {
            var currentDate = DateTime.Now;

            var meetupEvents = await _socialFeedService.GetMeetupsAsync();

            foreach (var meetupEvent in meetupEvents)
            {
                long unixDate = meetupEvent.HappensAt;
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                meetupEvent.HappensAtDateTime = start.AddMilliseconds(unixDate).ToLocalTime();
            }

            var facebookGroupFeeds = await _socialFeedService.GetFecebookGroupFeedsAsync();

            var upcomingEvents = meetupEvents.Where(m => m.HappensAtDateTime >= currentDate).ToList();
            var pastEvents = meetupEvents.Where(m => m.HappensAtDateTime < currentDate).ToList();

            return View(new HomepageViewModel
            {
                IsHighlightedEventUpcoming = upcomingEvents.Count() > 0,
                HighlightedEvent = upcomingEvents.Count() > 0 ? 
                    upcomingEvents.OrderBy(m => m.HappensAtDateTime).FirstOrDefault() : 
                    pastEvents.OrderByDescending(m => m.HappensAtDateTime).FirstOrDefault(),
                FacebookGroupFeeds = facebookGroupFeeds.OrderByDescending(fb => fb.UpdatedTime)
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
