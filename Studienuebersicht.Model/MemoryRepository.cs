using System;

namespace Studienuebersicht.Model
{
    public class MemoryRepository : IRepository
    {
        private MemoryAccountRepository account_repository;
        private MemoryModuleRepository module_repository;

        public MemoryRepository()
        {
            account_repository = new MemoryAccountRepository();
            module_repository = new MemoryModuleRepository();

            CreateDummyData();
        }

        private void CreateDummyData()
        {
            var a1 = new Account()
            {
                IsAdmin = true,
                EMail = "Eric.epp@stud.hshl.de",
                Password = BCrypt.Net.BCrypt.HashPassword("test")
            };

            var a2 = new Account()
            {
                IsAdmin = false,
                EMail = "test@test.de",
                Password = BCrypt.Net.BCrypt.HashPassword("test")
            };

            account_repository.Save(a1);
            account_repository.Save(a2);

            var m1 = new Module()
            {
                Id = new Guid(),
                Name = "Grundlagen der Informatik I",
                Professor = "Stuckenholz",
                Ects = 8,
                Grade = 2.5,
                Semester = 1
            };

            var m2 = new Module()
            {
                Id = new Guid(),
                Name = "Mathematik I",
                Professor = "Ponick",
                Ects = 6,
                Grade = 1.3,
                Semester = 1
            };

            var m3 = new Module()
            {
                Id = new Guid(),
                Name = "Naturwissenschaftliche Grundlagen",
                Professor = "Berndt/Kientopf/Amann",
                Ects = 9,
                Grade = 1.7,
                Semester = 1
            };

            var m4 = new Module()
            {
                Id = new Guid(),
                Name = "Personal Skills I",
                Professor = "Grewe",
                Ects = 3,
                Grade = 1.7,
                Semester = 1
            };

            var m5 = new Module()
            {
                Id = new Guid(),
                Name = "Technisches Englisch I",
                Professor = "Strack",
                Ects = 4,
                Grade = 1.3,
                Semester = 1
            };

            module_repository.Save(m1);
            module_repository.Save(m2);
            module_repository.Save(m3);
            module_repository.Save(m4);
            module_repository.Save(m5);
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
