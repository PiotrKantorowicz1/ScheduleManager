namespace Manager.Core.Models
{
    public static class Roles
    {
        public const string User = "user";
        public const string Admin = "admin";
        public const string Editor = "editor";

        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }
            role = role.ToLowerInvariant();

            return role == User || role == Admin || role == Editor;
        }
    }
}
