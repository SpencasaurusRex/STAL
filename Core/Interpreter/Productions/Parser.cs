using Core.Interpreter.Tokens;

namespace Core.Interpreter
{
	class Parser
	{
		readonly Tokenizer tokenizer;

		public Parser(Tokenizer tokenizer)
		{
			this.tokenizer = tokenizer;
		}
	}
}
