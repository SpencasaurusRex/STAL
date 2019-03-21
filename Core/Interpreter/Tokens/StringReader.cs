using System.Text;

namespace Core.Interpreter.Tokens
{
	public class StringTokenReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return c == '"' || c == '\'';
		}

		public bool CheckToken(PeekBuffer buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			char startingChar = buffer.Read();
			StringBuilder str = new StringBuilder();
			while (!buffer.EndOfStream)
			{
				char nextChar = buffer.Peek();
				if (nextChar == startingChar)
				{
					buffer.Read();
					break;
				}
				str.Append(buffer.Read());
			}
			return new TokenInfo(TokenType.String, str.ToString());
		}
	}
}
