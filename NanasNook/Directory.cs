using System;
using System.IO;

namespace NanasNook
{
	public class Directory
	{
		private string _path;

		private Directory(string path)
		{
			_path = path;
		}

		public static Directory ApplicationData
		{
			get
			{
				return new Directory(Environment.GetFolderPath(
					Environment.SpecialFolder.ApplicationData));
			}
		}

		public static Directory CommonApplicationData
		{
			get
			{
				return new Directory(Environment.GetFolderPath(
					Environment.SpecialFolder.CommonApplicationData));
			}
		}

		public static Directory operator /(Directory root, string folder)
		{
			return new Directory(Path.Combine(root._path, folder));
		}

		public static implicit operator string(Directory directory)
		{
			return directory._path;
		}
	}
}
