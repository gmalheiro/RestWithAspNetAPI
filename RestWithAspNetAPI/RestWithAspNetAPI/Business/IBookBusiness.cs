using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);

        Book FindById(long id);

        List<Book> FindAll();

        Book Update(Book book);

        Book Delete(int id);    
    }
}
