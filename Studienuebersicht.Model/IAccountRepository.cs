namespace Studienuebersicht.Model
{
    public interface IAccountRepository
    {
        Account FindAdminByEmailAndPassword(string email, string password);
        Account ById(int id);
        void Save(Account account);
    }
}
