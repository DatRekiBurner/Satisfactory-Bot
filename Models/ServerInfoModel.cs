namespace SatisfactoryBot.Models
{
    internal class ServerInfoModel
    {
        internal enum ServerStatus
        {
            Online,
            Offline
        }

        /// <summary>
        /// The ID of the process/CMD that was used run the Satisfactory server start command.
        /// </summary>
        internal int Id { get; set; }

        /// <summary>
        /// Status of the Satisfactory server.
        /// </summary>
        internal ServerStatus Status { get; set; } = ServerStatus.Offline;

        /// <summary>
        /// At what time the Satisfactory server was started.
        /// </summary>
        internal DateTime StartTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicate if the Satisfactory server was intentionally stopped or if it crashed.
        /// </summary>
        internal bool Stopped { get; set; } = false;
    }
}
