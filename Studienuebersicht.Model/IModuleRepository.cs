using System;
using System.Collections.Generic;

namespace Studienuebersicht.Model
{
    public interface IModuleRepository
    {
        List<Module> GetAll();
        Module ById(Guid id);
        int calcAllECTS(List<Module> modules);
        double calcAverageGrade(List<Module> modules);
        void Save(Module module);
        void Delete(Guid id);
    }
}
