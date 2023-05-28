using Microsoft.AspNetCore.SignalR.Client;

namespace Sebs.Toolkit.Signalr
{
    public interface ISignalrService
    {
        Task<HubConnection> Start(
            string hubUrl = "",
            int attemptsToReconnect = 0,
            int delaySeconds = 1,
            bool shouldAutoReconnect = true);
    }
}