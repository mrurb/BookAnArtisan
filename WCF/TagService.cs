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
        public Tag CreateTag(Tag tag)
        {
            return tagController.Create(tag);
        }

        public Tag ReadTag(Tag tag)
        {
            return tagController.Read(tag);
        }
        public Tag UpdateTag(Tag tag)
        {
            return tagController.Update(tag);
        }
        public Tag DeleteTag(Tag tag)
        {
            return tagController.Delete(tag);
        }

        public List<Tag> ReadAllTag()
        {
            return tagController.ReadAll();
        }
    }
}
