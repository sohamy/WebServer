using System.Collections.Generic;
using WebpServer.Entity;

namespace WebpServer.Repositories
{
    public interface IScoreRepository
    {
        Score Add(Score score);
        IEnumerable<Score> GetTop(int limit);
        Score? GetBestByPlayerId(string playerId);
    }
}
