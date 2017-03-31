
using Backtester.backend.model.ativos;
namespace Backtester.backend.model.system.condicoes
{
    public interface ICondicao
    {
        bool VerificaCondicao(Candle candle, TradeSystem ts);
    }
}
