using System.Text;

namespace Core.Interpreter.Tokens
{
	public class NameReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return char.IsLetter(c);
		}

		public bool CheckToken(PeekBuffer buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			StringBuilder name = new StringBuilder();

			while (!buffer.EndOfStream)
			{
				char nextChar = buffer.Peek();

				if (char.IsLetterOrDigit(nextChar))
				{
					name.Append(buffer.Read());
				}
				else
				{
					break;
				}
			}

			return new TokenInfo(TokenType.Name, name.ToString());
		}
	}
}
