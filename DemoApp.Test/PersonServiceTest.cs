using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DemoApp.Data;
using DemoApp.Services;
using DemoApp.Data.Repository;
using DemoApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using DemoApp.Services.CustomExceptions;
using DemoApp.Services.DTO;

namespace DemoApp.Test
{
    [TestClass]
    public class PersonServiceTest
    {
        private Mock<IUnitOfWork> _mockUow;
        private IPersonnelService _service;
        List<Person> listPeople;

        [TestInitialize]
        public void Initialise()
        {
            //Set some default data
            listPeople = new List<Person>();
            listPeople.Add(new Person { Age = 51, EmailAddress = "jsmith@gmail.com", FirstName = "John", LastName = "Smith", PersonId = 1 });
            listPeople.Add(new Person { Age = 45, EmailAddress = "rachaelgreen@aol.com", FirstName = "Rachael", LastName = "Green", PersonId = 2 });
            listPeople.Add(new Person { Age = 24, EmailAddress = "anthonywest@gmail.com", FirstName = "Anthony", LastName = "West", PersonId = 3 });

            //I'll mock a repository; note that as repository is just
            //a thin wrapper around EF's own repository, I'm not
            //going to test the repository as part of this demo, though
            //I could.
            var mockRepo = new Mock<IRepository<Person>>();
            mockRepo.Setup(x => x.GetAll()).Returns(listPeople);
            mockRepo.Setup(x => x.Add(It.IsAny<Person>())).Callback(
                (Person entity) =>
                {
                    listPeople.Add(entity);
                });
            mockRepo.Setup(x => x.FindById(It.IsAny<int>())).Returns(
                (int i) => listPeople.Where(
                    x => x.PersonId == i).Single()
                );
            mockRepo.Setup(x => x.Remove(It.IsAny<Person>())).Callback(
                (Person entity) =>
                {
                    listPeople.Remove(entity);
                });

            _mockUow = new Mock<IUnitOfWork>();
            _mockUow.Setup(x => x.PersonRepository).Returns(mockRepo.Object);
            
            //Using IoC/DI in unit tests is poor practice; the DI is not a service locator.
            _service = new PersonnelService(_mockUow.Object);
        }

        [TestMethod]
        public void People_Get_All_Returns_Initial_Collection()
        {
            //Arrange
            //Act
            List<PersonDto> people = _service.GetAll() as List<PersonDto>;
            //Assert
            Assert.IsTrue(people.Count == 3);
        }

        [TestMethod]
        public void People_Add_Duplicate_Email_Throws_Exception()
        {
            //Arrange
            PersonDto newPerson = new PersonDto()
            {
                Age = 30,
                FirstName = "John",
                LastName = "Smith",
                EmailAddress = "jsmith@gmail.com"
            };

            //Act and assert
            try
            {
                _service.AddPerson(newPerson);
                Assert.Fail("Exception not thrown");
            }
            catch(PersonExistsException)
            {
                _mockUow.VerifyAll();
            }
        }

        [TestMethod]
        public void People_Insert_Unique_Email()
        {
            //Arrange
            PersonDto newPerson = new PersonDto()
            {
                Age = 51,
                FirstName = "John",
                LastName = "Smith",
                EmailAddress = "john.smith@gmail.com"
            };

            //Act
            _service.AddPerson(newPerson);

            //Assert
            Assert.IsNotNull(_service.GetAll().FirstOrDefault(x => x.EmailAddress == "john.smith@gmail.com"));
        }

        [TestMethod]
        public void People_Remove_Existing_By_PersonDto()
        {
            //Arrange
            PersonDto personDto = new PersonDto()
            {
                Age = 45,
                FirstName = "Rachael",
                LastName = "Green",
                EmailAddress = "rachaelgreen@aol.com",
                PersonDtoId = 2
            };

            //Act
            _service.RemovePerson(personDto);

            //Assert
            Assert.IsTrue(_service.GetAll().Count == 2);
        }
    }
}
