using System;
using System.Collections.Generic;
using System.Linq;
using WebpServer.Protocol;
using WebpServer.Repositories;
using WebpServer.Entity;

namespace WebpServer.Service
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _repo;

        public ScoreService(IScoreRepository repo)
        {
            _repo = repo;
        }

        public ScoreProtocol Submit(SubmitScoreRequest request)
        {
            var score = new Score
            {
                PlayerId = request.PlayerId,
                PlayerName = request.PlayerName,
                ScoreValue = request.ScoreValue,
                CreatedAt = DateTime.UtcNow
            };

            score = _repo.Add(score);
            return ToDto(score);
        }

        public IEnumerable<ScoreProtocol> GetTop(int limit)
        {
            return _repo.GetTop(limit).Select(ToDto);
        }

        public ScoreProtocol GetBest(string playerId)
        {
            var best = _repo.GetBestByPlayerId(playerId);
            if (best == null)
                throw new KeyNotFoundException($"No score found for player {playerId}");

            return ToDto(best);
        }

        private static ScoreProtocol ToDto(Score s)
        {
            return new ScoreProtocol
            {
                PlayerId = s.PlayerId,
                PlayerName = s.PlayerName,
                ScoreValue = s.ScoreValue,
                CreatedAt = s.CreatedAt
            };
        }
    }

}
