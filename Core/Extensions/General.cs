using System.Diagnostics;

namespace SatisfactoryBot.Core.Extensions
{
    internal static class General
    {
        internal static Process? GetProcess(this int id) => Process.GetProcesses().Where(x => x.Id == id).FirstOrDefault();
    }
}
