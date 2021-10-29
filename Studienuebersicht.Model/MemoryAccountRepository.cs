using System;
using System.Collections.Generic;
using System.Linq;

namespace Studienuebersicht.Model
{
    public class MemoryAccountRepository : IAccountRepository
    {
        private List<Account> accounts = new List<Account>();

        public Account FindAdminByEmailAndPassword(string email, string password)
        {
            return (from acc in accounts
                    where acc.IsAdmin && acc.EMail.Equals(email) && acc.Password.Equals(password)
                    select acc).SingleOrDefault();
        }

        public void Save(Account account)
        {
            if (account.Id == Guid.Empty)
            {
                account.Id = Guid.NewGuid();
                accounts.Add(account);
            }
        }
    }
}
