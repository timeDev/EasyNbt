namespace EasyNbt
{
	/// <summary>
	/// An NBT tag
	/// </summary>
	public abstract class NbtTag
	{
		/// <summary>
		/// The type of this tag
		/// </summary>
		public abstract TagType TagType { get; }
	}

	/// <summary>
	/// List of all tagtypes
	/// </summary>
	public enum TagType
	{
		/// <summary>
		/// Represents a tag of type 0 or End
		/// </summary>
		TagEnd = 0,
		/// <summary>
		/// Represents a tag of type 1 or Byte
		/// </summary>
		TagByte = 1,
		/// <summary>
		/// Represents a tag of type 2 or Short
		/// </summary>
		TagShort = 2,
		/// <summary>
		/// Represents a tag of type 3 or Int
		/// </summary>
		TagInt = 3,
		/// <summary>
		/// Represents a tag of type 4 or Long
		/// </summary>
		TagLong = 4,
		/// <summary>
		/// Represents a tag of type 5 or Float
		/// </summary>
		TagFloat = 5,
		/// <summary>
		/// Represents a tag of type 6 or Double
		/// </summary>
		TagDouble = 6,
		/// <summary>
		/// Represents a tag of type 7 or Byte Array
		/// </summary>
		TagByteArray = 7,
		/// <summary>
		/// Represents a tag of type 8 or String
		/// </summary>
		TagString = 8,
		/// <summary>
		/// Represents a tag of type 9 or List
		/// </summary>
		TagList = 9,
		/// <summary>
		/// Represents a tag of type 10 or Compound
		/// </summary>
		TagCompound = 10,
		/// <summary>
		/// Represents a tag of type 11 or Int Array
		/// </summary>
		TagIntArray = 11
	}
}
