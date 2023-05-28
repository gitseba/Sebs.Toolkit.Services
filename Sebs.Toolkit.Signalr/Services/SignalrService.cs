using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace Sebs.Toolkit.Signalr
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 12/19/2022 11:53:10 AM
    /// </summary>
    public class SignalrService : ISignalrService
    {
        private HubConnection _hubConnection;
        private readonly string _hubUrl;
        private readonly int _counts;
        private readonly int _delaySeconds;
        private readonly ILogger? _logger;
        CancellationTokenSource cancellationToken;

        public SignalrService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<HubConnection> Start(
            string hubUrl = "",
            int attemptsToReconnect = 0,
            int delaySeconds = 1,
            bool shouldAutoReconnect = true)
        {
            CreateConnection();
            var openConnectionResult = await OpenConnection();
            if (openConnectionResult != null)
            {
                if (shouldAutoReconnect)
                {
                    _logger?.LogError("Automatically retry to reconnect!");
                    _ = await AttemptToReconnect(_counts);
                }
                else
                {
                    throw new Exception("Initialization failed. A connection with the Hub could not be opened. Please restart application to retry.");
                }
            }

            _hubConnection.Closed += ClosedHandler;
            _hubConnection.Reconnecting += ReconnectingHandler;
            _hubConnection.Reconnected += ReconnectedHandler;

            return _hubConnection;
        }

        private Task? ReconnectedHandler(string arg)
        {
            _logger?.LogInformation($"Reconnected. {arg}");
            cancellationToken.Cancel(false);
            return null;
        }

        private async Task ReconnectingHandler(Exception arg)
        {
            _logger?.LogInformation($"Reconnecting. {arg.Message}");
            await AttemptToReconnect(attempts: _counts);
        }

        private Task? ClosedHandler(Exception arg)
        {
            _logger?.LogError($"Connection with the Hub was been closed. {arg.Message}");
            return null;
        }

        private void CreateConnection()
        {
            _logger?.LogInformation("Create a connection with SignalR Hub.");

            _hubConnection = new HubConnectionBuilder()
               .WithUrl(_hubUrl, options =>
               {
                   options.AccessTokenProvider = () => Task.FromResult("Sebs");
               })
               .WithAutomaticReconnect()
               .Build();
        }

        private async Task<string> OpenConnection()
        {
            return await _hubConnection
                         .StartAsync()
                         .ContinueWith(task =>
                         {
                             if (task.Exception != null)
                             {
                                 _logger?.LogError(task.Exception.Message);
                                 return task.Exception.Message;
                             }

                             _logger.LogInformation("Connection is opened.");
                             return String.Empty;
                         });
        }

        private async Task<string> AttemptToReconnect(int attempts = 0)
        {
            var count = 1;
            string exception;
            cancellationToken = new CancellationTokenSource();
            do
            {
                _logger?.LogWarning($"Attempt {count} to reconnect ...");
                exception = await OpenConnection();
                if (cancellationToken.IsCancellationRequested || count == attempts)
                {
                    _logger?.LogError($"Reconnection succeed.");
                    break;
                }

                await Task.Delay(_delaySeconds * 1000);
                if (exception == null)
                {
                    _logger?.LogError($"Attempt {count}: Succeeded.");
                }
                else
                {
                    _logger?.LogError($"Attempt {count}: {exception}");
                    count++;
                }
            } while (exception != null);

            return exception;
        }
    }
}
