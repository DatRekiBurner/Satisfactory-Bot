using System.Diagnostics;
using SatisfactoryBot.Modules;
using SatisfactoryBot.Models;
using SatisfactoryBot.Core.Extensions;
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

        /// <summary>
        /// Stop the Satisfactory server.
        /// </summary>
        internal static Task StopServer(Process process)
        {
            process.Kill(true);
            Server.ServerInfo = new()
            {
                Id = 0,
                Status = ServerInfoModel.ServerStatus.Offline,
                Stopped = true
            };

            return Task.CompletedTask;
        }

        /// <summary>
        /// Start the Satisfactory server in a new CMD window.
        /// </summary>
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
                    Arguments = $"/C cd {Credentials.CurrentDir} && .\\FactoryServer.exe -multihome={Credentials.Creds.ServerIp} -log -unattended"
#endif
                },
                EnableRaisingEvents = true
            };
            cmd.Exited += new EventHandler(ProcessExited);
            cmd.Start();

            Server.ServerInfo = new()
            {
                Id = cmd.Id,
                Status = ServerInfoModel.ServerStatus.Online,
            };

            cmd.WaitForExit();
        }

        /// <summary>
        /// Automatically restart the Satisfactory server after 5 seconds if it crashed.
        /// </summary>
        private static async void ProcessExited(object? sender, EventArgs e)
        {
            await Task.Delay(5000);
            ServerInfoModel serverInfo = Server.ServerInfo;

            if (!serverInfo.Stopped)
            {
                Process? process = serverInfo.Id.GetProcess();
                if (process == null)
                    await StartServer();
            }
        }
    }
}
