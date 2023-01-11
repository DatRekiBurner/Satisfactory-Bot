using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace SatisfactoryBot.Core.Services.Discord
{
    internal class Response
    {
        /// <summary>
        /// Send a embed
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public static async Task SendEmbed(InteractionContext ctx, DiscordEmbedBuilder embed)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
        }

        /// <summary>
        /// Edit/update reponse with a embed
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="embed"></param>
        /// <returns></returns>
        public static async Task EditResponse(InteractionContext ctx, DiscordEmbedBuilder embed)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
        }
    }
}
