using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNbt
{
	public static class TagSaver
	{
		public static TagCompound SaveAll(object source)
		{
			var ret = new TagCompound();
			var t = source.GetType();
			foreach(var field in t.GetFields())
				foreach(var atr in field.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.WriteOnly) != TagOptions.WriteOnly)
						ret.Add(atr.Name, GetValueTag(field.GetValue(source)));

			foreach(var method in t.GetMethods())
				foreach(var atr in method.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.WriteOnly) != TagOptions.WriteOnly)
						ret.Add(atr.Name, GetValueTag(method.Invoke(source, null)));

			foreach(var prop in t.GetProperties())
				foreach(var atr in prop.GetCustomAttributes(typeof(NbtTagAttribute), false).Cast<NbtTagAttribute>())
					if((atr.Options & TagOptions.WriteOnly) != TagOptions.WriteOnly)
						ret.Add(atr.Name, GetValueTag(prop.GetValue(source)));

			return ret;
		}

		private static NbtTag GetValueTag(object data)
		{
			if(data is byte)
				return new TagByte { Data = (byte)data };
			if(data is short)
				return new TagShort { Data = (short)data };
			if(data is int)
				return new TagInt { Data = (int)data };
			if(data is long)
				return new TagLong { Data = (long)data };
			if(data is float)
				return new TagFloat { Data = (float)data };
			if(data is double)
				return new TagDouble { Data = (double)data };
			if(data is string)
				return new TagString { Data = (string)data };
			if(data is byte[])
				return new TagByteArray { Data = (byte[])data };
			if(data is int[])
				return new TagIntArray { Data = (int[])data };
			if(data is TagCompound)
				return ((TagCompound)data).Clone();
			if(data is TagList)
				return ((TagList)data).Clone();
			if(data is IDictionary<string, NbtTag>)
				return new TagCompound((IDictionary<string, NbtTag>)data);
			throw new ArgumentException("Invalid data type", "data");
		}
	}
}
