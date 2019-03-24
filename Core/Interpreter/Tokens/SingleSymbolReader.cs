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

		public bool CheckToken(PeekBuffer buffer)
		{
			return true;
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			buffer.Read();
			return new TokenInfo(type);
		}
	}
}
