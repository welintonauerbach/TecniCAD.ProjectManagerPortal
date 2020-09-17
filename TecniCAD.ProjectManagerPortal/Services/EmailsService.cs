using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TecniCAD.Models;
using TecniCAD.Utils;

namespace TecniCAD.ProjectManagerPortal.Controller
{    
    public class EmailsService
    {
        string baseUrl;
        string baseAdressEmails;
        string baseAdressCategories;

        string UriEmails;        

        private IConfiguration _configuration;

        public EmailsService(IConfiguration configuration)
        {
            _configuration = configuration;

            baseUrl = _configuration.GetSection("SiteConfig:SiteRootPath").Value;
            baseAdressEmails = _configuration.GetSection("SiteConfig:EmailSentsPath").Value;            

            UriEmails = $"{baseUrl}{baseAdressEmails}";           
        }

        public async Task<List<EmailSent>> GetEmails()
        {
            var h = new TcHttp($"{UriEmails}"); ;

            var result = await h.GetJsonList<EmailSent>();

            var emails = result.OrderBy(c => c.DateTime).ToList();
            return emails;
        }
    }
}