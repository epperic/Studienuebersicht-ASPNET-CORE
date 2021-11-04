using System;
using System.Collections.Generic;
using System.Linq;

namespace Studienuebersicht.Model
{
    public class MemoryModuleRepository : IModuleRepository
    {
        private List<Module> modules = new List<Module>();

        public List<Module> GetAll()
        {
            return modules;
        }

        private int PosOf(Guid Id)
        {
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].Id == Id)
                    return i;
            }
            return -1;
        }
        public Module ById(Guid id)
        {
            var pos = PosOf(id);
            if (pos != -1)
                return modules[pos];
            else
                return null;
        }

        //Extracts only the modules with given semester and puts them in a list
        public List<Module> GetSemester(int semester)
        {
            var filteredObjects = new List<Module>();
            modules.ForEach(delegate (Module module)
            {
                if (module.Semester == semester)
                {
                    filteredObjects.Add(module);
                }
            });
            return filteredObjects.OrderBy(x => x.Name).ToList();
        }

        //Gets only the Modules where a valid Grade != 0.0 has been submitted
        public List<Module> GetGrades()
        {
            var filteredObjects = new List<Module>();
            modules.ForEach(delegate (Module module)
            {
                if (module.Grade != 0.0)
                {
                    filteredObjects.Add(module);
                }
            });
            return filteredObjects.OrderBy(x => x.Semester).ToList();
        }

        //Gets only the Modules where the Grade has not been put in yet (Grade == 0.0) & Semester is < currentSemester 
        public List<Module> GetToDo(int currentSemester)
        {
            var filteredObjects = new List<Module>();
            modules.ForEach(delegate (Module module)
            {
                if (module.Grade == 0 && module.Semester < currentSemester)
                {
                    filteredObjects.Add(module);
                }
            });
            return filteredObjects.OrderBy(x => x.Semester).ToList();
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

        public void Delete(Guid id)
        {
            var pos = PosOf(id);
            if (pos != -1)
                modules.RemoveAt(pos);
        }

        public void Save(Module module)
        {
            if (module.Id == Guid.Empty)
            {
                module.Id = Guid.NewGuid();
                modules.Add(module);
            }
            else
            {
                var pos = PosOf(module.Id);
                modules[pos] = module;
            }
        }
    }
}
