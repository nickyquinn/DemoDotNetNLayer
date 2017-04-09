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
    /// <summary>
    /// Tests the concrete implementation PersonnelService using in-memory
    /// storage and mocks.
    /// </summary>
    [TestClass]
    public class PersonServiceTest
    {
        private Mock<IUnitOfWork> _mockUow;
        private IPersonnelService _service;
        List<Person> _listPeople;

        [TestInitialize]
        public void Initialise()
        {
            //Set some default data
            _listPeople = new List<Person>();
            _listPeople.Add(new Person { Age = 51, EmailAddress = "jsmith@gmail.com", FirstName = "John", LastName = "Smith", PersonId = 1 });
            _listPeople.Add(new Person { Age = 45, EmailAddress = "rachaelgreen@aol.com", FirstName = "Rachael", LastName = "Green", PersonId = 2 });
            _listPeople.Add(new Person { Age = 24, EmailAddress = "anthonywest@gmail.com", FirstName = "Anthony", LastName = "West", PersonId = 3 });

            //I'll mock a repository; note that as repository is just
            //a thin wrapper around EF's own repository, I'm not
            //going to test the repository as part of this demo, though
            //I could.
            var mockRepo = new Mock<IRepository<Person>>();
            mockRepo.Setup(x => x.GetAll()).Returns(_listPeople);
            mockRepo.Setup(x => x.Add(It.IsAny<Person>())).Callback(
                (Person entity) =>
                {
                    _listPeople.Add(entity);
                });
            mockRepo.Setup(x => x.FindById(It.IsAny<int>())).Returns(
                (int i) => _listPeople.SingleOrDefault(x => x.PersonId == i)
                );
            mockRepo.Setup(x => x.Remove(It.IsAny<Person>())).Callback(
                (Person entity) =>
                {
                    _listPeople.Remove(entity);
                });

            _mockUow = new Mock<IUnitOfWork>();
            _mockUow.Setup(x => x.PersonRepository).Returns(mockRepo.Object);

            //Using IoC/DI in unit tests is poor practice; these are unit tests,
            //they are not integration tests. For a suite of integration tests
            //I would do away with Moq as much as possible and utilise a Portable
            //SQL database or similar (see also: https://effort.codeplex.com/)
            _service = new PersonnelService(_mockUow.Object);
        }

        [TestMethod]
        public void People_Get_All_Returns_Initial_Collection()
        {
            //Arrange
            //Act
            List<PersonDto> people = _service.GetAll() as List<PersonDto>;
            //Assert
            Assert.IsTrue(people != null && people.Count == 3);
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

        [TestMethod]
        public void People_Remove_NonExistant_Throws_Exception()
        {
            //Arrange
            PersonDto personDto = new PersonDto()
            {
                Age = 66,
                FirstName = "Jim",
                LastName = "Davidson",
                EmailAddress = "jimdavidson@aol.com",
                PersonDtoId = 6
            };

            //Act and assert
            try
            {
                _service.RemovePerson(personDto);
                Assert.Fail("Exception not thrown");
            }
            catch (PersonNotExistsException)
            {
                _mockUow.VerifyAll();
            }
        }

        [TestMethod]
        public void People_Update_NonExistant_Throws_Exception()
        {
            //Arrange
            PersonDto personDto = new PersonDto()
            {
                Age = 66,
                FirstName = "Jim",
                LastName = "Davidson",
                EmailAddress = "jimdavidson@aol.com",
                PersonDtoId = 6
            };

            //Act and assert
            try
            {
                _service.UpdatePerson(personDto);
                Assert.Fail("Exception not thrown");
            }
            catch (PersonNotExistsException)
            {
                _mockUow.VerifyAll();
            }
        }
    }
}
