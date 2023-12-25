using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Hypermedia.Utils;
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
        PagedSearchVO<BookVO> FindWithPagedSearch(string? title, string sortDirection, int pageSize, int page);
    }
}
