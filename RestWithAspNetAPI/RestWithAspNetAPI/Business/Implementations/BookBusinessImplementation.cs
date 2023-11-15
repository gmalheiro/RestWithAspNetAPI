using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;

namespace RestWithAspNetAPI.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IBookRepository _bookRepository;

        public BookBusinessImplementation(IBookRepository bookRepository )
        {
            _bookRepository = bookRepository;
        }

        public Book Create(Book  book)
        {
            _bookRepository.Create(book);
            return book;
        }

        public List<Book> FindAll()
        {
           return _bookRepository.FindAll();
        }

        public Book FindById(long id)
        {
            return _bookRepository.FindById(id);
        }

        public Book Update(Book book)
        {
           _bookRepository.Update(book);
            return book;
        }

        public Book Delete(int id)
        {
            var book = _bookRepository.FindById(id);
            _bookRepository.Delete(id);
            return book;
        }
    }
}