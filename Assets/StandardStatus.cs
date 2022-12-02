namespace PearlSoft.Scripts.Runtime.Common
{
    public class StandardStatus : PearlBehaviour
    {

        #region Public Constants

        public const string INITIALIZATION_WAITING = "Initialization Stopped: Waiting";
        public const string INITIALIZATION_RUNNING = "Initialization Running";
        public const string INITIALIZATION_STOPPED_ERROR = "Initialization Stopped: Error";
        public const string INITIALIZATION_FINISHED = "Initialization Finished";

        public const string STOPPED_HEALTHY = "Stopped (Healthy)";
        public const string STOPPED_ERROR = "Stopped (Error)";

        #endregion

        #region Public Static Methods

        public static string InitializationWaiting(string pendingTaskDescription)
        {
            return $"{INITIALIZATION_WAITING} ({pendingTaskDescription})";
        }

        public static string StoppedError(string errorReason)
        {
            return $"Stopped (Error: {errorReason})";
        }

        #endregion

    }
}