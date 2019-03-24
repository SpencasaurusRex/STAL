using System.Text;

namespace Core.Interpreter.Tokens
{
	public class NameReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return char.IsLetter(c);
		}

		public bool CheckToken(PeekBuffer<char> buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer<char> buffer)
		{
			StringBuilder name = new StringBuilder();

			while (buffer.TryPeek(out var nextChar))
			{
                if (char.IsLetterOrDigit(nextChar))
				{
					name.Append(buffer.TryRead(out _));
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
