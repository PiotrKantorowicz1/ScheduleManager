using System.Collections.Generic;

namespace Manager.Core.Models.Types
{
    public class Priority
    {
        public const string High = "high";
        public const string Medium = "medium";
        public const string Low = "low";

        public static List<string> Priorities = new List<string>
        {
            High,
            Medium,
            Low
        };
        
        public static bool IsValid(string priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
            {
                return false;
            }
            priority = priority.ToLowerInvariant();

            return priority == High || priority == Medium || priority == Low;
        }
    }
}