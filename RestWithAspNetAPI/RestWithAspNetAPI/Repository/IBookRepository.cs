using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);

        Book FindById(long id);

        List<Book> FindAll();

        Book Update(Book book);

        Book Delete(int id);

        bool Exists(long id);
    }
}
