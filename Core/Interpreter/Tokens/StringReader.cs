using System.Text;

namespace STAL.Core.Interpreter.Tokens
{
	public class StringTokenReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return c == '"' || c == '\'';
		}

		public bool CheckToken(PeekBuffer<char> buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer<char> buffer)
		{
			buffer.TryRead(out var startingChar);
			StringBuilder str = new StringBuilder();
			while (buffer.TryRead(out var nextChar))
			{
				if (nextChar == startingChar)
				{
					break;
				}
				str.Append(nextChar);
			}
			return new TokenInfo(TokenType.String, str.ToString());
		}
	}
}
