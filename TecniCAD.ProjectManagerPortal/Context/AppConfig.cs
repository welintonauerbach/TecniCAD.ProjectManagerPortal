using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecniCAD.ProjectManagerPortal.Context
{    
    public interface IAppConfig
    {        
        string SiteAdress { get; set; }
    }

    public class AppConfig : IAppConfig
    {
        private IConfiguration _config;
        private string siteAdress;
                
        public AppConfig(IConfiguration config)
        {
            _config = config;
        }

        [Inject]
        public string SiteAdress { get => _config.GetSection("PortalConfig:SiteApi").Value; set => siteAdress = value; }
    }
}
