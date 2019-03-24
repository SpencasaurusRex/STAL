using System.Text;

namespace Core.Interpreter.Tokens
{
	public class NumberReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return char.IsDigit(c) || c == '.';
		}

		public bool CheckToken(PeekBuffer<char> buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer<char> buffer)
		{
			bool seenDecimal = false;
			StringBuilder number = new StringBuilder();

			while (buffer.TryPeek(out var nextChar))
			{
                if (char.IsDigit(nextChar))
                {
                    number.Append(nextChar);
                    buffer.TryRead(out _);
				}
				else if (!seenDecimal && nextChar == '.')
				{
					seenDecimal = true;
					number.Append(nextChar);
                    buffer.TryRead(out _);
                }
				else
				{
					break;
				}
			}

			return new TokenInfo(TokenType.Number, double.Parse(number.ToString()));
		}
	}
}