namespace Manager.Api.Framework
{
    public class AdminAuth : AuthAttribute
    {
        public AdminAuth() : base("admin")
        {
        }
    }
}