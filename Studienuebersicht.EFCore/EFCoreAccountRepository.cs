using Microsoft.EntityFrameworkCore;
using Studienuebersicht.Model;
using System.Linq;

namespace Studienuebersicht.EFCore
{
    public class EFCoreAccountRepository : IAccountRepository
    {
        private StudienuebersichtDbContext context;

        public EFCoreAccountRepository(StudienuebersichtDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var account = context.Accounts.Find(id);
            if (account != null)
            {
                context.Accounts.Remove(account);
                context.SaveChanges();
            }
        }

        public Account FindAccount(string email, string password)
        {
            var list = (from account in context.Accounts
                        where account.EMail.Equals(email)
                        select account).AsNoTracking().ToList();

            foreach (var account in list)
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                    return account;

            return null;
        }


        public void Save(Account account)
        {
            if (account.Id == 0)
                context.Add(account);
            else
                context.Accounts.Attach(account).State = EntityState.Modified;

            context.SaveChanges();
            context.Entry(account).State = EntityState.Detached;
        }

        Account IAccountRepository.ById(int id)
        {
            return (from account in context.Accounts where account.Id == id select account)
                .AsNoTracking().FirstOrDefault();
        }
    }
}