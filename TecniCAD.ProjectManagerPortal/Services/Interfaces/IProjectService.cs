using System.Collections.Generic;
using System.Threading.Tasks;
using TecniCAD.Models;

namespace TecniCAD.ProjectManagerPortal.Services
{
    public interface IProjectService
    {
        Task<bool> DeleteCustomer(int Id);
        Task<bool> DeleteProjectItem(int Id);
        Task<Customer> GetCustomer(int id);
        Task<List<Customer>> GetCustomers();
        Task<Project> GetProject(int id);
        Task<List<Project>> GetProjects();
        Task<bool> SaveProject(Project project);
        Task<bool> SaveProjectItem(ProjectItem projectItem);
        Task<bool> UpdateCustomer(int Id, Customer customer);
        Task<bool> UpdateProjectItem(int Id, ProjectItem projectItem);
        Task<bool> UpdateProject(int Id, Project project);
        Task<bool> SaveCustomer(Customer customer);
        Task<bool> SendEmail(EmailContent emailContent);
    }
}