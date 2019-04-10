using STAL.Core.Interpreter.Tokens;
using System;

namespace STAL.Core.Interpreter.Productions
{
	public class AggregateReader : IProductionReader
	{
		IProductionReader[] productionReaders;
		bool[] validReaders;
		int confirmedReader = -1;

		public AggregateReader(IProductionReader[] readers)
		{
			productionReaders = readers;
			validReaders = new bool[readers.Length];
		}

		public bool FirstToken(TokenInfo token)
		{
			bool ret = false;
			for (int i = 0; i < productionReaders.Length; i++)
			{
				validReaders[i] = productionReaders[i].FirstToken(token);
				if (validReaders[i]) ret = true;
			}
			return ret;
		}

		public bool IsProduction(PeekBuffer<TokenInfo> tokenStream, int index = 0)
		{
			for (int i = 0; i < productionReaders.Length; i++)
			{
				if (validReaders[i] && productionReaders[i].IsProduction(tokenStream))
				{
					confirmedReader = i;
					return true;
				}
			}
			return false;
		}

		public ProductionInfo ReadProduction(PeekBuffer<TokenInfo> tokenStream)
		{
			return productionReaders[confirmedReader].ReadProduction(tokenStream);
		}
	}
}
