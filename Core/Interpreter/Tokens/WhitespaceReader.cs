namespace Core.Interpreter.Tokens
{
	public class WhiteSpaceReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return char.IsWhiteSpace(c);
		}

		public bool CheckToken(PeekBuffer<char> buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer<char> buffer)
		{
			while (buffer.TryPeek(out var nextChar))
			{
				if (char.IsWhiteSpace(nextChar))
				{
					buffer.TryRead(out _);
				}
				else break;
			}
			return null;
		}
	}
}
