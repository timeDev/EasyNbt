using System;
using System.Collections.Generic;

namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type list
	/// </summary>
	public class TagList : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagList
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagList; }
		}

		private List<NbtTag> tags;

		/// <summary>
		/// Creates a new instance of the TagList class.
		/// </summary>
		public TagList(TagType valueType)
		{
			ValueType = valueType;
			tags = new List<NbtTag>();
		}

		public TagList(TagType valueType, IEnumerable<NbtTag> tags)
		{
			ValueType = valueType;
			foreach(var tag in tags)
				if(tag.TagType != valueType)
					throw new ArgumentException("One of the tags does not match given value type.", "tags");
			this.tags = new List<NbtTag>(tags);
		}

		/// <summary>
		/// The tags in the list
		/// </summary>
		public IEnumerable<NbtTag> Tags
		{
			get { return tags; }
			set
			{
				tags.Clear();
				tags.AddRange(value);
			}
		}

		/// <summary>
		/// The total number of items in the TagList.
		/// </summary>
		public int Count { get { return tags.Count; } }

		/// <summary>
		/// The <see cref="TagType"/> of the items in the TagList
		/// </summary>
		public TagType ValueType { get; private set; }
		/// <summary>
		/// Adds the tag to the list
		/// </summary>
		/// <param name="tag">The tag to add</param>
		public void Add(NbtTag tag)
		{
			if(tag == null)
				throw new ArgumentNullException("tag");
			if(tag.TagType != ValueType)
				throw new InvalidOperationException("Attempted to add a tag with wrong tag type.");
			tags.Add(tag);
		}

		/// <summary>
		/// Gets the tag at the given index.
		/// </summary>
		/// <param name="index">The index of the tag</param>
		/// <returns>The tag at the given index</returns>
		public NbtTag this[int index]
		{
			get { return tags[index]; }
		}

		public TagList Clone()
		{
			return new TagList(ValueType, tags);
		}

		public override int GetHashCode()
		{
			int ret = -6512;
			foreach(var item in tags)
			{
				ret += item.GetHashCode();
			}
			return ret;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagList;
			if(this.tags.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.GetHashCode() == other.GetHashCode();
		}
	}
}
