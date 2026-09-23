namespace Data.IRepository
{

    public interface ISmsSender
    {
        Task<bool> SendUnifonicSMS(string number, string message, string appId, string sender);
        Task<bool> SendSMS(string number, string message);
    }
}
