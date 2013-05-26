namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type byteArray
	/// </summary>
	public class TagByteArray : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagByteArray
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagByteArray; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public byte[] Data { get; set; }

		/// <summary />
		public byte[] ToByteArray()
		{
			return Data;
		}
		/// <summary />
		public void FromByteArray(byte[] val)
		{
			Data = val;
		}

		/// <summary />
		public static implicit operator byte[](TagByteArray val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagByteArray(byte[] val)
		{
			return new TagByteArray { Data = val };
		}

		public override int GetHashCode()
		{
			int ret = 0;
			foreach(var item in Data)
			{
				ret += 14 * item + 219;
			}
			return ret;
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagByteArray;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			if(this.GetHashCode() == other.GetHashCode())
				return true;
			return false;
		}
	}
}
