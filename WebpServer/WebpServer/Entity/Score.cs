namespace WebpServer.Entity
{
    public class Score
    {
        public int Id { get; set; }
        public string PlayerId { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public int ScoreValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
