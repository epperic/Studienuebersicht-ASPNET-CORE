namespace Studienuebersicht.Model
{
    public class Account : Entity
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
