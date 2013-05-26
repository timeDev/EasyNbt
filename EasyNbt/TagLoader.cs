using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyNbt
{
	/// <summary>
	/// Converts tags into values using reflection
	/// </summary>
	public static class TagLoader
	{
		public static void LoadAll(TagCompound source, object target)
		{
			Type t = target.GetType();
			foreach(var field in t.GetFields())
				foreach(var atr in field.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.ReadOnly) != TagOptions.ReadOnly)
						AssignValue(field, source[atr.Name], target);

			foreach(var method in t.GetMethods())
				foreach(var atr in method.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.ReadOnly) != TagOptions.ReadOnly)
						AssignValue(method, source[atr.Name], target);

			foreach(var prop in t.GetProperties())
				foreach(var atr in prop.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.ReadOnly) != TagOptions.ReadOnly)
						AssignValue(prop, source[atr.Name], target);
		}

		public static bool ValidateAll(TagCompound source, object target)
		{
			Type t = target.GetType();
			foreach(var field in t.GetFields())
				foreach(var atr in field.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.Optional) != TagOptions.Optional)
					{
						if(!source.Contains(atr.Name))
							return false;
						if(!Validate(field, source[atr.Name]))
							return false;
					}

			foreach(var method in t.GetMethods())
				foreach(var atr in method.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.Optional) != TagOptions.Optional && (atr.Options & TagOptions.WriteOnly) != TagOptions.WriteOnly)
					{
						if(!source.Contains(atr.Name))
							return false;
						if(!Validate(method, source[atr.Name]))
							return false;
					}

			foreach(var prop in t.GetProperties())
				foreach(var atr in prop.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.Optional) != TagOptions.Optional)
					{
						if(!source.Contains(atr.Name))
							return false;
						if(!Validate(prop, source[atr.Name]))
							return false;
					}

			return true;
		}

		private static bool Validate(MethodInfo method, NbtTag tag)
		{
			return method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == GetTagType(tag);
		}

		private static bool Validate(PropertyInfo prop, NbtTag tag)
		{
			return prop.PropertyType == GetTagType(tag);
		}

		private static bool Validate(FieldInfo field, NbtTag tag)
		{
			return field.FieldType == GetTagType(tag);
		}

		private static Type GetTagType(NbtTag tag)
		{
			switch(tag.TagType)
			{
				case TagType.TagByte:
					return typeof(byte);
				case TagType.TagShort:
					return typeof(short);
				case TagType.TagInt:
					return typeof(int);
				case TagType.TagLong:
					return typeof(long);
				case TagType.TagFloat:
					return typeof(float);
				case TagType.TagDouble:
					return typeof(double);
				case TagType.TagByteArray:
					return typeof(byte[]);
				case TagType.TagString:
					return typeof(string);
				case TagType.TagList:
					return typeof(TagList);
				case TagType.TagCompound:
					return typeof(TagCompound);
				case TagType.TagIntArray:
					return typeof(int[]);
				default:
					throw new ArgumentException("Not a valid TagType", "tag");
			}
		}

		private static void AssignValue(PropertyInfo prop, NbtTag tag, object target)
		{
			prop.SetValue(target, GetTagValue(tag));
		}

		private static void AssignValue(MethodInfo method, NbtTag tag, object target)
		{
			method.Invoke(target, new object[] { GetTagValue(tag) });
		}

		private static void AssignValue(FieldInfo field, NbtTag tag, object target)
		{
			field.SetValue(target, GetTagValue(tag));
		}

		private static dynamic GetTagValue(NbtTag tag)
		{
			switch(tag.TagType)
			{
				case TagType.TagByte:
					return ((TagByte)tag).Data;
				case TagType.TagShort:
					return ((TagShort)tag).Data;
				case TagType.TagInt:
					return ((TagInt)tag).Data;
				case TagType.TagLong:
					return ((TagLong)tag).Data;
				case TagType.TagFloat:
					return ((TagFloat)tag).Data;
				case TagType.TagDouble:
					return ((TagDouble)tag).Data;
				case TagType.TagByteArray:
					return ((TagByteArray)tag).Data;
				case TagType.TagString:
					return ((TagString)tag).Data;
				case TagType.TagList:
					return ((TagList)tag).Clone();
				case TagType.TagCompound:
					return ((TagCompound)tag).Clone();
				case TagType.TagIntArray:
					return ((TagIntArray)tag).Data;
				default:
					throw new ArgumentException("Not a valid TagType", "tag");
			}
		}
	}
}
