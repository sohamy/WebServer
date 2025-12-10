// Service/ITimeService.cs
using System;
using System.Threading.Tasks;

namespace WebpServer.Services
{
    public interface ITimeService
    {
        Task<DateTimeOffset> GetSeoulTimeAsync();
    }
}
