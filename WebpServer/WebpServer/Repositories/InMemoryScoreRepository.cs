using System.Collections.Generic;
using System.Linq;
using WebpServer.Entity;

namespace WebpServer.Repositories
{
    public class InMemoryScoreRepository : IScoreRepository
    {
        private readonly List<Score> _scores = new();
        private int _nextId = 1;

        public Score Add(Score score)
        {
            score.Id = _nextId++;
            _scores.Add(score);
            return score;
        }

        public IEnumerable<Score> GetTop(int limit)
        {
            return _scores
                .OrderByDescending(s => s.ScoreValue)
                .ThenBy(s => s.CreatedAt)
                .Take(limit);
        }

        public Score? GetBestByPlayerId(string playerId)
        {
            return _scores
                .Where(s => s.PlayerId == playerId)
                .OrderByDescending(s => s.ScoreValue)
                .ThenBy(s => s.CreatedAt)
                .FirstOrDefault();
        }
    }

}
