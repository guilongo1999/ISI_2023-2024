namespace RESTapi_simples
{
    public class UserInfo
    {

        public string Username { get; set; } = string.Empty;

        public byte[] passwordHash { get; set; } //8 bit unsigned integer [0-255]

        public byte[] passwordSalt { get; set; } // um pouco mais de informacao antes do hash para resolver o problema com passwords identicas, mais faceis de quebrar a encriptacao




    }
}
