namespace Business.Abstractions
{
    using Business.Entities;

    public interface IEmailSender
    {
        void SendToSelf(string subject, string body);
    }
}