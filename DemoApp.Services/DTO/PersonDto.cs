using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.Services.DTO
{
    public class PersonDto
    {
        public int PersonDtoId { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email address")]
        [RegularExpression("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$", 
            ErrorMessage= "You must enter a valid email address")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
