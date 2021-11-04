using Microsoft.EntityFrameworkCore;
using Studienuebersicht.Model;
using System.Linq;

namespace Studienuebersicht.EFCore
{
    public class EFCoreRepository : IRepository
    {
        private EFCoreModuleRepository module_repository;
        private EFCoreAccountRepository account_repository;
        private StudienuebersichtDbContext context;

        public EFCoreRepository()
        {
            context = new StudienuebersichtDbContext();
            context.Database.Migrate();

            module_repository = new EFCoreModuleRepository(context);
            account_repository = new EFCoreAccountRepository(context);

            if (context.Accounts.Any())
            {
                account_repository.Save(new Account()
                {
                    IsAdmin = true,
                    EMail = "Eric.epp@stud.hshl.de",
                    Password = BCrypt.Net.BCrypt.HashPassword("test")
                });
            }
        }

        public IAccountRepository Accounts
        {
            get { return account_repository; }
        }
        public IModuleRepository Modules
        {
            get { return module_repository; }
        }
    }
}