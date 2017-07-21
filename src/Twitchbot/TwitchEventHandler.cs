using NTwitch.Chat;
using System.Threading.Tasks;

namespace Twitchbot
{
    public class TwitchEventHandler
    {
        private readonly TwitchChatClient _client;
        private readonly Configuration _config;

        public TwitchEventHandler(TwitchChatClient client, Configuration config)
        {
            _client = client;
            _config = config;

            if (_config.Events.NewSubReplies != null || _config.Events.ReSubReplies != null)
                _client.MessageReceived += OnMessageReceivedAsync;

            if (_config.Events.HostingStartedReplies != null)
                _client.HostingStarted += OnHostingStartedAsync;
            if (_config.Events.HostingStoppedReplies != null)
                _client.HostingStopped += OnHostingStoppedAsync;
            
            if (_config.Events.UserBannedReplies != null)
                _client.UserBanned += OnUserBannedAsync;
        }

        private async Task OnMessageReceivedAsync(ChatMessage msg)
        {
            if (msg is ChatNoticeMessage notice)
            {
                if (notice.Type == "sub")
                {
                    var selected = _config.Events.GetNewSubReply();
                    if (selected == null)
                        return;

                    selected = selected.Replace("%channel%", notice.Channel.Name)
                                       .Replace("%user%", notice.User.DisplayName)
                                       .Replace("%months%", notice.Months.ToString());
                    await notice.Channel.SendMessageAsync(selected);
                }
                else if (notice.Type == "resub")
                {
                    var selected = _config.Events.GetReSubReply();
                    if (selected == null)
                        return;

                    selected = selected.Replace("%channel%", notice.Channel.Name)
                                       .Replace("%user%", notice.User.DisplayName)
                                       .Replace("%months%", notice.Months.ToString());
                    await notice.Channel.SendMessageAsync(selected);
                }
            }
        }

        private async Task OnHostingStartedAsync(Cacheable<string, ChatSimpleChannel> host, Cacheable<string, ChatSimpleChannel> channel, int viewers)
        {
            var selected = _config.Events.GetHostingStartedReply();
            if (selected == null)
                return;

            selected = selected.Replace("%host%", host.Key)
                               .Replace("%channel%", channel.Key)
                               .Replace("%viewers%", viewers.ToString());
            if (host.HasValue)
                await host.Value.SendMessageAsync(selected);
        }

        private async Task OnHostingStoppedAsync(Cacheable<string, ChatSimpleChannel> host, int viewers)
        {
            var selected = _config.Events.GetHostingStoppedReply();
            if (selected == null)
                return;

            selected = selected.Replace("%host%", host.Key)
                               .Replace("%viewers%", viewers.ToString());
            if (host.HasValue)
                await host.Value.SendMessageAsync(selected);
        }

        private async Task OnUserBannedAsync(ChatSimpleChannel channel, ChatSimpleUser user, BanOptions ban)
        {
            var selected = _config.Events.GetUserBannedReply();
            if (selected == null)
                return;

            selected = selected.Replace("%channel%", channel.Name)
                               .Replace("%user%", user.DisplayName)
                               .Replace("%duration%", ban.Duration.ToString())
                               .Replace("%reason%", ban.Reason);
            
            await channel.SendMessageAsync(selected);
        }
    }
}
