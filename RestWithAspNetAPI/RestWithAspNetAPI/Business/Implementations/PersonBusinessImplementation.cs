﻿using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;

namespace RestWithAspNetAPI.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IRepository<Person> _personRepository;
        private readonly PersonVO _personVo;

        public PersonBusinessImplementation(IRepository<Person> personRepository, PersonVO personVo)
        {
            _personRepository = personRepository;
            _personVo = personVo;
        }

        public Person Create(Person person)
        {
            _personRepository.Create(person);
            return person;
        }

        public List<Person> FindAll()
        {
           return _personRepository.FindAll();
        }

        public Person FindById(long id)
        {
            return _personRepository.FindById(id);
        }

        public Person Update(Person person)
        {
           _personRepository.Update(person);
            return person;
        }

        public Person Delete(int id)
        {
            var person = _personRepository.FindById(id);
            _personRepository.Delete(id);
            return person;
        }
    }
}