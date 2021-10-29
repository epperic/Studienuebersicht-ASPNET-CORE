using System;
using System.Collections.Generic;

namespace Studienuebersicht.Model
{
    public interface IModuleRepository
    {
        List<Module> GetAll();
        Module ById(Guid id);
        public List<Module> GetSemester(List<Module> modules, int semester);
        int calcAllECTS(List<Module> modules);
        double calcAverageGrade(List<Module> modules);
        void Save(Module module);
        void Delete(Guid id);
    }
}
