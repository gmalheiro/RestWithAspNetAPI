using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Services;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {

        private readonly MySQLContext? _mySQLContext;

        public PersonServiceImplementation(MySQLContext mySQLContext)
        {
            _mySQLContext = mySQLContext;
        }

        public Person Create(Person person)
        {
            var personToBeInserted = person;

            if (personToBeInserted is null)
            {
                return new Person();
            }
            else
            {
                _mySQLContext?.Add(personToBeInserted);
                _mySQLContext?.SaveChanges();
                return personToBeInserted;
            }

        }

        public List<Person> FindAll()
        {
            var people = new List<Person>();

            people = _mySQLContext?.Persons.ToList();

            if (people?.Count > 0)
                return people;
            else
                return new List<Person>();

        }

        public Person FindById(long id)
        {
            var person = new Person();

            person = _mySQLContext?.Persons.FirstOrDefault(p => p.Id == id);

            if (person is null)
                return new Person();
            else return person;
        }

        public Person Update(Person person)
        {
            var personToBeUpdated = new Person();
            
            if(person is null)
            {
                return new Person();
            }
            else
            {
                personToBeUpdated = _mySQLContext?.Persons.Find(person.Id) ?? new Person();
                
                personToBeUpdated.FirstName = person?.FirstName ?? "";

                personToBeUpdated.LastName = person?.LastName ?? "";

                personToBeUpdated.Address = person?.Address ?? "";

                personToBeUpdated.Gender = person?.Gender ?? "";

                _mySQLContext?.SaveChanges();

                return personToBeUpdated;

            }

        }

        public Person Delete(int id)
        {
            var person = new Person();

            person = _mySQLContext?.Persons.FirstOrDefault(p => p.Id == id);

            if (person is null)
            {
                return new Person();
            }
            else
            {
                _mySQLContext?.Remove(person);
                _mySQLContext?.SaveChanges();
                return person;
            }
        }
    }
}