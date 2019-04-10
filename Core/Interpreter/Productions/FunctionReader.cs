using STAL.Core.Interpreter.Tokens;
using System;
using System.Text;

namespace STAL.Core.Interpreter.Productions
{
	public class FunctionReader : IProductionReader
	{
		ExpressionReader expressionReader;
		ExpressionReader ExpressionReader
		{
			get
			{
				if (expressionReader == null)
					expressionReader = new ExpressionReader();
				return expressionReader;
			}
		}

		public bool FirstToken(TokenInfo token)
		{
			return token.Type == TokenType.Name;
		}

		public bool IsProduction(PeekBuffer<TokenInfo> tokenStream, int index = 0)
		{
			return tokenStream.TryPeek(index++, out var first) && first.Type == TokenType.Name &&
				tokenStream.TryPeek(index, out var second) && second.Type == TokenType.LeftParen;
		}

		public ProductionInfo ReadProduction(PeekBuffer<TokenInfo> tokenStream)
		{
			StringBuilder data = new StringBuilder();
			tokenStream.TryRead(out var nameToken);
			tokenStream.TryRead(out _);
			data.Append(nameToken.Data.ToString());
			data.Append("(");
			
			while (tokenStream.TryPeek(out var next))
			{
				if (next.Type == TokenType.RightParen)
				{
					tokenStream.TryRead(out _);
					data.Append(")");
					return new ProductionInfo(data.ToString());
				}

				// Read next token
				if (!ExpressionReader.FirstToken(next)) return null;
				if (!ExpressionReader.IsProduction(tokenStream)) return null;
				var expression = ExpressionReader.ReadProduction(tokenStream);

				if (expression == null) return null;
				data.Append(expression.Data);

				// The token after an expression should be comma or right-paren
				if (!tokenStream.TryPeek(out next))
				{
					return null;
				}
				if (next.Type == TokenType.RightParen)
				{
					continue;
				}
				if (next.Type == TokenType.Comma)
				{
					tokenStream.TryRead(out _);
				}
			}

			return null;
		}
	}
}
