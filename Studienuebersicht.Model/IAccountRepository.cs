namespace Studienuebersicht.Model
{
    public interface IAccountRepository
    {
        Account FindAccount(string email, string password);
        Account ById(int id);
        void Save(Account account);
    }
}
