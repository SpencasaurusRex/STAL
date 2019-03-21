namespace Core.Interpreter.Tokens
{
	public class TokenInfo
	{
		public readonly dynamic data;
		public readonly TokenType type;

		public TokenInfo(TokenType type, dynamic data = null)
		{
			this.data = data;
			this.type = type;
		}
	}

	public enum TokenType
	{
		Name,		//data : string
		Number,		//data : double
		String,		//data : string
		Colon,		//data : null
		Comma,		//data : null
		LeftParen,	//data : null
		RightParen,	//data : null
		Undefined
	}
}
