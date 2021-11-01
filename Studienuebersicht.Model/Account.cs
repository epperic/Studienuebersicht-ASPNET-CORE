namespace Studienuebersicht.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
