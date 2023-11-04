using Microsoft.EntityFrameworkCore;
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
            if (!Exists(person.Id)) return new Person();

            var result = _mySQLContext?.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    // set changes and save
                    _mySQLContext?.Entry(result).CurrentValues.SetValues(person);
                    _mySQLContext?.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;

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

        private bool Exists(long id)
        {
            var personInDb = _mySQLContext?.Persons.Any(p => p.Id.Equals(id));
            
            return personInDb.HasValue;
        }
    }
}