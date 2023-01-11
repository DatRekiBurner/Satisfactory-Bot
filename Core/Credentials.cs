using Newtonsoft.Json;
using SatisfactoryBot.Models;

namespace SatisfactoryBot.Core
{
    public class Credentials
    {
        public static string CurrentDir { get; private set; } = Directory.GetCurrentDirectory();
        public static string ConfigFile { get; private set; } = Path.Combine(CurrentDir, "config.json");
        public static CredentialsModel Creds { get; private set; } = GetCreds();

        internal static CredentialsModel GetCreds()
        {
            CredentialsModel result = new();

            string error = string.Empty;
            if (File.Exists(ConfigFile))
            {
                CredentialsModel? creds = JsonConvert.DeserializeObject<CredentialsModel>(File.ReadAllText(ConfigFile));
                if (creds == null)
                    error = $"The credentials file is empty";
                else
                {
                    if (string.IsNullOrWhiteSpace(creds.Token))
                        error = "You haven't specified the token for the Discord bot application";
                    else if (creds.GuildId == 0)
                        error = "You haven't specified the guild where the bot can be used";
                    else if (string.IsNullOrWhiteSpace(creds.ServerIp))
                        error = "You haven't specified the ip that the Satisfactory server uses";
                    else
                        result = creds;
                }
            }
            else
            {
                File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(new CredentialsModel(), Formatting.Indented));
                error = "You haven't filled in your credentials file yet";
            }

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine($"{error}.\nLocation: {ConfigFile}");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            return result;
        }
    }
}
