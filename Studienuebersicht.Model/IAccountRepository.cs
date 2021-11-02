namespace Studienuebersicht.Model
{
    public interface IAccountRepository
    {
        Account FindAccount(string email);
        Account ById(int id);
        void Save(Account account);
    }
}
