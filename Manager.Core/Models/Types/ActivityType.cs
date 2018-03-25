using System.Collections.Generic;

namespace Manager.Core.Models.Types
{
    public class ActivityType
    {
        public const string Work = "work";
        public const string Programming = "programming";
        public const string Private = "private";
        public const string Other = "other";

        public static List<string> Types = new List<string>
        {
            Work,
            Programming,
            Private,
            Other
        };

        public static bool IsValid(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return false;
            }
            type = type.ToLowerInvariant();

            return type == Work || type == Programming || type == Private || type == Other;
        }
    }
}