namespace Core.Interpreter.Productions
{
	// TODO: Make this into an interface and implement with each type of production
	// Interface should require returning of named identifiers and their type so we can perform static analysis
	// Implementations should also provide some way of recording parsing errors
	public class ProductionInfo
	{
		public string Data { get; set; }
		public ProductionInfo(string data)
		{
			Data = data;
		}
	}

	public enum ProductionType
	{
		Function,
		Variable,
		String,
	}
}