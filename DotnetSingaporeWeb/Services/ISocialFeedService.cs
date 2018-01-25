using DotnetSingapore.Data.Facebook;
using DotnetSingapore.Data.Meetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSingaporeWeb.Services
{
    public interface ISocialFeedService
    {
        Task<IEnumerable<Event>> GetMeetupsAsync();

        Task<IEnumerable<Feed>> GetFecebookGroupFeedsAsync();
    }
}
