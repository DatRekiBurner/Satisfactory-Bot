using DSharpPlus.SlashCommands;

namespace SatisfactoryBot.Core.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SpecificGuildOnlyAttribute : SlashCheckBaseAttribute
    {
        public override Task<bool> ExecuteChecksAsync(InteractionContext ctx)
        {
            if (ctx.Guild.Id == Credentials.Creds.GuildId)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
    }
}
