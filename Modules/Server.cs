using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using SatisfactoryBot.Core;
using SatisfactoryBot.Core.Extensions;
using SatisfactoryBot.Core.Extensions.Attributes;
using SatisfactoryBot.Core.Services.Discord;
using SatisfactoryBot.Models;
using System.Diagnostics;

namespace SatisfactoryBot.Modules
{
    [SlashRequireGuild]
    [SpecificGuildOnly]
    internal class Server : ApplicationCommandModule
    {
        internal static ServerInfoModel ServerInfo { get; set; } = new();

        internal class Messages
        {
            internal static string Success = "The server has been successfully {0}.";
            internal static string Error = "The server could not be {0}.";

            internal static string NotFound = "I could not find any process with the ID that was assigned to the server (PowerShell) window.";
            internal static string AlreadyRunning = "The server is already running.";
            internal static string NotRunning = "There is no server running";
        }

        [SlashCommand("Start", "Start the Satisfactory server")]
        public static async Task Start(InteractionContext ctx)
        {
            string title = nameof(Start);
            string type = "started";
            DiscordEmbedBuilder embed;

            Process? process = ServerInfo.Id.GetProcess();

            if (ServerInfo.Id == 0 || process == null)
            {
                await ProcessFunctions.StartServer();

                if (ServerInfo.Id == 0)
                    embed = new Embed.Error(ctx, title, string.Format(Messages.Error, type)).Embed;
                else
                    embed = new Embed.Success(ctx, title, string.Format(Messages.Success, type)).Embed;
            }
            else
                embed = new Embed.Error(ctx, title, Messages.AlreadyRunning).Embed;

            await Response.SendEmbed(ctx, embed);
        }

        [SlashCommand("Stop", "Stop the Satisfactory server")]
        public static async Task Stop(InteractionContext ctx)
        {
            string title = nameof(Stop);
            string type = "stopped";
            DiscordEmbedBuilder embed;

            if (ServerInfo.Id == 0)
                embed = new Embed.Error(ctx, title, Messages.NotRunning).Embed;
            else
            {
                Process? process = ServerInfo.Id.GetProcess();

                if (process == null)
                    embed = new Embed.Error(ctx, title, Messages.NotFound).Embed;
                else
                {
                    await ProcessFunctions.StopServer(process);
                    embed = new Embed.Success(ctx, title, string.Format(Messages.Success, type)).Embed;
                }
            }

            await Response.SendEmbed(ctx, embed);
        }

        [SlashCommand("Restart", "Restart the Satisfactory server")]
        public static async Task Restart(InteractionContext ctx)
        {
            string title = nameof(Restart);
            string type = "restarted";
            DiscordEmbedBuilder embed;

            if (ServerInfo.Id == 0)
                embed = new Embed.Error(ctx, title, Messages.NotRunning).Embed;
            else
            {
                Process? process = ServerInfo.Id.GetProcess();

                if (process == null)
                    embed = new Embed.Error(ctx, title, Messages.NotFound).Embed;
                else
                {
                    await ProcessFunctions.StopServer(process);
                    await ProcessFunctions.StartServer();

                    if (ServerInfo.Id == 0)
                        embed = new Embed.Error(ctx, title, $"{string.Format(Messages.Success, "stopped")} But it could not be restarted.").Embed;
                    else
                        embed = new Embed.Success(ctx, title, string.Format(Messages.Success, type)).Embed;
                }
            }

            await Response.SendEmbed(ctx, embed);
        }

        [SlashCommand("Status", "The status of the Satisfactory server")]
        public static async Task Status(InteractionContext ctx)
        {
            DiscordEmbedBuilder embed;
            Process? process = ServerInfo.Id.GetProcess();

            if (ServerInfo.Id == 0 || process == null)
                embed = new Embed.Error(ctx, nameof(Status), Messages.NotRunning).Embed;
            else
            {
                TimeSpan difference = DateTime.UtcNow.Subtract(ServerInfo.StartTime);

                embed = new Embed.Success(ctx).Embed;
                embed.AddField("Status", ServerInfo.Status.ToString());
                embed.AddField("ID", process.Id.ToString());
                embed.AddField("Running for", string.Format("{0} hour(s), {1} minute(s), {2} second(s)", difference.Hours, difference.Minutes, difference.Seconds));
            }

            await Response.SendEmbed(ctx, embed);
        }
    }
}
