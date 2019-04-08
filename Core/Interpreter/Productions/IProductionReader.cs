using Core.Interpreter.Tokens;

namespace Core.Interpreter.Productions
{
    public interface IProductionReader
    {
        bool FirstToken(TokenInfo token);
        bool IsProduction(PeekBuffer<TokenInfo> tokenStream, int index = 0);
        ProductionInfo ReadProduction(PeekBuffer<TokenInfo> tokenStream);
    }
}
