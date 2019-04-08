using Core.Interpreter.Tokens;

namespace Core.Interpreter.Productions
{
	/// <summary>
	/// An expression can be:
	///		A function
	///		A variable
	///		A string
	/// </summary>
	public class ExpressionReader : IProductionReader
	{
		FunctionReader functionReader;
		FunctionReader FunctionReader
		{
			get
			{
				if (functionReader == null) functionReader = new FunctionReader();
				return functionReader;
			}
		}

		VariableReader variableReader = new VariableReader();
		VariableReader VariableReader
		{
			get
			{
				if (variableReader == null) variableReader = new VariableReader();
				return variableReader;
			}
		}

		StringReader stringReader = new StringReader();
		StringReader StringReader
		{
			get
			{
				if (stringReader == null) stringReader = new StringReader();
				return stringReader;
			}
		}

		ProductionType type;

		public bool FirstToken(TokenInfo token)
		{
			return FunctionReader.FirstToken(token) || VariableReader.FirstToken(token) || StringReader.FirstToken(token);
		}

		public bool IsProduction(PeekBuffer<TokenInfo> tokenStream, int index = 0)
		{
			if (FunctionReader.IsProduction(tokenStream, index))
			{
				type = ProductionType.Function;
				return true;
			}
			if (VariableReader.IsProduction(tokenStream, index))
			{
				type = ProductionType.Variable;
				return true;
			}
			if (StringReader.IsProduction(tokenStream, index))
			{
				type = ProductionType.String;
				return true;
			}
			return false;
		}

		public ProductionInfo ReadProduction(PeekBuffer<TokenInfo> tokenStream)
		{
			if (type == ProductionType.Function) return FunctionReader.ReadProduction(tokenStream);
			if (type == ProductionType.Variable) return VariableReader.ReadProduction(tokenStream);
			if (type == ProductionType.String) return StringReader.ReadProduction(tokenStream);
			return null;
		}
	}
}
