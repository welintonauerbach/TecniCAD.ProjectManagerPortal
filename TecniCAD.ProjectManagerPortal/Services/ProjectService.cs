using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TecniCAD.Models;
using TecniCAD.ProjectManagerPortal.Controller;

using TecniCAD.Utils;

namespace TecniCAD.ProjectManagerPortal.Services
{
    
    public class ProjectService : ControllerBase, IProjectService
    {
        string baseUrl;
        string baseAdressProjects;
        string baseAdressCustomers;
        string baseAdressProjectItems;
        string baseAdressEmail;

        string UriProjects;
        string UriCustomers;
        string UriProjectItems;
        string UriEmail;

        //private readonly SiteConfig _siteConfig;
        private IConfiguration _configuration;

        public ProjectService(IConfiguration configuration)
        {
            _configuration = configuration;

            baseUrl = _configuration.GetSection("SiteConfig:SiteRootPath").Value;
            baseAdressProjects = _configuration.GetSection("SiteConfig:ProjectsPath").Value;
            baseAdressCustomers = _configuration.GetSection("SiteConfig:CustomersPath").Value;
            baseAdressProjectItems = _configuration.GetSection("SiteConfig:ProjectItemsPath").Value;
            baseAdressEmail = _configuration.GetSection("SiteConfig:EmailPath").Value;             

            UriProjects = $"{baseUrl}{baseAdressProjects}";
            UriCustomers = $"{baseUrl}{baseAdressCustomers}";
            UriProjectItems = $"{baseUrl}{baseAdressProjectItems}";
            UriEmail = $"{baseUrl}{baseAdressEmail}";
        }

        public async Task<List<Project>> GetProjects()
        {
            var http = new TcHttp(UriProjects);

            var result = await http.GetJsonList<Project>();

            var manualLinks = result.OrderBy(c => c.Customer.Number).ToList();

            return manualLinks;
        }

        public async Task<Project> GetProject(int id)
        {
            var http = new TcHttp($"{UriProjects}/{id}");

            var result = await http.GetJsonClass<Project>();

            return result;
        }

        public async Task<bool> SaveProject(Project project)
        {
            var http = new TcHttp($"{UriProjects}");
            var content = http.GetJsonHttpContent(project);

            var response = await http.GetHttpClient().PostAsync(UriProjects, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateProject(int Id, Project project)
        {
            var http = new TcHttp($"{UriProjects}/{Id}");
            var content = http.GetJsonHttpContent(project);

            var response = await http.GetHttpClient().PutAsync($"{UriProjects}/{Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SaveProjectItem(ProjectItem projectItem)
        {
            var http = new TcHttp($"{UriProjectItems}");
            var content = http.GetJsonHttpContent(projectItem);

            var response = await http.GetHttpClient().PostAsync(UriProjectItems, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateProjectItem(int Id, ProjectItem projectItem)
        {
            var http = new TcHttp($"{UriProjectItems}/{Id}");
            var content = http.GetJsonHttpContent(projectItem);

            var response = await http.GetHttpClient().PutAsync($"{UriProjectItems}/{Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteProjectItem(int Id)
        {
            var http = new TcHttp($"{UriProjectItems}/{Id}");
            var response = await http.GetHttpClient().DeleteAsync($"{UriProjectItems}/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> SaveCustomer(Customer customer)
        {
            var http = new TcHttp($"{UriCustomers}");
            var content = http.GetJsonHttpContent(customer);

            var response = await http.GetHttpClient().PostAsync(UriCustomers, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var http = new TcHttp(UriCustomers);

            var result = await http.GetJsonList<Customer>();

            var customers = result.OrderBy(c => c.Number).ToList();

            return customers;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var http = new TcHttp($"{UriCustomers}/{id}");

            var result = await http.GetJsonClass<Customer>();

            return result;
        }

        public async Task<bool> UpdateCustomer(int Id, Customer customer)
        {
            var http = new TcHttp($"{UriCustomers}/{Id}");
            var content = http.GetJsonHttpContent(customer);

            var response = await http.GetHttpClient().PutAsync($"{UriCustomers}/{Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(int Id)
        {
            var http = new TcHttp($"{UriCustomers}/{Id}");
            var response = await http.GetHttpClient().DeleteAsync($"{UriCustomers}/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SendEmail(EmailContent emailContent)
        {
            var http = new TcHttp($"{UriEmail}");
            var content = http.GetJsonHttpContent(emailContent);

            var response = await http.GetHttpClient().PostAsync(UriEmail, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
