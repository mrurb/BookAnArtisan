using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TagController : IController<Tag>
    {
        TagDB db = new TagDB();
        public Tag Create(Tag tag)
        {
            return db.Create(tag);
        }

        public Tag Read(Tag tag)
        {
            return db.Read(tag);
        }
        public Tag Update(Tag tag)
        {
            return db.Update(tag);
        }
        public Tag Delete(Tag tag)
        {
            return db.Delete(tag);
        }

        public List<Tag> ReadAll()
        {
            return db.ReadAll();
        }

        
    }
}
