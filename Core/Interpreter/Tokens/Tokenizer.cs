using System.Collections;
using System.Collections.Generic;

namespace STAL.Core.Interpreter.Tokens
{
    public class Tokenizer : IEnumerator<TokenInfo>
	{
        readonly PeekBuffer<char> input;
        readonly List<ITokenReader> tokenReaders;

		public Tokenizer(PeekBuffer<char> input, List<ITokenReader> tokenReaders)
		{
			this.input = input;
			this.tokenReaders = tokenReaders;
		}

		bool TryGetToken(out TokenInfo tokenInfo)
		{
            tokenInfo = null;
		readers:
            if (!input.TryPeek(out var firstChar))
            {
                return false;
            }
			foreach (var reader in tokenReaders)
            {
                if (!reader.IsStartingChar(firstChar) || !reader.CheckToken(input))
                {
                    continue;
                }

                var output = reader.ReadToken(input);
                if (output == null) goto readers;
                tokenInfo = output;
                return true;
            }
			tokenInfo = new TokenInfo(TokenType.Undefined);
            return false;
        }

        public void Dispose()
        {    
        }

        public bool MoveNext()
        {
            if (TryGetToken(out var next))
            {
                Current = next;
                return true;
            }
            return false;
        }

        public void Reset()
        {
        }

        public TokenInfo Current { get; private set; }

        object IEnumerator.Current => Current;
    }
}
