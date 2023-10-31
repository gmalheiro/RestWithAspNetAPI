using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        public volatile int count;
        
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(int id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person FindById(int id)
        {
            return new Person 
            {
                Id = 1,
                Name = "Gabriel Malheiro",
                Address = "São Paulo - São Paulo - Brasil",
                Gender  = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                Name = "Gabriel Malheiro",
                Address = "São Paulo - São Paulo - Brasil",
                Gender = "Male"
            };
        }

        private int IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
