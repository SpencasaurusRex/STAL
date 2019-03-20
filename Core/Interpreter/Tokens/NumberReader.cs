using System.Text;

namespace Core.Interpreter.Tokens
{
	public class NumberReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return char.IsDigit(c) || c == '.';
		}

		public bool CheckToken(PeekBuffer buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			bool seenDecimal = false;
			StringBuilder number = new StringBuilder();

			while (!buffer.EndOfStream)
			{
				char nextChar = buffer.Peek();

				if (char.IsDigit(nextChar))
				{
					number.Append(buffer.Read());
				}
				else if (!seenDecimal && nextChar == '.')
				{
					seenDecimal = true;
					number.Append(buffer.Read());
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
