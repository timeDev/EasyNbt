namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type int
	/// </summary>
	public class TagInt : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagInt
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagInt; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public int Data { get; set; }
		/// <summary />
		public int ToInt32()
		{ return Data; }
		/// <summary />
		public void FromInt32(int val)
		{ Data = val; }

		/// <summary />
		public static implicit operator int(TagInt val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagInt(int val)
		{
			return new TagInt { Data = val };
		}

		public override int GetHashCode()
		{
			return 14 * Data + 219;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagInt;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.Data == other.Data;
		}
	}
}
