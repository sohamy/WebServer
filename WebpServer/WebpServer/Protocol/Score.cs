namespace WebpServer.Protocol
{
    public class ScoreProtocol
    {
        public string PlayerId { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public int ScoreValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class SubmitScoreRequest
    {
        public string PlayerId { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public int ScoreValue { get; set; }
    }
}
