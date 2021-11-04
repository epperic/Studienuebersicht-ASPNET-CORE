using Microsoft.AspNetCore.Mvc;
using Studienuebersicht.Model;
using System;

namespace Studienuebersicht.API.Controllers
{
    public class ModuleController : Controller
    {
        private IRepository repository;

        public ModuleController(IRepository repository)
        {
            this.repository = repository;
        }

        [Route("/Module/Add")]
        [HttpPost]
        public IActionResult Add([FromBody] Module module)
        {
            repository.Modules.Save(module);
            return new OkObjectResult("Module added successfully!");
        }

        [Route("/Module/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(repository.Modules.GetAll());
        }

        [Route("/Semester/{semester}")]
        [HttpGet]
        public IActionResult GetSemester(int semester)
        {
            return new OkObjectResult(repository.Modules.GetSemester(semester));
        }

        [Route("/Grades")]
        [HttpGet]
        public IActionResult GetGrades()
        {
            return new OkObjectResult(repository.Modules.GetGrades());
        }

        [Route("/ToDo/{currentSemester}")]
        [HttpGet]
        public IActionResult GetToDo(int currentSemester)
        {
            return new OkObjectResult(repository.Modules.GetToDo(currentSemester));
        }

        [Route("/Module/{id}")]
        [HttpGet]
        public IActionResult GetSingle(Guid id)
        {
            var module = repository.Modules.ById(id);

            if (module == null)
                return new ConflictObjectResult("Module with that id does not exist!");

            return new OkObjectResult(module);
        }

        [Route("/Module/{id}")]
        [HttpPut]
        public IActionResult Update(Guid id, [FromBody] Module module)
        {
            if (repository.Modules.ById(id) == null)
                return new ConflictObjectResult("Module with that id does not exist!");

            module.Id = id;
            repository.Modules.Save(module);
            return new OkObjectResult("Module updated successfully!");
        }

        [Route("/Module/{id}")]
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (repository.Modules.ById(id) == null)
                return new ConflictObjectResult("Module with that id does not exist!");

            repository.Modules.Delete(id);
            return new OkObjectResult("Module deleted successfully!");
        }
    }
}