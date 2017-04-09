using DemoApp.Data.Entities;
using DemoApp.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Services
{
    public interface IPersonnelService
    {
        /// <summary>
        /// Get all Person DTO objects in the system
        /// </summary>
        /// <returns></returns>
        IList<PersonDto> GetAll();

        /// <summary>
        /// Add a person DTO to the system.
        /// </summary>
        /// <param name="person"></param>
        void AddPerson(PersonDto person);

        /// <summary>
        /// Remove a person from the system
        /// </summary>
        /// <param name="person"></param>
        void RemovePerson(PersonDto person);

        /// <summary>
        /// Updates an existing person in the system
        /// </summary>
        /// <param name="person"></param>
        void UpdatePerson(PersonDto person);
    }
}
