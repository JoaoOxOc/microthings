using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Interfaces
{
    public interface ISlackNotificator
    {
        Task<string> SendSlackMessageToChannelAsync(string text);
    }
}
