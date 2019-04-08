using Core.Interpreter;
using Core.Interpreter.Productions;
using Core.Interpreter.Tokens;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
	public class Engine
	{
		// TODO: Configurable for acceptable file extensions
		readonly FileRepository fileRepository;

		public Engine()
		{
			// TODO: Have IoC configuration for entire interpreter module too
			fileRepository = new FileRepository();
		}

		public void LoadFiles(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				throw new DirectoryNotFoundException($"Unable to find directory at {directoryPath}");
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
            var file = fileRepository.OpenFile(filePath);

			// TODO: Add token reader configuration
            List<ITokenReader> tokenReaders = StandardTokenReaders();
            Tokenizer tokenizer = new Tokenizer(file, tokenReaders);

			// TODO: Add production reader configuration
			List<IProductionReader> productionReaders = StandardProductionReaders();
			Parser parser = new Parser(new PeekBuffer<TokenInfo>(tokenizer), productionReaders);

			foreach (var productionInfo in parser.Parse())
			{
				Console.WriteLine(productionInfo);
			}
			// TODO: Finish
		}

        static List<ITokenReader> StandardTokenReaders()
        {
            return new List<ITokenReader>
            {
                new WhiteSpaceReader(),
                new NameReader(),
                new NumberReader(),
                new StringTokenReader(),
                new CommentReader(),
                new SingleSymbolReader(':', TokenType.Colon),
                new SingleSymbolReader(',', TokenType.Comma),
                new SingleSymbolReader('(', TokenType.LeftParen),
                new SingleSymbolReader(')', TokenType.RightParen)
            };
        }

		static List<IProductionReader> StandardProductionReaders()
		{
			return new List<IProductionReader>
			{
				new FunctionReader(),
			};
		}
    }
}
