using System.Collections.Generic;
using BLL;
using Model;

namespace WCF
{
	public class TagService : ITagService
	{
		private readonly TagController tagController = new TagController();

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