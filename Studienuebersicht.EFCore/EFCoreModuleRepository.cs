using Microsoft.EntityFrameworkCore;
using Studienuebersicht.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Studienuebersicht.EFCore
{
    public class EFCoreModuleRepository : IModuleRepository
    {
        private StudienuebersichtDbContext context;

        public EFCoreModuleRepository(StudienuebersichtDbContext context)
        {
            this.context = context;
        }

        public List<Module> GetAll()
        {
            return context.Modules.OrderBy(x => x.Semester).ThenByDescending(x => x.Name).AsNoTracking().ToList();
        }

        public void Delete(Guid id)
        {
            var module = context.Modules.Find(id);
            if (module != null)
            {
                context.Modules.Remove(module);
                context.SaveChanges();
            }
        }


        public void Save(Module module)
        {
            if (module.Id == Guid.Empty)
            {
                module.Id = Guid.NewGuid();
                context.Add(module);
            }
            else
            {
                context.Modules.Attach(module).State = EntityState.Modified;
            }
            context.SaveChanges();
            context.Entry(module).State = EntityState.Detached;
        }

        public Module ById(Guid id)
        {
            return context.Modules.Where(x => x.Id == id).FirstOrDefault();
        }

        //Extracts only the modules with given semester and puts them in a list
        public List<Module> GetSemester(int semester)
        {
            return context.Modules.Where(x => x.Semester == semester).ToList();
        }

        //Gets only the Modules where a valid Grade != 0.0 has been submitted
        public List<Module> GetGrades()
        {
            return context.Modules.Where(x => x.Grade != 0.0).ToList();
        }

        //Gets only the Modules where the Grade has not been put in yet (Grade == 0.0) & Semester is < currentSemester 
        public List<Module> GetToDo(int currentSemester)
        {
            return context.Modules.Where(x => x.Grade == 0 && x.Semester < currentSemester).ToList();
        }

        //calculates the sum of all ECTS, where a grade has been registered
        public int calcAllECTS(List<Module> modules)
        {
            int collectedECTS = 0;
            modules.ForEach(delegate (Module module)
            {
                if (module.Grade != 0.0)
                {
                    collectedECTS += (module.Ects * 1);
                }
            });
            return collectedECTS;
        }

        //calculates the average grade of the given list of modules
        public double calcAverageGrade(List<Module> modules)
        {
            double sum = 0;
            double count = 0;
            modules.ForEach(delegate (Module module)
            {
                if (module.Grade != 0.0)
                {
                    sum += module.Grade * 1;
                    count++;
                }
            });
            return sum / count;
        }
    }
}