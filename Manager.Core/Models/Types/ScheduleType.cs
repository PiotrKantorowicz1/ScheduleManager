using System.Collections.Generic;

namespace Manager.Core.Models.Types
{
    public class ScheduleType
    {
        public const string Work = "work";
        public const string Coffee = "coffee";
        public const string Doctor = "doctor";
        public const string Shopping = "shopping";
        public const string Other = "other";


        public static List<string> Types = new List<string>
        {
            Work,
            Coffee,
            Doctor,
            Shopping,
            Other
        };

        public static bool IsValid(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return false;
            }
            type = type.ToLowerInvariant();

            return type == Work || type == Coffee || type == Doctor || type == Shopping || type == Other;
        }
    }
}
