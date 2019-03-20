using System;

namespace Core.Interpreter.Tokens
{
	public class WhiteSpaceReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return char.IsWhiteSpace(c);
		}

		public bool CheckToken(PeekBuffer buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			while (!buffer.EndOfStream)
			{
				char nextChar = buffer.Peek();
				if (char.IsWhiteSpace(nextChar))
				{
					buffer.Read();
				}
				else break;
			}
			return null;
		}
	}
}
