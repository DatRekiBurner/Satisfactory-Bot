using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using SatisfactoryBot.Core;

namespace SatisfactoryBot
{
    internal class Bot
    {
        public static DiscordShardedClient? Discord { get; private set; }

        public static readonly List<ApplicationCommandModule> SlashCommandModules = Reflection.InitializeClasses<ApplicationCommandModule>();

        public static async Task Run()
        {
            Discord = new(new DiscordConfiguration()
            {
                Token = Credentials.Creds.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
                AutoReconnect = true
            });

            #region CommandsNext
            IReadOnlyDictionary<int, CommandsNextExtension> commands = await Discord.UseCommandsNextAsync(new CommandsNextConfiguration()
            {
                EnableDefaultHelp = true,
                EnableMentionPrefix = true,
            });

            #endregion
            #region SlashCommands
            Task<IReadOnlyDictionary<int, SlashCommandsExtension>> slashCommands = Discord.UseSlashCommandsAsync(new SlashCommandsConfiguration()
            {

            });

            IReadOnlyDictionary<int, SlashCommandsExtension> slashCommand = await slashCommands;


            foreach (ApplicationCommandModule module in SlashCommandModules)
            {
#if (DEBUG)
                try
                {
                    slashCommand.RegisterCommands(module.GetType(), Credentials.Creds.GuildId);
                }
                catch
                {
                    slashCommand.RegisterCommands(module.GetType());
                }
#else
                slashCommand.RegisterCommands(module.GetType());
#endif
            }
            #endregion

            Discord.Ready += Events.Ready;

            await ProcessFunctions.StartServer();

            await Discord.StartAsync();
            await Task.Delay(-1);

        }
    }
}
