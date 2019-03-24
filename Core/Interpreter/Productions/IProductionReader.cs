using Core.Interpreter.Tokens;

namespace Core.Interpreter.Productions
{
    interface IProductionReader
    {
        bool FirstToken(TokenInfo token);
        bool IsProduction(PeekBuffer<TokenInfo> tokenStream);
        void ReadProduction(PeekBuffer<TokenInfo> tokenStream);
    }
}
