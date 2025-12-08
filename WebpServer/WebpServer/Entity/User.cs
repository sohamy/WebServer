using System;

namespace WebpServer.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
