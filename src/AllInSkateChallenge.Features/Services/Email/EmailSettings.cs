namespace AllInSkateChallenge.Features.Services.Email
{
    public class EmailSettings
    {
        public string CommResourceConnectionString { get; set; }

        public string SenderEmail { get; set; }

        public string SmtpServer { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
