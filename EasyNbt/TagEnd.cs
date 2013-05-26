namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type end. Only used to mark the end of compound tags.
	/// </summary>
	public class TagEnd : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagEnd
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagEnd; }
		}

		public override int GetHashCode()
		{
			return 14 * 0 + 219;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagByte;
			if(other == null)
				return false;
			return true;
		}
	}
}
