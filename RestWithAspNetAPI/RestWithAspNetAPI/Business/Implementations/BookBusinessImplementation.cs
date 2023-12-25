using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Data.Converter.Implementations;
using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Hypermedia.Utils;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;

namespace RestWithAspNetAPI.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IRepository<Book> _bookRepository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> bookRepository , BookConverter converter)
        {
            _bookRepository = bookRepository;
            _converter = converter;
        }

        public BookVO Create(BookVO  book)
        {
            var bookEntity = _converter.Parse(book);
            _bookRepository.Create(bookEntity);
            return book;
        }

        public List<BookVO> FindAll()
        {
            var books = _converter.Parse(_bookRepository.FindAll());
            return books;
        }

        public BookVO FindById(long id)
        {
            var bookVO = _converter.Parse(_bookRepository.FindById(id));
            return bookVO;
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity= _converter.Parse(book);
            _bookRepository.Update(bookEntity);
            return book;
        }

        public BookVO Delete(int id)
        {
            var book = _bookRepository.FindById(id);
            _bookRepository.Delete(id);
            var bookVO = _converter.Parse(book);    
            return bookVO;
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch(
           string? title, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from books p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(title)) query = query + $" and p.title like '%{title}%' ";
            query += $" order by p.title {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from books p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(title)) countQuery = countQuery + $" and p.title like '%{title}%' ";

            var books = _bookRepository.FindWithPagedSearch(query);
            int totalResults = _bookRepository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

    }
}