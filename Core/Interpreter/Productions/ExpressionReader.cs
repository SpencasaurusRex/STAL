using STAL.Core.Interpreter.Tokens;

namespace STAL.Core.Interpreter.Productions
{
	/// <summary>
	/// An expression can be:
	///		A function
	///		A variable
	///		A string
	///		A number
	/// </summary>
	public class ExpressionReader : AggregateReader
	{
		public ExpressionReader() : base(
			new IProductionReader[] {
				new FunctionReader(),
				new VariableReader(),
				new NumberReader(),
				new StringProductionReader()
			})
		{
		}
	}
}
