using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetSingapore.Http
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}
