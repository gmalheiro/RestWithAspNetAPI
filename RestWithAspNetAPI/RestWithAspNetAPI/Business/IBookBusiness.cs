using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Business
{
    public interface IBookBusiness
    {
        Person Create(Book book);

        Person FindById(long id);

        List<Book> FindAll();

        Person Update(Book book);

        Person Delete(int id);    
    }
}
