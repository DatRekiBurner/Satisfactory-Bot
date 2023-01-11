using Newtonsoft.Json;

namespace SatisfactoryBot.Models
{
    public class CredentialsModel
    {
        /// <summary>
        /// The token of the Discord bot application.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the guild where you want the bot to respond to commands (it will not respond to anything outside of this guild).
        /// </summary>
        [JsonProperty("guild-id")]
        public ulong GuildId { get; set; } = 0;

        /// <summary>
        /// The IP that the Satisfactory uses.
        /// </summary>
        [JsonProperty("server-ip")]
        public string ServerIp { get; set; } = string.Empty;
    }
}
