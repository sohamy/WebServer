using System;

namespace WebpServer.Exceptions
{
    // 클라이언트가 잘못된 값 보냈을 때
    public class GameValidationException : Exception
    {
        public GameValidationException(string message)
            : base(message)
        {
        }
    }
}
