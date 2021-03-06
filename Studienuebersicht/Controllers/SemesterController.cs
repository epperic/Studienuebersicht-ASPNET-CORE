using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Studienuebersicht.Model;
using Studienuebersicht.MVC.ViewModel;
using System;

namespace Studienuebersicht.MVC.Controllers
{
    public class SemesterController : Controller
    {
        private ILogger<SemesterController> logger;
        private IRepository repository;
        private ModuleTableViewModel semesterTable;

        //prepares the ViewModel 
        private void initializeSemester(int id)
        {
            var semester = repository.Modules.GetSemester(id);
            semesterTable = new ModuleTableViewModel
            {
                Modules = semester,
                AllECTS = repository.Modules.calcAllECTS(semester),
                AverageGrade = repository.Modules.calcAverageGrade(semester)
            };
        }

        public SemesterController(ILogger<SemesterController> logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index(int id)
        {
            logger.LogInformation("Index method called");
            if (id != 0)
            {
                initializeSemester(id);
                return View(semesterTable);
            }
            else
            {
                return Redirect($"~/Semester/1");
            }
        }

        public IActionResult Add()
        {
            var obj = new ModuleViewModel();
            return View("Edit", obj);
        }

        public IActionResult Edit(Guid id)
        {
            var module = repository.Modules.ById(id);
            var viewModel = ModuleViewModel.Convert(module);

            if (module != null)
                return View("Edit", viewModel);
            else
                return NotFound();
        }

        public IActionResult Save([FromForm] ModuleViewModel moduleViewModel)
        {
            if (!ModelState.IsValid)
                return View("Edit", moduleViewModel);

            var module = ModuleViewModel.Convert(moduleViewModel);
            repository.Modules.Save(module);
            return Redirect($"~/Semester/{module.Semester}");
        }

        public IActionResult Delete(Guid id)
        {
            var module = repository.Modules.ById(id);
            if (module != null)
            {
                repository.Modules.Delete(id);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                return NotFound();
            }
        }
    }
}