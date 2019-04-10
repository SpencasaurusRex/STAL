using STAL.Core.Interpreter.Tokens;

namespace STAL.Core.Interpreter.Productions
{
	public class StringProductionReader : IProductionReader
	{
		public bool FirstToken(TokenInfo token)
		{
			return token.Type == TokenType.String;
		}

		public bool IsProduction(PeekBuffer<TokenInfo> tokenStream, int index = 0)
		{
			return true;
		}

		public ProductionInfo ReadProduction(PeekBuffer<TokenInfo> tokenStream)
		{
			tokenStream.TryRead(out var token);
			return new ProductionInfo(token.Data);
		}
	}
}
