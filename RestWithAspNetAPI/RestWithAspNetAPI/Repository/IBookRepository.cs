using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Repository
{
    public interface IBokRepository
    {
        Person Create(Book book);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Book book);

        Person Delete(int id);

        bool Exists(long id);
    }
}
