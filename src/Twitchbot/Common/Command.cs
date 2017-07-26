using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace Twitchbot
{
    public class Command
    {
        [YamlMember(Alias = "starts_with")]
        public string StartsWith { get; set; }
        [YamlMember(Alias = "contains")]
        public string Contains { get; set; }
        [YamlMember(Alias = "ends_with")]
        public string EndsWith { get; set; }
        [Required, YamlMember(Alias = "replies")]
        public HashSet<string> Replies { get; set; }
    }
}
