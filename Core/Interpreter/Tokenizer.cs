using System.Text;

namespace Core.Interpreter
{
	internal class Tokenizer
	{
		PeekBuffer input;
		StringBuilder buffer = new StringBuilder();

		const string NAME_STARTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		const string NAME_CHARS = NAME_STARTERS + DIGITS + "_";
		const string DIGITS = "0123456789.";
		const char COMMENT_STARTER = '/';

		public Tokenizer(PeekBuffer input)
		{
			this.input = input;
		}

		public Token GetToken()
		{
			char firstChar = input.Peek();
			while (!input.EndOfStream)
			{
				if (DIGITS.IndexOf(firstChar) >= 0)
				{
					return ReadNumber();
				}
				else if (NAME_STARTERS.IndexOf(firstChar) >= 0)
				{
					return ReadName();
				}
				else if (firstChar == COMMENT_STARTER)
				{
					CheckForComment();
				}
			}
			// TODO: Handle EndOfStream
			return new Token(TokenType.Undefined);
		}

		public Token ReadNumber()
		{
			while (!input.EndOfStream)
			{
				char nextChar = input.Peek();
				if (DIGITS.IndexOf(nextChar) >= 0)
				{
					buffer.Append(nextChar);
					input.Read();
				}
				else
				{
					break;
				}
			}
			string str = buffer.ToString();
			buffer.Clear();
			// TODO: Parsing
			return new Token(TokenType.Number, str);
		}

		public void CheckForComment()
		{
			while (!input.EndOfStream)
			{
				// TODO
				char nextChar = input.Peek();
			}
		}

		public Token ReadName()
		{
			while (!input.EndOfStream)
			{
				char nextChar = input.Peek();
				if (NAME_CHARS.IndexOf(nextChar) >= 0)
				{
					buffer.Append(nextChar);
					input.Read();
				}
				else
				{
					break;
				}
			}

			string str = buffer.ToString();
			buffer.Clear();
			return new Token(TokenType.Name, str);
		}
	}
}
