using System;
namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type double
	/// </summary>
	public class TagDouble : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagDouble
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagDouble; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public double Data { get; set; }
		/// <summary />
		public double ToDouble()
		{
			return Data;
		}
		/// <summary />
		public void FromDouble(double val)
		{
			Data = val;
		}

		/// <summary />
		public static implicit operator double(TagDouble val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagDouble(double val)
		{
			return new TagDouble { Data = val };
		}

		public override int GetHashCode()
		{
			return (int)Math.Round((100 * (14 * Data + 219)));
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagDouble;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.Data == other.Data;
		}
	}
}
