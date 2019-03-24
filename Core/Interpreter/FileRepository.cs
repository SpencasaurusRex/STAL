using System;
using System.IO;

namespace Core.Interpreter
{
	class FileRepository
	{
		public StreamReader OpenFile(string filePath)
		{
			CheckFile(filePath);
			return File.OpenText(filePath);
		}

		public void CheckFile(string filePath)
		{
			if (!ValidFileExtension(filePath))
			{
				throw new ArgumentException($"{filePath} is not a STAL program. STAL programs must end with .stal");
			}
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException($"Unable to find file at {filePath}");
			}
		}

		public bool ValidFileExtension(string filePath)
		{
			return filePath.ToLower().EndsWith(".stal");
		}
	}
}
