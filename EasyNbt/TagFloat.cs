using System;
namespace EasyNbt
{
	/// <summary>
	/// An NBTTag of type float
	/// </summary>
	public class TagFloat : NbtTag
	{
		/// <summary>
		/// The type of the tag. Always TagType.TagFloat
		/// </summary>
		public override TagType TagType
		{
			get { return TagType.TagFloat; }
		}

		/// <summary>
		/// The data of this tag
		/// </summary>
		public float Data { get; set; }
		/// <summary />
		public float ToSingle()
		{
			return Data;
		}
		/// <summary />
		public void FromSingle(float val)
		{
			Data = val;
		}

		/// <summary />
		public static implicit operator float(TagFloat val)
		{
			return val.Data;
		}

		/// <summary />
		public static implicit operator TagFloat(float val)
		{
			return new TagFloat { Data = val };
		}

		public override int GetHashCode()
		{
			return (int)Math.Round((100 * (14 * Data + 219)));
		}

		public override bool Equals(object obj)
		{
			var other = obj as TagFloat;
			if(this.Data.Equals(obj))
				return true;
			if(other == null)
				return false;
			return this.Data == other.Data;
		}
	}
}
