using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TecniCAD.Models;

namespace TecniCAD.ProjectManagerPortal.Services
{
    public interface IManualService
    {
        Task<List<Category>> GetCategories();
        Task<List<FileLink>> GetManuals();
        Task<bool> DeleteManual(int Id);
        Task<bool> PostDocument(FileLink file);
        Task<bool> UpdateDocument(int id, FileLink file);
        Task<List<Category>> PostCategory(Category category);
        void Put(int id, [FromBody] string value);
    }
}