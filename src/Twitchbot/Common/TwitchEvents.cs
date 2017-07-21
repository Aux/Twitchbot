using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Twitchbot
{
    public class TwitchEvents
    {
        [YamlMember(Alias = "new_sub")]
        public List<string> NewSubReplies { get; set; }
        [YamlMember(Alias = "re_sub")]
        public List<string> ReSubReplies { get; set; }
        [YamlMember(Alias = "hosting_started")]
        public List<string> HostingStartedReplies { get; set; }
        [YamlMember(Alias = "hosting_stopped")]
        public List<string> HostingStoppedReplies { get; set; }
        [YamlMember(Alias = "user_banned")]
        public List<string> UserBannedReplies { get; set; }
    }
}
