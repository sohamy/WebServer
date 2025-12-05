using System;

namespace WebpServer.Exceptions
{
    // 인증/권한 문제
    public class GameUnauthorizedException : Exception
    {
        public GameUnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
