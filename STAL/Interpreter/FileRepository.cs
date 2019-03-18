using System;
using System.IO;

namespace STAL.Interpreter
{
	internal class FileRepository
	{
		Engine engine;

		public FileRepository(Engine engine)
		{
			this.engine = engine;
		}

		public StreamReader OpenFile(string filePath)
		{
			if (!filePath.ToLower().EndsWith(".stal"))
			{
				throw new ArgumentException(string.Format("{0} is not a STAL program. STAL programs must end with .stal", filePath));
			}
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(string.Format("Unable to find file at {0}", filePath));
			}

			return File.OpenText(filePath);
		}
	}
}
