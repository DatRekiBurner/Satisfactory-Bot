using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace SatisfactoryBot
{
    internal class Events
    {
        public static async Task Ready(DiscordClient sender, ReadyEventArgs e)
        {
            await sender.UpdateStatusAsync(new DiscordActivity()
            {
                Name = $"Managing the Satisfactory server",
                ActivityType = ActivityType.Playing
            }).ConfigureAwait(false);
        }
    }
}
