namespace TecniCAD.ProjectManagerPortal.Models
{
    public interface ISiteConfig
    {
        string CategoriesPath { get; set; }
        string CustomersPath { get; set; }
        string FileLinkPath { get; set; }
        string ProjectItemsPath { get; set; }
        string ProjectsPath { get; set; }
        string SiteRootPath { get; set; }

        string GetProjectItems();
        string GetUriCategories();
        string GetUriCustomers();
        string GetUriFileLink();
        string GetUriProjects();
    }
}