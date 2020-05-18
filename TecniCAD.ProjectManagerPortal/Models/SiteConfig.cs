using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecniCAD.ProjectManagerPortal.Models
{
    public class SiteConfig : ISiteConfig
    {
        public string SiteRootPath { get; set; }
        public string ProjectsPath { get; set; }
        public string FileLinkPath { get; set; }
        public string CategoriesPath { get; set; }
        public string ProjectItemsPath { get; set; }
        public string CustomersPath { get; set; }


        public string GetUriProjects() { return $"{SiteRootPath}{ProjectsPath}"; }
        public string GetUriFileLink() { return $"{SiteRootPath}{FileLinkPath}"; }
        public string GetUriCategories() { return $"{SiteRootPath}{CategoriesPath}"; }
        public string GetProjectItems() { return $"{SiteRootPath}{ProjectItemsPath}"; }
        public string GetUriCustomers() { return $"{SiteRootPath}{CustomersPath}"; }

    }
}
