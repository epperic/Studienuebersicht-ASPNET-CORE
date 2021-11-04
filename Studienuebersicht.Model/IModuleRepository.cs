using System;
using System.Collections.Generic;

namespace Studienuebersicht.Model
{
    public interface IModuleRepository
    {
        List<Module> GetAll();
        Module ById(Guid id);
        public List<Module> GetSemester(int semester);
        public List<Module> GetGrades();
        public List<Module> GetToDo(int currentSemester);
        int calcAllECTS(List<Module> modules);
        double calcAverageGrade(List<Module> modules);
        void Save(Module module);
        void Delete(Guid id);
    }
}
