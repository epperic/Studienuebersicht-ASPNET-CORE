﻿using System;
using System.Collections.Generic;

namespace Studienuebersicht.Model
{
    public class MemoryModuleRepository : IModuleRepository
    {
        private List<Module> modules = new List<Module>();
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

        public List<Module> GetAll()
        {
            return modules;
        }

        public void Save(Module module)
        {
            if (module.Id == null)
            {
                module.Id = new Guid();
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