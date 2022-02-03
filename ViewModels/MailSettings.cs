namespace BlogMVC.ViewModels
{
    public class MailSettings
    {
        // To configure and use an SMTP server i.e. from Google
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
