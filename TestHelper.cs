using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageInformation.Domain.Model;
using ManageInformation.Infrastructure.Data;
using ManageInformation.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;

namespace TestProject2
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            //Используем базу обычную базу данных, не в памяти
            //Имя тестовой базы данных должно отличатсья от базы данных проекта
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
                .Options;

            _context = new Context(contextOptions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
            var person1 = new Person
            {
                Id = 0,
                MVDId = 100,
                NalogovayaId = 100,
                GIBDDId = 400,
                PFRId = 100,

            };
            
           
            _context.person.Add(person1);
            _context.SaveChanges();
            //Запрещаем отслеживание (разрываем связи с БД)
            _context.ChangeTracker.Clear();
        }

        public PersonRepo PersonRepository
        {
            get
            {
                return new PersonRepo(_context);
            }
        }
    }
}
