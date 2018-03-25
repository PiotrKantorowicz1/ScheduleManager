using System.Collections.Generic;

namespace Manager.Core.Models.Types
{
    public class Status
    {
        public const string ToComplete = "tocomplete";
        public const string InProgress = "inprogress";
        public const string Completed = "completed";
        public const string Canceled = "canceled";

        public static List<string> Statuses = new List<string>
        {
            ToComplete,
            InProgress,
            Completed,
            Canceled
        };

        public static bool IsValid(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return false;
            }
            status = status.ToLowerInvariant();

            return status == ToComplete || status == InProgress || status == Completed || status == Canceled;
        }
    }
}