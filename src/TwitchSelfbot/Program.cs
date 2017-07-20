using System;
using System.Threading.Tasks;

namespace TwitchSelfbot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            await Task.Delay(0);
        }
    }
}