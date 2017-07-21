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
        [Required, YamlMember(Alias = "events")]
        public TwitchEvents Events { get; set; }

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
                           "token: oi1j2o3i\r\n\r\n" +
                           "# Channels the bot will join after connecting to chat\r\n" +
                           "channels:\r\n" +
                           "  - auxesistv\r\n" +
                           "  - shiralya\r\n\r\n" +
                           "# All events the bot should reply to. Replies are\r\n" +
                           "# randomly selected from list provided for each event\r\n#\r\n" +
                           "# Available events: new_sub, re_sub, hosting_started,\r\n" +
                           "# hosting_stopped, user_banned\r\n#\r\n" +
                           "# Available variables: %user%, %channel%\r\n" +
                           "events:\r\n" +
                           "  new_sub:\r\n" +
                           "    - Thanks for subscribing %user% Kappa\r\n" +
                           "    - Thank you for the sub %user% LUL\r\n" +
                           "  re_sub:\r\n" +
                           "    - Wow! %user% resubscribed for %months% month(s)\r\n";
            File.Create(configPath).Dispose();
            File.WriteAllText(configPath, contents);
        }
    }
}
