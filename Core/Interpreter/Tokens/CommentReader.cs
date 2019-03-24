namespace Core.Interpreter.Tokens
{
	public class CommentReader : ITokenReader
	{
		public bool IsStartingChar(char c)
		{
			return c == '/';
		}

		public bool CheckToken(PeekBuffer<char> buffer)
		{
			return buffer.TryPeek(out var first) && 
                   buffer.TryPeek(1, out var second) && 
                   first == second && 
                   first == '/';
        }

		public TokenInfo ReadToken(PeekBuffer<char> buffer)
		{
			buffer.TryRead(out _);
			buffer.TryRead(out _);
			bool nextLineSeen = false;
			while (buffer.TryPeek(out var nextChar))
            {
                if (nextChar == '\r' || nextChar == '\n')
				{
					nextLineSeen = true;
				}
				else if (nextLineSeen)
				{
					break;
				}
				buffer.TryRead(out _);
			}
			return null;
		}
	}
}
