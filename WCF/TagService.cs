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
        public Tag Create(Tag tag)
        {
            TagController tagController = new TagController();
            return tagController.Create(tag);
        }

        public Tag Read(Tag tag)
        {
            TagController tagController = new TagController();
            return tagController.Read(tag);
        }
        public Tag Update(Tag tag)
        {
            TagController tagController = new TagController();
            return tagController.Update(tag);
        }
        public Tag Delete(Tag tag)
        {
            TagController tagController = new TagController();
            return tagController.Delete(tag);
        }

        public List<Tag> ReadAll()
        {
            TagController tagController = new TagController();
            return tagController.ReadAll();
        }
    }
}
