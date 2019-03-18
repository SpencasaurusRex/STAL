namespace STAL.Interpreter
{
	internal class Token
	{
		readonly dynamic data;
		readonly TokenType type;

		public Token(TokenType type, dynamic data = null)
		{
			this.data = data;
			this.type = type;
		}
	}

	internal enum TokenType
	{
		Name,		//data : string
		Number,		//data : double
		Quote,		//data : null
		Colon,		//data : null
		Comma,		//data : null
		LeftParen,	//data : null
		RightParen,	//data : null
		Undefined
	}
}
