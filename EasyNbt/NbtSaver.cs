using System;
using System.IO;
using System.Text;

namespace EasyNbt
{
	/// <summary>
	/// Saves NBT data
	/// </summary>
	public static class NbtSaver
	{
		private static Stream fstr;

		/// <summary>
		/// Writes the tag main with tha name name into s
		/// </summary>
		/// <param name="main">The main tag to save</param>
		/// <param name="name">The name of the main tag</param>
		/// <param name="stream">The stream to write into</param>
		public static void Write(TagCompound main, string name, Stream stream)
		{
			if(stream == null || !stream.CanWrite) return;
			fstr = stream;
			WriteTag(name, main);
		}

		private static void WriteValue(NbtTag tag)
		{
			switch(tag.TagType)
			{
				case TagType.TagEnd:
					break;
				case TagType.TagByte:
					WriteByte(tag as TagByte);
					break;
				case TagType.TagShort:
					WriteShort(tag as TagShort);
					break;
				case TagType.TagInt:
					WriteInt(tag as TagInt);
					break;
				case TagType.TagLong:
					WriteLong(tag as TagLong);
					break;
				case TagType.TagFloat:
					WriteFloat(tag as TagFloat);
					break;
				case TagType.TagDouble:
					WriteDouble(tag as TagDouble);
					break;
				case TagType.TagByteArray:
					WriteByteArray(tag as TagByteArray);
					break;
				case TagType.TagString:
					WriteString(tag as TagString);
					break;
				case TagType.TagList:
					WriteList(tag as TagList);
					break;
				case TagType.TagCompound:
					WriteCompound(tag as TagCompound);
					break;
				case TagType.TagIntArray:
					WriteIntArray(tag as TagIntArray);
					break;
			}
		}

		private static void WriteByte(TagByte tag)
		{
			fstr.WriteByte(tag.Data);
		}

		private static void WriteShort(TagShort tag)
		{
			var buf = BitConverter.GetBytes(tag.Data);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 2);
		}

		private static void WriteInt(TagInt tag)
		{
			var buf = BitConverter.GetBytes(tag.Data);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 4);
		}

		private static void WriteLong(TagLong tag)
		{
			var buf = BitConverter.GetBytes(tag.Data);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 8);
		}

		private static void WriteFloat(TagFloat tag)
		{
			var buf = BitConverter.GetBytes(tag.Data);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 4);
		}

		private static void WriteDouble(TagDouble tag)
		{
			var buf = BitConverter.GetBytes(tag.Data);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 8);
		}

		private static void WriteByteArray(TagByteArray tag)
		{
			var buf = BitConverter.GetBytes(tag.Data.Length);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 4);
			fstr.Write(tag.Data, 0, tag.Data.Length);
		}

		private static void WriteString(TagString tag)
		{
			var buf = BitConverter.GetBytes((short)tag.Data.Length);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 2);

			var enc = Encoding.GetEncoding(28591);
			buf = enc.GetBytes(tag.Data);
			fstr.Write(buf, 0, buf.Length);
		}

		private static void WriteList(TagList tag)
		{
			var buf = BitConverter.GetBytes(tag.Count);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.WriteByte((byte)tag.ValueType);
			fstr.Write(buf, 0, 4);

			foreach(var t in tag.Tags)
			{
				WriteValue(t);
			}
		}

		private static void WriteCompound(TagCompound tag)
		{
			foreach(var item in tag.Tags)
			{
				WriteTag(item.Key, item.Value);
			}

			WriteTag(null, new TagEnd());
		}

		private static void WriteIntArray(TagIntArray tag)
		{
			var buf = BitConverter.GetBytes(tag.Data.Length);
			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(buf);
			}
			fstr.Write(buf, 0, 4);

			var data = new byte[tag.Data.Length * 4];
			for(var i = 0; i < tag.Data.Length; i++)
			{
				var buffer = BitConverter.GetBytes(tag.Data[i]);
				if(BitConverter.IsLittleEndian)
				{
					Array.Reverse(buffer);
				}
				Array.Copy(buffer, 0, data, i * 4, 4);
			}

			fstr.Write(data, 0, data.Length);
		}

		private static void WriteTag(string name, NbtTag tag)
		{
			fstr.WriteByte((byte)tag.TagType);

			if(tag.TagType == TagType.TagEnd) return;
			WriteString(name);
			WriteValue(tag);
		}
	}
}
