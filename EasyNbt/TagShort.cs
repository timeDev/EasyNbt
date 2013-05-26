namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type short
	/// </summary>
	public class TagShort : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagShort
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagShort; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public short Data { get; set; }
		/// <summary />
		public short ToInt32()
		{ return Data; }
		/// <summary />
		public void FromInt32(short val)
		{ Data = val; }

		/// <summary />
		public static implicit operator short(TagShort val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagShort(short val)
		{
			return new TagShort { Data = val };
		}

		public override int GetHashCode()
		{
			return 14 * Data + 219;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagShort;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.Data == other.Data;
		}
	}
}
