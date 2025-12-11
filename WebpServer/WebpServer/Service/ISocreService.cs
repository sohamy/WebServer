using WebpServer.Protocol;

namespace WebpServer.Service
{
    public interface IScoreService
    {
        ScoreProtocol Submit(SubmitScoreRequest request);
        IEnumerable<ScoreProtocol> GetTop(int limit);
        ScoreProtocol GetBest(string playerId);
    }
}
