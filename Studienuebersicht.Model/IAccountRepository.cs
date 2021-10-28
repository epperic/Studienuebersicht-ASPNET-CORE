namespace Studienuebersicht.Model
{
    public interface IAccountRepository
    {
        Account FindAdminByEmailAndPassword(string email, string password);
        void Save(Account account);
    }
}
