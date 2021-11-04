using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Studienuebersicht.Model;
using Studienuebersicht.MVC.ViewModel;
using System.Collections.Generic;

namespace Studienuebersicht.MVC.Controllers
{
    public class ToDoController : Controller
    {
        private ILogger<ToDoController> logger;
        private IRepository repository;
        private ToDoViewModel viewModel;

        //prepares the viewModel
        private void initializeViewModel(int currentSemester)
        {
            var toDo = repository.Modules.GetToDo(currentSemester);
            viewModel = new ToDoViewModel
            {
                Modules = toDo,
                HasSubmitted = true,
                CurrentSemester = currentSemester
            };
        }
        public ToDoController(ILogger<ToDoController> logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
            viewModel = new ToDoViewModel() { Modules = new List<Module>(), CurrentSemester = 0, HasSubmitted = false };
        }

        public IActionResult Index()
        {
            logger.LogInformation("Index method called");
            return View(viewModel);
        }

        public IActionResult Current([FromForm] int currentSemester)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            if (currentSemester <= 8 && currentSemester > 0)
            {
                initializeViewModel(currentSemester);
                return View("Index", viewModel);
            }
            else
            {
                return Redirect($"~/ToDo");
            }

        }
    }
}
