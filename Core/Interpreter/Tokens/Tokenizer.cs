using System.Collections.Generic;
using System.IO;

namespace Core.Interpreter.Tokens
{
    class Tokenizer
	{
        readonly PeekBuffer input;
        readonly List<ITokenReader> tokenReaders;

		public Tokenizer(PeekBuffer input, List<ITokenReader> tokenReaders)
		{
			this.input = input;
			this.tokenReaders = tokenReaders;
		}

		public TokenInfo GetToken()
		{
			if (EndOfFile)
			{
				throw new EndOfStreamException();
			}

		readers:
			char firstChar = input.Peek();
			foreach (var reader in tokenReaders)
            {
                if (!reader.IsStartingChar(firstChar) || !reader.CheckToken(input))
                {
                    continue;
                }

                var output = reader.ReadToken(input);
                if (output == null) goto readers;
                return output;
            }
			return new TokenInfo(TokenType.Undefined);
		}

		public bool EndOfFile => input.EndOfStream;
	}
}
