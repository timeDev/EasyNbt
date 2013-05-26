using System;
namespace EasyNbt
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public sealed class NbtTagAttribute : Attribute
	{
		readonly string name;

		public NbtTagAttribute(string name)
		{
			this.name = name;
			Options = TagOptions.None;
		}

		public string Name { get { return name; } }
		public TagOptions Options { get; set; }
	}

	[Flags]
	public enum TagOptions
	{
		None = 0,
		Optional = 1,
		ReadOnly = 2,
		WriteOnly = 4,
	}
}