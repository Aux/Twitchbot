using NTwitch;
using NTwitch.Chat;
using System;
using System.Threading.Tasks;

namespace Twitchbot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private TwitchEventHandler _events;
        private TwitchChatClient _client;
        private CommandHandler _commands;
        private Configuration _config;

        public async Task StartAsync()
        {
            if (!Configuration.Exists())
            {
                Configuration.Create();
                Console.WriteLine($"The config.yml file has been created in: `{AppContext.BaseDirectory}`");
                Console.WriteLine("Please configure your settings and then restart the bot...");
                Console.ReadKey();
                return;
            }

            _config = Configuration.Load();
            _client = new TwitchChatClient(new TwitchChatConfig
            {
                LogLevel = LogSeverity.Info
            });
            _events = new TwitchEventHandler(_client, _config);
            _commands = new CommandHandler(_client, _config);
            
            _client.Log += OnLogAsync;
            _client.Connected += OnConnectedAsync;

            await _client.LoginAsync(_config.Token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task OnLogAsync(LogMessage msg)
            => Console.Out.WriteLineAsync($"[{msg.Level}] {msg.Source}: {msg.Message}");

        private async Task OnConnectedAsync()
        {
            foreach (var channel in _config.Channels)
                await _client.JoinChannelAsync(channel);
        }
    }
}