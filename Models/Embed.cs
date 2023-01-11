using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace SatisfactoryBot.Models
{
    public class Embed
    {
        public class Success
        {
            public DiscordEmbedBuilder Embed { get; set; }

            public Success(InteractionContext ctx, string title)
            {
                Embed = new Standard(ctx, title).Embed;
                Embed.Color = DiscordColor.Green;
            }

            public Success(InteractionContext ctx, string title, string description)
            {
                Embed = new Standard(ctx, title, description).Embed;
                Embed.Color = DiscordColor.Green;
            }
        }

        public class Error
        {
            public DiscordEmbedBuilder Embed { get; set; }

            public Error(InteractionContext ctx, string title)
            {
                Embed = new Standard(ctx, title).Embed;
                Embed.Color = DiscordColor.Red;
            }

            public Error(InteractionContext ctx, string title, string description)
            {
                Embed = new Standard(ctx, title, description).Embed;
                Embed.Color = DiscordColor.Red;
            }
        }

        public class Standard
        {
            public DiscordEmbedBuilder Embed { get; set; }

            public Standard(InteractionContext ctx, string title)
            {
                Embed = new()
                {
                    Title = title,
                    Footer = new()
                    {
                        Text = $"Executed by {ctx.User.Username}#{ctx.User.Discriminator}",
                        IconUrl = ctx.User.AvatarUrl
                    },
                    Color = DiscordColor.DarkButNotBlack
                };
            }

            public Standard(InteractionContext ctx, string title, string description)
            {
                Embed = new()
                {
                    Title = title,
                    Footer = new()
                    {
                        Text = $"Executed by {ctx.User.Username}#{ctx.User.Discriminator}",
                        IconUrl = ctx.User.AvatarUrl
                    },
                    Color = DiscordColor.DarkButNotBlack,
                    Description = description
                };
            }
        }
    }
}
