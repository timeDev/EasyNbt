using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyNbt
{
	/// <summary>
	/// Converts NBT data into text.
	/// </summary>
	public static class NbtWriter
	{
		/// <summary>
		/// Converts a <see cref="TagCompound"/> into a string.
		/// </summary>
		/// <param name="tag">The tag to convert</param>
		/// <param name="name">The name of the tag</param>
		/// <returns>The converted string with linebreaks and tab spacing</returns>
		public static string NbtString(TagCompound tag, string name)
		{
			var bld = new StringBuilder();
			foreach(var item in NbtStringArr(tag, name))
			{
				bld.AppendLine(item);
			}
			return bld.ToString();
		}
		/// <summary>
		/// Converts a <see cref="TagCompound"/> into a string array.
		/// </summary>
		/// <param name="tag">The tag to convert</param>
		/// <param name="name">The name of the tag</param>
		/// <returns>All the lines of the converted tag with tab spacing</returns>
		public static string[] NbtStringArr(TagCompound tag, string name)
		{
			if(tag == null)
				throw new ArgumentNullException("tag", "tag is null.");
			var ret = new List<string> { "TagCompound(" + name + ") : " + tag.Tags.Count() + " entries", "{" };

			foreach(var item in tag.Tags.Select(p => p.Key))
			{
				ret.AddRange(GetStrings(tag[item], item).Select(sitem => "\t" + sitem));
			}
			ret.Add("}");
			return ret.ToArray();
		}

		private static IEnumerable<string> GetStrings(NbtTag tag, string name)
		{
			var ret = new List<string>();
			switch(tag.TagType)
			{
				case TagType.TagByte:
					ret.Add(String.Format("TagByte({0}) : {1}", name, ((TagByte)tag).Data));
					break;
				case TagType.TagShort:
					ret.Add(String.Format("TagShort({0}) : {1}", name, ((TagShort)tag).Data));
					break;
				case TagType.TagInt:
					ret.Add(String.Format("TagInt({0}) : {1}", name, ((TagInt)tag).Data));
					break;
				case TagType.TagLong:
					ret.Add(String.Format("TagLong({0}) : {1}", name, ((TagLong)tag).Data));
					break;
				case TagType.TagFloat:
					ret.Add(String.Format("TagFloat({0}) : {1}", name, ((TagFloat)tag).Data));
					break;
				case TagType.TagDouble:
					ret.Add(String.Format("TagDouble({0}) : {1}", name, ((TagDouble)tag).Data));
					break;
				case TagType.TagByteArray:
					ret.Add(String.Format("TagByteAray({0}) : [{1} entries]", name, ((TagByteArray)tag).Data.Length));
					break;
				case TagType.TagString:
					ret.Add(String.Format("TagString({0}) : {1}", name, ((TagString)tag).Data));
					break;
				case TagType.TagList:
					ret.Add(String.Format("TagList({0}) : {1} entries of type {2}", name, ((TagList)tag).Count, (tag as TagList).ValueType));
					ret.Add("{");
					foreach(var listitem in ((TagList)tag).Tags)
					{
						ret.AddRange(GetStrings(listitem, "[none]").Select(sitem => "\t" + sitem));
					}
					ret.Add("}");
					break;
				case TagType.TagCompound:
					ret.AddRange(NbtStringArr(tag as TagCompound, name));
					break;
				case TagType.TagIntArray:
					ret.Add(String.Format("TagIntAray({0}) : [{1} entries]", name, ((TagIntArray)tag).Data.Length));
					break;
			}
			return ret;
		}
	}
}
