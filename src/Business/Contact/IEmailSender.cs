namespace DetroitHarps.Business.Contact
{
    public interface IEmailSender
    {
        void SendToSelf(string subject, string body);
    }
}