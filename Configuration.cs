namespace Blog
{
    public static class Configuration
    {
        // TOKEN - JWT - Json Web Token
        public static string JwtKey = "U0RMR1ZLSUtITkFTREJJS0xITjIzNDUyNVNERlJPSEpBV1NFUkhPUMOHR0laREZLTE5ETENWMzQ1NjdLV1NKTkZMQVdFSUtHTkFTT0zDh1ZJS1pITlNEWExWSUtBU0RFUk4yMzQ1MjM0MDU=";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "cursp_api_AIRUB4/ALN/ALKAVNR==";
        public static SmtpConfiguration Smtp = new();


        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}