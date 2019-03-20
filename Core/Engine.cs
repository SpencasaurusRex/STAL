using Core.Interpreter;
using Core.Interpreter.Tokens;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
	public class Engine
	{
		// TODO: Configurable for acceptable file extensions

		public Engine()
		{
			// TODO: Have IoC for entire interpreter module too

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
					LoadFile(file);
				}
			}
		}

		// TODO: This and LoadFiles() should probably be in a separate file in the interpreter folder
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

			PeekBuffer buffer = new PeekBuffer(File.OpenText(filePath));

			// TODO: Add token reader configuration
			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			tokenReaders.Add(new NameReader());
			tokenReaders.Add(new NumberReader());
			tokenReaders.Add(new SingleSymbolReader(':', TokenType.Colon));
			tokenReaders.Add(new SingleSymbolReader(',', TokenType.Comma));
			tokenReaders.Add(new SingleSymbolReader('(', TokenType.LeftParen));
			tokenReaders.Add(new SingleSymbolReader(')', TokenType.RightParen));
			tokenReaders.Add(new SingleSymbolReader('"', TokenType.Quote));

			Tokenizer tokenizer = new Tokenizer(buffer, tokenReaders);
		}
	}
}
