namespace Core.Interpreter.Tokens
{
	public class SingleSymbolReader : ITokenReader
	{
        readonly char symbol;
        readonly TokenType type;

		public SingleSymbolReader(char symbol, TokenType type)
		{
			this.symbol = symbol;
			this.type = type;
		}

		public bool IsStartingChar(char c)
		{
			return c == symbol;
		}

		public bool CheckToken(PeekBuffer<char> buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer<char> buffer)
		{
			buffer.TryRead(out _);
			return new TokenInfo(type);
		}
	}
}
