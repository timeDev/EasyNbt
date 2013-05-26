namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type byte
	/// </summary>
	public class TagByte : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagByte
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagByte; }
		}
		/// <summary />
		public byte ToByte()
		{
			return Data;
		}
		/// <summary />
		public void FromByte(byte val)
		{
			Data = val;
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public byte Data { get; set; }

		/// <summary />
		public static implicit operator byte(TagByte val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagByte(byte val)
		{
			return new TagByte { Data = val };
		}

		public override int GetHashCode()
		{
			return 14 * Data + 219;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagByte;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			if(this.Data == other.Data)
				return true;
			return false;
		}
	}
}
