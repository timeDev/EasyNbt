using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type compound
	/// </summary>
	public class TagCompound : NbtTag, IEnumerable<KeyValuePair<string, NbtTag>>
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagCompund
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagCompound; }
		}

		private Dictionary<string, NbtTag> tags;
		/// <summary>
		/// Creates a new instance of the <see cref="TagCompound"/> class.
		/// </summary>
		public TagCompound()
		{
			tags = new Dictionary<string, NbtTag>();
		}
		/// <summary>
		/// Creates a new instance of the <see cref="TagCompound"/> class.
		/// </summary>
		/// <param name="tags">The tags in the new TagCompound</param>
		public TagCompound(IDictionary<string, NbtTag> tags)
		{
			this.tags = new Dictionary<string, NbtTag>(tags);
		}
		/// <summary>
		/// The tags in this TagCompound
		/// </summary>
		public IEnumerable<KeyValuePair<string, NbtTag>> Tags { get { return tags; } }
		/// <summary>
		/// Gets or sets the tag with the given name.
		/// </summary>
		/// <param name="name">The name of the tag to get or set</param>
		/// <returns>The tag with the given name</returns>
		public NbtTag this[string name]
		{
			get { return tags[name]; }
			set { tags[name] = value; }
		}
		/// <summary>
		/// Gets the <see cref="TagCompound"/> with the given name.
		/// </summary>
		/// <param name="name">The name of the tag</param>
		/// <returns>The <see cref="TagCompound"/> with the given name or null, if it doesn't exist or if it isn't a TagCompound.</returns>
		public TagCompound GetCompound(string name)
		{
			return GetTag<TagCompound>(name);
		}
		/// <summary>
		/// Gets the tag with the given name.
		/// </summary>
		/// <param name="name">The name of the tag</param>
		/// <returns>The tag with the given name or null, if it doesn't exist.</returns>
		public NbtTag GetTag(string name)
		{
			return tags.ContainsKey(name) ? tags[name] : null;
		}

		/// <summary>
		/// Gets the tag with the given name and type.
		/// </summary>
		/// <typeparam name="T">The type of tag to get. Must inherit NBTTag</typeparam>
		/// <param name="name">The name of the tag</param>
		/// <returns>The tag with the given name or null, if the tag isn't of type T.</returns>
		public T GetTag<T>(string name) where T : NbtTag
		{
			return GetTag(name) as T;
		}
		/// <summary>
		/// Adds a tag to the TagCompound
		/// </summary>
		/// <param name="name">The name of the new tag</param>
		/// <param name="tag">The tag to add</param>
		public void Add(string name, NbtTag tag)
		{
			tags.Add(name, tag);
		}

		/// <summary />
		IEnumerator<KeyValuePair<string, NbtTag>> IEnumerable<KeyValuePair<string, NbtTag>>.GetEnumerator()
		{
			return tags.GetEnumerator();
		}

		/// <summary />
		public IEnumerator GetEnumerator()
		{
			return tags.GetEnumerator();
		}
		/// <summary>
		/// Checks, if the tag contains a tag with the given name.
		/// </summary>
		/// <param name="name">The name of the tag to search for</param>
		/// <returns>True, if the tag contains a tag with the given name</returns>
		public bool Contains(string name)
		{
			return tags.ContainsKey(name);
		}

		public TagCompound Clone()
		{
			return new TagCompound(tags);
		}

		public override int GetHashCode()
		{
			int ret = -6512;
			bool and = true;
			foreach(var item in tags)
			{
				if(and)
					ret &= item.GetHashCode();
				else
					ret |= item.GetHashCode();
				and = !and;
			}
			return ret;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagCompound;
			if(this.tags.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.GetHashCode() == other.GetHashCode();
		}

		public static TagCompound Merge(params TagCompound[] tags)
		{
			var ret = new TagCompound();
			foreach(var item in tags)
				foreach(var tag in item.Tags)
					if(!ret.Contains(tag.Key))
						ret.Add(tag.Key, tag.Value);
			return ret;
		}
	}
}