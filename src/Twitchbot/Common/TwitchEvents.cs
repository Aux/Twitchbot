using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace Twitchbot
{
    public class TwitchEvents
    {
        [YamlIgnore]
        private readonly Random _random = new Random();

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

        public string GetNewSubReply()
            => GetRandomReply(NewSubReplies);
        public string GetReSubReply()
            => GetRandomReply(ReSubReplies);
        public string GetHostingStartedReply()
            => GetRandomReply(HostingStartedReplies);
        public string GetHostingStoppedReply()
            => GetRandomReply(HostingStoppedReplies);
        public string GetUserBannedReply()
            => GetRandomReply(UserBannedReplies);

        private string GetRandomReply(IEnumerable<string> replies)
        {
            if (replies == null) return null;
            if (replies.Count() == 1) return replies.First();

            int selectedIndex = _random.Next(0, replies.Count());
            return replies.ElementAt(selectedIndex);
        }
    }
}
