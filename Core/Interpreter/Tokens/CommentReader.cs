namespace Core.Interpreter.Tokens
{
	public class CommentReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return c == '/';
		}

		public bool CheckToken(PeekBuffer buffer)
		{
			var first = buffer.Peek();
			var second = buffer.Peek(1);
			return first == second && first == '/';
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			buffer.Read();
			buffer.Read();
			bool nextLineSeen = false;
			while (!buffer.EndOfStream)
			{
				char nextChar = buffer.Peek();
				if (nextChar == '\r' || nextChar == '\n')
				{
					nextLineSeen = true;
				}
				else if (nextLineSeen)
				{
					break;
				}
				buffer.Read();
			}
			return null;
		}
	}
}
