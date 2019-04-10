namespace STAL.Core.Interpreter.Tokens
{
	public class TokenInfo
	{
		public readonly dynamic Data;
		public readonly TokenType Type;

		public TokenInfo(TokenType type, dynamic data = null)
		{
			Data = data;
			Type = type;
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
