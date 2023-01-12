using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace SatisfactoryBot.Models
{
    public class Embed
    {
        public class Success
        {
            public DiscordEmbedBuilder Embed { get; set; }

            public Success(InteractionContext ctx, string? title = null, string? description = null)
            {
                Embed = new Standard(ctx, DiscordColor.Green, title, description).Embed;
            }
        }

        public class Error
        {
            public DiscordEmbedBuilder Embed { get; set; }

            public Error(InteractionContext ctx, string? title = null, string? description = null)
            {
                Embed = new Standard(ctx, DiscordColor.Red, title, description).Embed;
            }
        }

        public class Standard
        {
            public DiscordEmbedBuilder Embed { get; set; }

            public Standard(InteractionContext ctx, DiscordColor? color = null, string? title = null, string? description = null)
            {
                Embed = new()
                {
                    Footer = new()
                    {
                        Text = $"Executed by {ctx.User.Username}#{ctx.User.Discriminator}",
                        IconUrl = ctx.User.AvatarUrl
                    },
                };

                if (color == null)
                    Embed.Color = DiscordColor.DarkButNotBlack;
                else
                    Embed.Color = (DiscordColor)color;

                if (!string.IsNullOrWhiteSpace(title))
                    Embed.Title = title;

                if (!string.IsNullOrWhiteSpace(description))
                    Embed.Description = description;
            }
        }
    }
}
