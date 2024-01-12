using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ManageInformation.Domain.Model;
using ManageInformation.Infrastructure;
using System.Numerics;
using ManageInformation.Infrastructure.Repos;

namespace TestProject2
{
    public class TestPersonRepository
    {
        [Fact]
        //Тест, проверяющий, что база данных создалась
        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;

            Assert.Equal(1, 1);
        }

        [Fact]
        public void TestAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = new Person
            {
                Id = 0,
                MVDId = 101,
                NalogovayaId = 101,
                GIBDDId = 101,
                PFRId = 101,
                 
            };
            
            personRepository.CreatePerson(person);//.Wait();
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            //personRepository.ChangeTrackerClear();

            Assert.Equal(101, personRepository.GetPersonsById(person.Id).MVDId);
            Assert.Equal(101, personRepository.GetPersonsById(person.Id).PFRId);
            Assert.Equal(101, personRepository.GetPersonsById(person.Id).GIBDDId);
           
        }

        [Fact]
        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = personRepository.GetPersonsById(1);
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            //personRepository.ChangeTrackerClear();
            person.MVDId = 1000;
            personRepository.UpdatePerson(person);

            Assert.Equal(1000, personRepository.GetPersonsById(51).MVDId);
        }

        [Fact]
        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = personRepository.GetPersonsById(51);
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
           // personRepository.ChangeTrackerClear();
            personRepository.DeletePerson(person);

            Assert.Equal(51, personRepository.GetPersonsById(51).Id);
        }
    }

}
