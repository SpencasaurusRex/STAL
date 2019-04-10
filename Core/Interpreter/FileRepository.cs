using System;
using System.Collections.Generic;
using System.IO;

namespace STAL.Core.Interpreter
{
	class FileRepository
	{
		public PeekBuffer<char> OpenFile(string filePath)
		{
			CheckFile(filePath);
            var enumerator = GetCharEnumerator(filePath);
            return new PeekBuffer<char>(enumerator);
        }

        static IEnumerator<char> GetCharEnumerator(string filePath)
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                while (!reader.EndOfStream)
                {
                    yield return (char) reader.Read();
                }
            }
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
