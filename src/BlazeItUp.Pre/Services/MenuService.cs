using BlazeItUp.Pre.Models;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeItUp.Pre.Services
{
    public class MenuService
    {
        private Dictionary<String, PageLink> _links;

        public MenuService()
        {
            _links = new Dictionary<string, PageLink>
            {
                { "about",   new PageLink { Href = "About", Icon = Icons.Material.Filled.Home, Title = "About Blaze It Up"  } },
                { "survey",   new PageLink { Href = "Survey", Icon = Icons.Material.Filled.DataUsage, Title = "Survey and Results"  } },
                { "contribute",   new PageLink { Href = "Contribute", Icon = Icons.Material.Filled.PersonAdd, Title = "Contribute"  } },
                { "contact",   new PageLink { Href = "Contact", Icon = Icons.Material.Filled.Mail, Title = "Contact"  } },
            };
        }

        public IEnumerable<PageLink> PageLinks => _links.Values;

        public String GetTileFromPath(String path)
        {
            if (String.IsNullOrEmpty(path) == true)
            {
                path = "about";
            }
            else
            {
                path = path.ToLower();
            }


            if (_links.ContainsKey(path) == false)
            {
                return String.Empty;
            }

            return _links[path].Title;
        }
    }
}
