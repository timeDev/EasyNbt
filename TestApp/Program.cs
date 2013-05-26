using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNbt;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var pgm = new Program();
			var tag1 = PopulateTag();
			Console.WriteLine("Validation: {0}", TagLoader.ValidateAll(tag1, pgm));
			TagLoader.LoadAll(tag1, pgm);
			Console.WriteLine("Loaded all tags");
			var tag2 = TagSaver.SaveAll(pgm);
			Console.WriteLine("Saved all tags");
			Console.WriteLine("Tags are same: {0}", TagCompound.Equals(tag1, tag2));
			Console.ReadKey();
		}

		private static TagCompound PopulateTag()
		{
			var ret = new TagCompound();
			ret.Add("mpubl", new TagByte { Data = 101 });
			ret.Add("mprot", new TagByte { Data = 101 });
			ret.Add("mpriv", new TagByte { Data = 101 });

			ret.Add("fpubl", new TagByte { Data = 101 });
			ret.Add("fprot", new TagByte { Data = 101 });
			ret.Add("fpriv", new TagByte { Data = 101 });

			ret.Add("ppubl", new TagByte { Data = 101 });
			ret.Add("pprot", new TagByte { Data = 101 });
			ret.Add("ppriv", new TagByte { Data = 101 });
			return ret;
		}

		[NbtTag("mpubl", Options = TagOptions.WriteOnly)]
		public void SetTagPublic(byte tag)
		{
			Console.WriteLine("SetTagPublic: {0}", tag);
		}

		[NbtTag("mprot", Options = TagOptions.WriteOnly)]
		protected void SetTagProtected(byte tag)
		{
			Console.WriteLine("SetTagProtected: {0}", tag);
		}

		[NbtTag("mpriv", Options = TagOptions.WriteOnly)]
		private void SetTagPrivate(byte tag)
		{
			Console.WriteLine("SetTagPrivate: {0}", tag);
		}
		
		[NbtTag("mpubl", Options = TagOptions.ReadOnly)]
		public byte GetTagPublic()
		{
			return 101;
		}

		[NbtTag("mprot", Options = TagOptions.ReadOnly)]
		public byte GetTagProtected()
		{
			return 101;
		}

		[NbtTag("mpriv", Options = TagOptions.ReadOnly)]
		private byte GetTagPrivate()
		{
			return 101;
		}

		[NbtTag("fpriv")]
		private byte fieldTagPrivate;

		[NbtTag("fpubl")]
		public byte fieldTagPublic;

		[NbtTag("fprot")]
		protected byte fieldTagProtected;
		
		[NbtTag("ppubl")]
		public byte PropTagPublic
		{
			get { return 101; }
			set { Console.WriteLine("PropTagPublic: {0}", value); }
		}

		[NbtTag("pprot")]
		protected byte PropTagProtected
		{
			get { return 101; }
			set { Console.WriteLine("PropTagProtected: {0}", value); }
		}

		[NbtTag("ppriv")]
		private byte PropTagPrivate
		{
			get { return 101; }
			set { Console.WriteLine("PropTagPrivate: {0}", value); }
		}
	}
}
