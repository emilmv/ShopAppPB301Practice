namespace ShopAppPB301Practice.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(List<string> emails, string subject, string body);
    }
}