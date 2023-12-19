using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable (long id);
        List<Person> FindByName(string firstName, string secondName);
    }
}
