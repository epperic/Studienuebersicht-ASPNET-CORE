namespace Studienuebersicht.Model
{
    public interface IRepository
    {
        public IAccountRepository Accounts { get; }
        public IModuleRepository Modules { get; }
    }
}
