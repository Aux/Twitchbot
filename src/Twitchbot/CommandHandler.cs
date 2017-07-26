using NTwitch.Chat;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Twitchbot
{
    public class CommandHandler
    {
        private readonly TwitchChatClient _client;
        private readonly Configuration _config;
        private readonly Random _random;

        public CommandHandler(TwitchChatClient client, Configuration config)
        {
            _client = client;
            _config = config;
            _random = new Random();

            if (_config.Commands.Count > 0)
                _client.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(ChatMessage msg)
        {
            foreach (var cmd in _config.Commands)
            {
                if (cmd.Replies.Count == 0)
                    continue;

                if (IsCommand(msg.Content, cmd))
                {
                    var reply = GetReply(cmd, msg);
                    await msg.Channel.SendMessageAsync(reply);
                    return;
                }
            }
        }

        private bool IsCommand(string message, Command cmd)
        {
            bool startsWith = true, 
                contains = true, 
                endsWith = true;

            if (!string.IsNullOrWhiteSpace(cmd.StartsWith))
                startsWith = message.StartsWith(cmd.StartsWith);
            if (!string.IsNullOrWhiteSpace(cmd.Contains))
                contains = message.Contains(cmd.Contains);
            if (!string.IsNullOrWhiteSpace(cmd.EndsWith))
                endsWith = message.EndsWith(cmd.EndsWith);
            
            return startsWith && contains && endsWith;
        }

        private string GetReply(Command cmd, ChatMessage msg)
        {
            int selectedIndex = _random.Next(0, cmd.Replies.Count);
            string reply = cmd.Replies.ElementAt(selectedIndex);
            return ParseVariables(reply, msg);
        }

        private string ParseVariables(string message, ChatMessage msg)
        {
            message = message.Replace("%user%", msg.User.DisplayName);
            message = message.Replace("%channel%", msg.Channel.Name);
            return message;
        }
    }
}
