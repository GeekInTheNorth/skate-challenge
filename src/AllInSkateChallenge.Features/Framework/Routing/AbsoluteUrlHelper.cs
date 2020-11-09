using System;

using Microsoft.Extensions.Options;

namespace AllInSkateChallenge.Features.Framework.Routing
{

    public class AbsoluteUrlHelper : IAbsoluteUrlHelper
    {
        public readonly RouteSettings routeSettings;

        public AbsoluteUrlHelper(IOptions<RouteSettings> routeSettings)
        {
            this.routeSettings = routeSettings.Value;
        }

        public string Get(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return null;
            }

            var absolutePath = new Uri(new Uri(routeSettings.SiteUrl), relativePath);

            return absolutePath.ToString();
        }
    }
}
