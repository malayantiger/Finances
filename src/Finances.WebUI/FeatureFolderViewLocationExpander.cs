using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace Finances.WebUI
{
    public class FeatureFolderViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new[]
            {
                "{2}/Views/{1}/{0}.cshtml",
                "{2}/Views/Shared/{0}.cshtml",
                "Shared/Views/{0}.cshtml"
            };
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
