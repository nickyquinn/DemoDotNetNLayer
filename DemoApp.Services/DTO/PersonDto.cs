using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Services.DTO
{
    public class PersonDto
    {
        public int PersonDtoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
    }
}
