using System;
using System.IO;

namespace Core
{
	public class Engine
	{
		// TODO: Configurable for acceptable file extensions

		public Engine()
		{

		}

		public void LoadFiles(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				throw new DirectoryNotFoundException(string.Format("Unable to find directory at {0}", directoryPath));
			}

			foreach (var file in Directory.EnumerateFiles(directoryPath))
			{
				if (file.ToLower().EndsWith(".stal"))
				{
					LoadFiles(file);
				}
			}
		}

		public void LoadFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(string.Format("Unable to find file at {0}", filePath));
			}
			if (!filePath.ToLower().EndsWith(".stal"))
			{
				throw new ArgumentException(string.Format("{0} is not a STAL program. STAL programs must end with .stal", filePath));
			}

		}
	}
}
