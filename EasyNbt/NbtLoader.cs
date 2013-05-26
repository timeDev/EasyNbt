using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyNbt
{
	/// <summary>
	/// Loads NBT data from a file
	/// </summary>
	public static class NbtLoader
	{
		
		private static Stream fstr;
		private static EndOfStreamException eosException = new EndOfStreamException("Unexpected end of Stream");
		private static IOException nlException = new IOException("Negative length of array/list");
		private static IOException itException = new IOException("Invalid tag type");

		/// <summary>
		/// Reads the main TagCompound out of the given Stream
		/// </summary>
		/// <param name="stream">The Stream to read from</param>
		/// <returns>The main <see cref="TagCompound"/> of the Stream</returns>
		public static TagCompound Read(Stream stream)
		{
			fstr = stream;
			return ReadMain();
		}

		private static TagCompound ReadMain()
		{
			var type = (TagType)fstr.ReadByte();
			if(type == TagType.TagCompound)
			{
				ReadString(); //Name of the Main Tag
				return ReadValue<TagCompound>();
			}

			return null;
		}

		private static TagCompound ReadMain(out string mtagname)
		{
			var type = (TagType)fstr.ReadByte();
			mtagname = String.Empty;
			if(type == TagType.TagCompound)
			{
				mtagname = ReadString(); //Name of the Main Tag
				return ReadValue<TagCompound>();
			}
			return null;
		}

		private static TTag ReadValue<TTag>() where TTag : NbtTag, new()
		{
			switch(new TTag().TagType)
			{
				case TagType.TagEnd:
					return null;
				case TagType.TagByte:
					return ReadByte() as TTag;
				case TagType.TagShort:
					return ReadShort() as TTag;
				case TagType.TagInt:
					return ReadInt() as TTag;
				case TagType.TagLong:
					return ReadLong() as TTag;
				case TagType.TagFloat:
					return ReadFloat() as TTag;
				case TagType.TagDouble:
					return ReadDouble() as TTag;
				case TagType.TagByteArray:
					return ReadByteArray() as TTag;
				case TagType.TagString:
					return ReadString() as TTag;
				case TagType.TagList:
					return ReadList() as TTag;
				case TagType.TagCompound:
					return ReadCompound() as TTag;
				case TagType.TagIntArray:
					return ReadIntArray() as TTag;
			}
			throw new NotSupportedException("Invalid type parameter");
		}

		private static TagIntArray ReadIntArray()
		{
			var buf = new byte[4];
			fstr.Read(buf, 0, 4);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			var length = BitConverter.ToInt32(buf, 0);
			if(length < 0)
				throw nlException;

			var data = new int[length];
			for(var i = 0; i < length; i++)

				data[i] = ReadInt();


			return new TagIntArray { Data = data };
		}

		private static TagCompound ReadCompound()
		{
			var type = GetNextTagType();
			var tags = new Dictionary<string, NbtTag>();
			while(type != TagType.TagEnd)
			{
				tags.Add(ReadString(), ReadValue(type));
				type = GetNextTagType();
			}
			return new TagCompound(tags);
		}

		private static NbtTag ReadValue(TagType type)
		{
			switch(type)
			{
				case TagType.TagEnd:
					return null;
				case TagType.TagByte:
					return ReadByte();
				case TagType.TagShort:
					return ReadShort();
				case TagType.TagInt:
					return ReadInt();
				case TagType.TagLong:
					return ReadLong();
				case TagType.TagFloat:
					return ReadFloat();
				case TagType.TagDouble:
					return ReadDouble();
				case TagType.TagByteArray:
					return ReadByteArray();
				case TagType.TagString:
					return ReadString();
				case TagType.TagList:
					return ReadList();
				case TagType.TagCompound:
					return ReadCompound();
				case TagType.TagIntArray:
					return ReadIntArray();
				default: throw new ArgumentOutOfRangeException("type");
			}
		}

		private static TagList ReadList()
		{
			var b = fstr.ReadByte();
			if(b == -1)
				throw eosException;
			if(b > Enum.GetValues(typeof(TagType)).GetUpperBound(0))
				throw itException;
			var type = (TagType)b;

			var buf = new byte[4];
			fstr.Read(buf, 0, 4);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			var listlength = BitConverter.ToInt32(buf, 0);
			if(listlength < 0)
				throw nlException;

			var tags = new List<NbtTag>();
			for(var i = 0; i < listlength; i++)
				tags.Add(ReadValue(type));

			return new TagList(type) { Tags = tags };
		}

		private static TagByteArray ReadByteArray()
		{
			var buf = new byte[4];
			fstr.Read(buf, 0, 4);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			var arlength = BitConverter.ToInt32(buf, 0);
			if(arlength < 0)
				throw nlException;

			buf = new byte[arlength];
			fstr.Read(buf, 0, arlength);
			return buf;
		}

		private static TagShort ReadShort()
		{
			var buf = new byte[2];
			fstr.Read(buf, 0, 2);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			return BitConverter.ToInt16(buf, 0);

		}

		private static TagInt ReadInt()
		{
			var buf = new byte[4];
			fstr.Read(buf, 0, 4);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			return BitConverter.ToInt32(buf, 0);
		}

		private static TagLong ReadLong()
		{
			var buf = new byte[8];
			fstr.Read(buf, 0, 8);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			return BitConverter.ToInt64(buf, 0);
		}

		private static TagFloat ReadFloat()
		{
			var buf = new byte[4];
			fstr.Read(buf, 0, 4);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			return BitConverter.ToSingle(buf, 0);
		}

		private static TagDouble ReadDouble()
		{
			var buf = new byte[8];
			fstr.Read(buf, 0, 8);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);
			return BitConverter.ToDouble(buf, 0);
		}

		private static TagString ReadString()
		{
			var buf = new byte[2];
			fstr.Read(buf, 0, 2);
			if(BitConverter.IsLittleEndian)
				Array.Reverse(buf);

			var length = BitConverter.ToInt16(buf, 0);
			if(length < 0)
				throw nlException;

			buf = new byte[length];
			fstr.Read(buf, 0, length);

			return new TagString { Data = Encoding.GetEncoding(28591).GetString(buf) };
		}

		private static TagType GetNextTagType()
		{
			return (TagType)fstr.ReadByte();
		}

		private static TagByte ReadByte()
		{
			var b = fstr.ReadByte();
			if(b == -1)
				throw eosException;
			return (byte)b;
		}

		/// <summary>
		/// Reads the main TagCompound out of the given Stream
		/// </summary>
		/// <param name="stream">The Stream to read from</param>
		/// <param name="mainTagName">The name of the main tag</param>
		/// <returns>The main <see cref="TagCompound"/> of the Stream</returns>
		public static TagCompound Read(Stream stream, out string mainTagName)
		{
			fstr = stream;
			return ReadMain(out mainTagName);
		}
	}
}
