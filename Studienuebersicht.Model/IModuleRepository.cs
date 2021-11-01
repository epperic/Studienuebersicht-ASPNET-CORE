using System;
using System.Collections.Generic;

namespace Studienuebersicht.Model
{
    public interface IModuleRepository
    {
        List<Module> GetAll();
        Module ById(Guid id);
        public List<Module> GetSemester(List<Module> modules, int semester);
        public List<Module> GetGrades(List<Module> modules);
        public List<Module> GetToDo(List<Module> modules, int activeSemester);
        int calcAllECTS(List<Module> modules);
        double calcAverageGrade(List<Module> modules);
        void Save(Module module);
        void Delete(Guid id);
    }
}
