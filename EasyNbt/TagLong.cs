namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type long
	/// </summary>
	public class TagLong : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagLong
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagLong; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public long Data { get; set; }
		/// <summary />
		public long ToInt64()
		{ return Data; }
		/// <summary />
		public void FromInt64(long val)
		{ Data = val; }

		/// <summary />
		public static implicit operator long(TagLong val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagLong(long val)
		{
			return new TagLong { Data = val };
		}

		public override int GetHashCode()
		{
			return (int)((14 * Data + 219) % int.MaxValue);
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagLong;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.Data == other.Data;
		}
	}
}
