using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class ProjectController : IController<Project>
    {
        public Project Create(Project t)
        {
            ProjectDB db = new ProjectDB();
            return db.Create(t);
        }

        public Project Read(int id)
        {
            ProjectDB db = new ProjectDB();
            return db.Read(id);
        }

        public Project Update(Project t)
        {
            ProjectDB db = new ProjectDB();
            return db.Update(t);
        }

        public Project Delete(Project t)
        {
            ProjectDB db = new ProjectDB();
            return db.Delete(t);
        }
    }
}
