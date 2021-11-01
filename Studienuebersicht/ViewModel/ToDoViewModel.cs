using Studienuebersicht.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Studienuebersicht.MVC.ViewModel
{
    public class ToDoViewModel
    {
        public List<Module> Modules { get; set; }

        [Display(Name = "current Semester")]
        [Required]
        [Range(1, 8,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int CurrentSemester { get; set; }
        public bool HasSubmitted { get; set; }
    }
}