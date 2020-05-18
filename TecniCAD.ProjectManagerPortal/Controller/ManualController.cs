using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TecniCAD.Models;
using TecniCAD.Utils;
using Microsoft.Extensions.Configuration;

namespace TecniCAD.ProjectManagerPortal.Controller
{
    public class ManualController
    {
        string baseUrl;
        string baseAdressFileLink;
        string baseAdressCategories;

        string UriFileLink;
        string UriCategories;

        private IConfiguration _configuration;

        public ManualController(IConfiguration configuration)
        {
            _configuration = configuration;

            baseUrl = _configuration.GetSection("SiteConfig:SiteRootPath").Value;
            baseAdressFileLink = _configuration.GetSection("SiteConfig:FileLinkPath").Value;
            baseAdressCategories = _configuration.GetSection("SiteConfig:CategoriesPath").Value;

            UriFileLink = $"{baseUrl}{baseAdressFileLink}";
            UriCategories = $"{baseUrl}{baseAdressCategories}";
        }

        public async Task<List<Category>> GetCategories()
        {
            var h = new TcHttp($"{UriCategories}"); ;

            var result = await h.GetJsonList<Category>();

            var categories = result.OrderBy(c => c.Name).ToList();
            return categories;
        }

        public async Task<List<FileLink>> GetManuals()
        {
            var http = new TcHttp(UriFileLink);

            var result = await http.GetJsonList<FileLink>();

            var manualLinks = result.OrderBy(c => c.Category.Name).ToList();

            return manualLinks;
        }

        public async Task<bool> DeleteManual(int Id)
        {
            var http = new TcHttp($"{UriFileLink}/{Id}");
            var response = await http.GetHttpClient().DeleteAsync($"{UriFileLink}/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> PostDocument(FileLink file)
        {
            var http = new TcHttp(UriFileLink);
            var content = http.GetJsonHttpContent(file);

            var response = await http.GetHttpClient().PostAsync(UriFileLink, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<bool> UpdateDocument(int id, FileLink file)
        {
            if (id != file.FileLinkId)
            {
                return false;
            }

            var http = new TcHttp($"{UriFileLink}/{id}");
            var content = http.GetJsonHttpContent(file);

            var response = await http.GetHttpClient().PutAsync($"{UriFileLink}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<List<Category>> PostCategory(Category category)
        {
            var http = new TcHttp(UriCategories);
            var content = http.GetJsonHttpContent(category);

            var response = await http.GetHttpClient().PostAsync(UriCategories, content);

            if (response.IsSuccessStatusCode)
            {
                return await GetCategories(); ;
            }
            else { return null; }
        }

        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
