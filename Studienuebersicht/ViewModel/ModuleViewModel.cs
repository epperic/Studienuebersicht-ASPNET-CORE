using Studienuebersicht.Model;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Studienuebersicht.MVC.ViewModel
{
    public class ModuleViewModel : Entity
    {
        [Required]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Value for {0} must be between {1} and {2} characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Value for {0} must be between {1} and {2} characters.")]
        public string Professor { get; set; }

        [Display(Name = "ECTS")]
        [Required]
        [Range(2, 30,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Ects { get; set; }

        [Required]
        [RegularExpression(@"^(\d)*(\.)?([0-6]{1})?$", ErrorMessage = "Must be a Number & seperated with a dot.")]
        public string Grade { get; set; }

        [Required]
        [Range(1, 8,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Semester { get; set; }

        public static Module Convert(ModuleViewModel viewModel)
        {
            return new Module()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Professor = viewModel.Professor,
                Ects = viewModel.Ects,
                Grade = double.Parse(viewModel.Grade, CultureInfo.InvariantCulture),
                Semester = viewModel.Semester
            };
        }

        public static ModuleViewModel Convert(Module module)
        {
            return new ModuleViewModel()
            {
                Id = module.Id,
                Name = module.Name,
                Professor = module.Professor,
                Ects = module.Ects,
                Grade = module.Grade.ToString(CultureInfo.InvariantCulture),
                Semester = module.Semester
            };
        }
    }
}