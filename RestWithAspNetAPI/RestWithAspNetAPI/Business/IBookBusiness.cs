using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindById(long id);

        List<BookVO> FindAll();

        BookVO Update(BookVO book);

        BookVO Delete(int id);    
    }
}
