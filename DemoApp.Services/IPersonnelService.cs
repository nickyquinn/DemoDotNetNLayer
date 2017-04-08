using DemoApp.Data.Entities;
using DemoApp.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Services
{
    public interface IPersonnelService
    {
        IList<PersonDto> GetAll();
        void AddPerson(PersonDto person);
        void RemovePerson(PersonDto person);
        void UpdatePerson(PersonDto person);
    }
}
