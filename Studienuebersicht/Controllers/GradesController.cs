using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Studienuebersicht.Model;
using Studienuebersicht.MVC.ViewModel;

namespace Studienuebersicht.MVC.Controllers
{
    public class GradesController : Controller
    {
        private ILogger<GradesController> logger;
        private IRepository repository;
        private ModuleTableViewModel gradesTable;

        //prepares the viewModel
        private void initializeTableViewModel()
        {
            var grades = repository.Modules.GetGrades();
            gradesTable = new ModuleTableViewModel
            {
                Modules = grades,
                AllECTS = repository.Modules.calcAllECTS(grades),
                AverageGrade = repository.Modules.calcAverageGrade(grades)
            };
        }
        public GradesController(ILogger<GradesController> logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Index method called");
            initializeTableViewModel();
            return View(gradesTable);
        }

    }
}
