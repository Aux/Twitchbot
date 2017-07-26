using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using YamlDotNet.Serialization;

namespace Twitchbot
{
    public class Configuration
    {
        private static readonly string configPath = Path.Combine(AppContext.BaseDirectory, "config.yml");

        [Required, YamlMember(Alias = "token")]
        public string Token { get; set; }
        [Required, YamlMember(Alias = "channels")]
        public List<string> Channels { get; set; }
        [YamlMember(Alias = "events")]
        public TwitchEvents Events { get; set; }
        [YamlMember(Alias = "commands")]
        public List<Command> Commands { get; set; }

        public static bool Exists()
            => File.Exists(configPath);

        public static Configuration Load()
        {
            var contents = File.ReadAllText(configPath);
            var deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<Configuration>(contents);
        }

        public static void Create()
        {
            var contents = "# A Twitch oauth token the bot will use to log in to chat\r\n" +
                            "token: yourtokenhere\r\n\r\n" +
                            "# Channels the bot will join after connecting to chat\r\n" +
                            "channels:\r\n" +
                            "  - auxesistv\r\n" +
                            "  - shiralya\r\n\r\n" +
                            "# All events the bot should reply to. Replies are\r\n" +
                            "# randomly selected from list provided for each event\r\n#\r\n" +
                            "# Available events: new_sub, re_sub, hosting_started,\r\n" +
                            "# hosting_stopped, user_banned\r\n" +
                            "events:\r\n" +
                            "  new_sub:\r\n" +
                            "    - Thanks for subscribing %user% Kappa\r\n" +
                            "    - Thank you for the sub %user% LUL\r\n" +
                            "  re_sub:\r\n" +
                            "    - Wow! %user% resubscribed for %months% month(s)\r\n\r\n" +
                            "# Commands the bot will reply to in chat. Supports basic\r\n" +
                            "# variables like %user% and %channel%.\r\n#\r\n" +
                            "# Available conditions: starts_with, contains, ends_with\r\n" +
                            "commands:\r\n" +
                            "  - starts_with: '!repo'\r\n" +
                            "    replies:\r\n" +
                            "      - https://github.com/Aux/Twitchbot\r\n" +
                            "  - starts_with: '!quote'\r\n" +
                            "    replies:\r\n" +
                            "      - '\"I don’t want to earn my living; I want to live.\" - Oscar Wilde'\r\n" +
                            "      - '\"Life shrinks or expands in proportion to one’s courage.\" - Anais Nin'\r\n" +
                            "      - '\"Whatever you are, be a good one.\" - Abraham Lincoln'\r\n" +
                            "  - contains: thanks twitchbot\r\n" +
                            "    replies:\r\n" +
                            "      - You're welcome %user%!\r\n" +
                            "  - ends_with: ???\r\n" +
                            "    replies:\r\n" +
                            "      - Calm down with the question marks pls\r\n";
            File.Create(configPath).Dispose();
            File.WriteAllText(configPath, contents);
        }
    }
}
