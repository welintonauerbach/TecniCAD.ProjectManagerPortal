using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecniCAD.ProjectManagerPortal.Models
{
    public enum Mode
    {
        None,
        Add,
        Edit,
        Delete
    }

    public enum ListType
    {
        ForView,
        ForSelect,
        ForAll
    }
}
