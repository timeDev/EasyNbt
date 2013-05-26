namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type intArray
	/// </summary>
	public class TagIntArray : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagIntArray
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagIntArray; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public int[] Data { get; set; }
		/// <summary />
		public int[] ToInt32Array()
		{ return Data; }
		/// <summary />
		public void FromInt32Array(int[] val)
		{ Data = val; }

		/// <summary />
		public static implicit operator int[](TagIntArray val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagIntArray(int[] val)
		{
			return new TagIntArray { Data = val };
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
			var other = obj as TagIntArray;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.GetHashCode() == other.GetHashCode();
		}
	}
}
