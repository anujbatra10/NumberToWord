using System.ComponentModel.DataAnnotations;

namespace NumbersToWordsConverter.Models
{
    public class UserModel
    {

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter valid name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]

        [Range(0, double.MaxValue, ErrorMessage = ("Please enter valid number"))]
        [Display(Name = "Number")]
        public double Number { get; set; }



    }
}