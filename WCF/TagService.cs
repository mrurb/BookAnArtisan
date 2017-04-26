using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
    public class TagService : ITagService
    {
        TagController tagController = new TagController();
        public Tag Create(Tag tag)
        {
            return tagController.Create(tag);
        }

        public Tag Read(Tag tag)
        {
            return tagController.Read(tag);
        }
        public Tag Update(Tag tag)
        {
            return tagController.Update(tag);
        }
        public bool Delete(Tag tag)
        {
            return tagController.Delete(tag);
        }

        public List<Tag> ReadAll()
        {
            return tagController.ReadAll();
        }
    }
}
