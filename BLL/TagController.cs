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
        public Tag Create(Tag tag)
        {
            TagDB db = new TagDB();
            return db.Create(tag);
        }

        public Tag Read(Tag tag)
        {
            TagDB db = new TagDB();
            return db.Read(tag);
        }
        public Tag Update(Tag tag)
        {
            TagDB db = new TagDB();
            return db.Update(tag);
        }
        public Tag Delete(Tag tag)
        {
            TagDB db = new TagDB();
            return db.Delete(tag);
        }

        public List<Tag> ReadAll()
        {
            TagDB db = new TagDB();
            return db.ReadAll();
        }

        
    }
}
