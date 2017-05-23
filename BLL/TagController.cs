using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
	public class TagController : IController<Tag>
	{
		private readonly TagDB tagDb = new TagDB();
		public Tag Create(Tag tag)
		{
			return tagDb.Create(tag);
		}

		public Tag Read(Tag tag)
		{
			return tagDb.Read(tag);
		}
		public Tag Update(Tag tag)
		{
			return tagDb.Update(tag);
		}
		public Tag Delete(Tag tag)
		{
			return tagDb.Delete(tag);
		}

		public List<Tag> ReadAll()
		{
			return tagDb.ReadAll();
		}


	}
}
