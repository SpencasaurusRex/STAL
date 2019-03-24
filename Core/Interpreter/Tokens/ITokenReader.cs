namespace Core.Interpreter.Tokens
{
	public interface ITokenReader
	{
		bool IsStartingChar(char c);
		// TODO: Pass in a true PeekBuffer (cannot read, only peek)
		bool CheckToken(PeekBuffer<char> buffer);
		// TODO: Pass in a PeekReadBuffer (can read and peek)
		TokenInfo ReadToken(PeekBuffer<char> buffer);
	}
}
