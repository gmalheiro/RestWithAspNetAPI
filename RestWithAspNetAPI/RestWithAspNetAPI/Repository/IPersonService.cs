﻿using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Repository
{
    public interface IPersonService
    {
        Person Create(Person person);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Person person);

        Person Delete(int id);    
    }
}
