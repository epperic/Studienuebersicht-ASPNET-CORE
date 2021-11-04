using System.Collections.Generic;
using System.Linq;

namespace Studienuebersicht.Model
{
    public class MemoryAccountRepository : IAccountRepository
    {
        private List<Account> accounts = new List<Account>();

        public Account FindAccount(string email, string password)
        {
            var account = (from acc in accounts
                           where acc.EMail.Equals(email)
                           select acc).SingleOrDefault();

            if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
                return account;
            else
                return null;
        }

        public Account ById(int id)
        {
            var pos = PosOf(id);
            if (pos != -1)
                return accounts[pos];
            else
                return null;
        }
        private int PosOf(int id)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Id == id)
                    return i;
            }

            return -1;
        }
        public void Save(Account account)
        {
            if (account.Id == 0)
            {
                account.Id = accounts.Count + 1;
                accounts.Add(account);
            }
            else
            {
                var pos = PosOf(account.Id);
                accounts[pos] = account;
            }
        }
    }
}
