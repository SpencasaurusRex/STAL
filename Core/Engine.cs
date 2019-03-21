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
		FileRepository fileRepository;

		public Engine()
		{
			// TODO: Have IoC configuration for entire interpreter module too
			fileRepository = new FileRepository(this);
		}

		public void LoadFiles(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				throw new DirectoryNotFoundException(string.Format("Unable to find directory at {0}", directoryPath));
			}

			foreach (var file in Directory.EnumerateFiles(directoryPath))
			{
				if (fileRepository.ValidFileExtension(file))
				{
					LoadFile(file);
				}
			}
		}

		// TODO: This and LoadFiles() should probably be in a separate file in the interpreter folder
		public void LoadFile(string filePath)
		{
			PeekBuffer buffer = new PeekBuffer(fileRepository.OpenFile(filePath));

			// TODO: Add token reader configuration
			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			tokenReaders.Add(new WhiteSpaceReader());
			tokenReaders.Add(new NameReader());
			tokenReaders.Add(new NumberReader());
			tokenReaders.Add(new StringTokenReader());
			tokenReaders.Add(new SingleSymbolReader(':', TokenType.Colon));
			tokenReaders.Add(new SingleSymbolReader(',', TokenType.Comma));
			tokenReaders.Add(new SingleSymbolReader('(', TokenType.LeftParen));
			tokenReaders.Add(new SingleSymbolReader(')', TokenType.RightParen));

			Tokenizer tokenizer = new Tokenizer(buffer, tokenReaders);
			while (!tokenizer.EndOfFile)
			{
				var token = tokenizer.GetToken();
				Console.WriteLine(string.Format("{0} {1}", token.type, token.data ?? ""));
				if (token.type == TokenType.Undefined) break;
			}
		}
	}
}
