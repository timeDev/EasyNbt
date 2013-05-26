using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;

namespace EasyNbt
{
	/// <summary>
	/// A basic NBT file
	/// </summary>
	public class NbtFile : IDisposable
	{
		private List<Stream> streams;
		/// <summary>
		/// The <see cref="CompressionType"/> used by the file
		/// </summary>
		public CompressionType Compression { get; private set; }
		/// <summary>
		/// The path of the file
		/// </summary>
		public string FileName { get; private set; }
		/// <summary>
		/// The main or root tag of this file.
		/// </summary>
		public TagCompound MainTag { get; set; }
		/// <summary>
		/// The name of the <see cref="MainTag"/>
		/// </summary>
		public string MainTagName { get; set; }
		/// <summary>
		/// Creates a new instance of the <see cref="NbtFile"/> class.
		/// </summary>
		/// <param name="path">The path to set</param>
		/// <param name="comp">The compression mode to use</param>
		public NbtFile(string path, CompressionType comp)
		{
			Compression = comp;
			FileName = path;
			streams = new List<Stream>();
		}

		/// <summary>
		/// Saves the file.
		/// </summary>
		public void Save()
		{
			if(MainTag != null)
				NbtSaver.Write(MainTag, MainTagName, GetWriteStream());
		}

		/// <summary>
		/// Loads the file
		/// </summary>
		public void Load()
		{
			string name;
			MainTag = NbtLoader.Read(GetReadStream(), out name);
			MainTagName = name;
		}

		/// <summary>
		/// Returns a Stream to read from using the files compression mode
		/// </summary>
		/// <returns>A Stream to read from</returns>
		public Stream GetReadStream()
		{
			return GetReadStream(Compression);
		}

		/// <summary>
		/// Returns a Stream to read from using the given compression mode
		/// </summary>
		/// <param name="compression">The compression mode to use</param>
		/// <returns>A Stream to read from</returns>
		private Stream GetReadStream(CompressionType compression)
		{
			try
			{
				var fstr = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				var length = fstr.Seek(0, SeekOrigin.End);
				fstr.Seek(0, SeekOrigin.Begin);

				var data = new byte[length];
				fstr.Read(data, 0, data.Length);

				fstr.Close();
				Stream ret;

				switch(compression)
				{
					case CompressionType.None:
						ret = new MemoryStream(data);
						break;
					case CompressionType.GZip:
						ret = new GZipStream(new MemoryStream(data), CompressionMode.Decompress);
						break;
					case CompressionType.Zlib:
						ret = new ZlibStream(new MemoryStream(data), CompressionMode.Decompress);
						break;
					case CompressionType.Deflate:
						ret = new DeflateStream(new MemoryStream(data), CompressionMode.Decompress);
						break;
					default:
						throw new ArgumentException("Invalid CompressionType specified", "compression");
				}
				streams.Add(ret);
				return ret;
			}
			catch(Exception ex)
			{
				throw new IOException("Failed to open compressed NBT data stream for input.", ex);
			}
		}

		/// <summary>
		/// Returns a Stream to write into using the files compression mode
		/// </summary>
		/// <returns>A Stream to write into</returns>
		public Stream GetWriteStream()
		{
			return GetWriteStream(Compression);
		}

		/// <summary>
		/// Returns a Stream to write into using the given compression mode
		/// </summary>
		/// <param name="compression">The compression mode to use</param>
		/// <returns>A Stream to write into</returns>
		private Stream GetWriteStream(CompressionType compression)
		{
			var fstr = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
			try
			{
				Stream ret;
				switch(compression)
				{
					case CompressionType.None:
						ret = fstr;
						break;
					case CompressionType.GZip:
						ret = new GZipStream(fstr, CompressionMode.Compress);
						break;
					case CompressionType.Zlib:
						ret = new ZlibStream(fstr, CompressionMode.Compress);
						break;
					case CompressionType.Deflate:
						ret = new DeflateStream(fstr, CompressionMode.Compress);
						break;
					default:
						throw new ArgumentException("Invalid CompressionType specified", "compression");
				}
				streams.Add(ret);
				return ret;
			}
			catch(Exception ex)
			{
				throw new IOException("Failed to initialize compressed NBT data stream for output.", ex);
			}
		}

		#region IDisposable Member

		/// <summary />
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		/// <summary />
		protected virtual void Dispose(bool disposing)
		{
			if(!disposing) return;
			foreach(var stream in streams)
			{
				stream.Dispose();
			}
			streams.Clear();
			streams = null;
		}
		/// <summary />
		~NbtFile()
		{
			Dispose(false);
		}

		#endregion
	}

	#region CompressionType
	/// <summary>
	/// The types of compression used by NBTFiles
	/// </summary>
	public enum CompressionType
	{
		/// <summary>
		/// No compression
		/// </summary>
		None,
		/// <summary>
		/// Zlib compression
		/// </summary>
		Zlib,
		/// <summary>
		/// Deflate compression
		/// </summary>
		Deflate,
		/// <summary>
		/// GZip compression
		/// </summary>
		GZip,
	}
	#endregion
}
