using STAL.Core.Interpreter.Productions;
using STAL.Core.Interpreter.Tokens;
using System.Collections.Generic;

namespace STAL.Core.Interpreter
{
	public class Parser
	{
		readonly PeekBuffer<TokenInfo> tokens;
		readonly List<IProductionReader> productionReaders;

		public Parser(PeekBuffer<TokenInfo> tokens, List<IProductionReader> productionReaders)
		{
			this.tokens = tokens;
			this.productionReaders = productionReaders;
		}

		public List<ProductionInfo> Parse()
		{
			List<ProductionInfo> results = new List<ProductionInfo>();

			while (tokens.TryPeek(out var token))
			{
				foreach (var reader in productionReaders)
				{
					if (!reader.FirstToken(token)) continue;
					if (!reader.IsProduction(tokens)) continue;
					results.Add(reader.ReadProduction(tokens));
				}
			}

			return results;
		}
	}
}
