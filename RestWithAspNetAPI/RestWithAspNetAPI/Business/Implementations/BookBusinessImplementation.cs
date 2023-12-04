using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Data.Converter.Implementations;
using RestWithAspNetAPI.Data.VO;
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
    }
}