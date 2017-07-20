using NTwitch.Chat;
using System;
using System.Threading.Tasks;

namespace TwitchSelfbot
{
    public class TwitchEventHandler
    {
        private readonly TwitchChatClient _client;
        private readonly Configuration _config;
        private readonly Random _random;

        public TwitchEventHandler(TwitchChatClient client, Configuration config)
        {
            _client = client;
            _config = config;
            _random = new Random();

            if (_config.Events.NewSubReplies != null || _config.Events.ReSubReplies != null)
                _client.MessageReceived += OnMessageReceivedAsync;

            if (_config.Events.HostingStartedReplies != null)
                _client.HostingStarted += OnHostingStartedAsync;
            if (_config.Events.HostingStoppedReplies != null)
                _client.HostingStopped += OnHostingStoppedAsync;
            
            if (_config.Events.UserBannedReplies != null)
                _client.UserBanned += OnUserBannedAsync;
        }

        private Task OnMessageReceivedAsync(ChatMessage msg)
        {
            throw new NotImplementedException();
        }

        private Task OnHostingStartedAsync(Cacheable<string, ChatSimpleChannel> host, Cacheable<string, ChatSimpleChannel> channel, int viewers)
        {
            throw new NotImplementedException();
        }

        private Task OnHostingStoppedAsync(Cacheable<string, ChatSimpleChannel> host, int viewers)
        {
            throw new NotImplementedException();
        }

        private Task OnUserBannedAsync(ChatSimpleChannel channel, ChatSimpleUser user, BanOptions ban)
        {
            throw new NotImplementedException();
        }
    }
}
