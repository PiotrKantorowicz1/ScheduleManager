namespace Manager.Struct.Services
{
    public interface ICrypton
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}