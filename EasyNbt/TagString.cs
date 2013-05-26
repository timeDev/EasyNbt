namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type string
	/// </summary>
	public class TagString : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagString
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagString; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public string Data { get; set; }
		/// <summary />
		public override string ToString()
		{ return Data; }
		/// <summary />
		public void FromString(string val)
		{ Data = val; }

		/// <summary />
		public static implicit operator string(TagString val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagString(string val)
		{
			return new TagString { Data = val };
		}

		public override int GetHashCode()
		{
			int ret = -2554;
			foreach(var item in Data)
			{
				ret += item.GetHashCode();
			}
			return ret;
		}

		public override bool Equals(object obj)
		{
			if(this.Data.Equals(obj))
				return true;
			var other = obj as TagString;
			if(other == null)
				return false;
			return this.Data == other.Data;
		}
	}
}
