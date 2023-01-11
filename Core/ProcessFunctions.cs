using System.Diagnostics;
#if DEBUG
using System.Net.Sockets;
using System.Net;
#endif

namespace SatisfactoryBot.Core
{
    internal class ProcessFunctions
    {
        internal static async Task StartServer()
        {
            new Thread(StartProcess).Start();
            await Task.Delay(1000);
        }

        internal static Task StopServer(Process process)
        {
            process.Kill(true);
            Modules.Server.Id = 0;
            return Task.CompletedTask;
        }

        private static void StartProcess(object? obj)
        {
            Process cmd = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = @"c:\windows\system32\",
                    FileName = "cmd.exe",
                    UseShellExecute = true,
#if DEBUG
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = $"/C ping -t {Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).First()}"
#else
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = $"/C .\\FactoryServer.exe -multihome={Credentials.Creds.ServerIp} -log -unattended"
#endif
                }
            };
            cmd.Start();
            Modules.Server.Id = cmd.Id;
            cmd.WaitForExit();
        }
    }
}
