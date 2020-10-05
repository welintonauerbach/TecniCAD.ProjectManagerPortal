using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecniCAD.Models;

namespace TecniCAD.ProjectManagerPortal.Repository
{
    public interface IProjectRepository
    {
    }

    public class ProjectRepository : IProjectRepository
    {
        protected Project project;
        protected List<ProjectItem> itemlList;

        protected ProjectItem projectItem;
        protected List<FileLink> manualList;
        protected EmailContent email;
        protected FileLink FileLinkSelected;




    }

    
}
