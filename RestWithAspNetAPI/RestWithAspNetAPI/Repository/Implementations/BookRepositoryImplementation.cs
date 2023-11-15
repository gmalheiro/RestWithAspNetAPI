using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;

namespace RestWithAspNetAPI.Repository.Implementations
{
    public class BookRepositoryImplementation: IBookRepository
    {

        private readonly MySQLContext? _mySQLContext;

        public BookRepositoryImplementation(MySQLContext mySQLContext)
        {
            _mySQLContext = mySQLContext;
        }

        public Book Create(Book book)
        {
            var bookToBeInserted = book;

            if (bookToBeInserted is null)
            {
                return new Book();
            }
            else
            {
                _mySQLContext?.Add(bookToBeInserted);
                _mySQLContext?.SaveChanges();
                return bookToBeInserted;
            }

        }

        public List<Book> FindAll()
        {
            var book = new List<Book>();

            book = _mySQLContext?.Books.ToList();

            if (book?.Count > 0)
                return book;
            else
                return new List<Book>();

        }

        public Book FindById(long id)
        {
            var book = new Book();

            book = _mySQLContext?.Books.FirstOrDefault(b => b.Id == id);

            if (book is null)
                return new Book();

            else return book;
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return new Book();

            var result = _mySQLContext?.Books.SingleOrDefault(b => b.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    _mySQLContext?.Entry(result).CurrentValues.SetValues(book);
                    _mySQLContext?.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return book;

        }

        public Book Delete(int id)
        {
            var book = new Book();

            book = _mySQLContext?.Books.FirstOrDefault(b => b.Id == id);

            if (book is null)
            {
                return new Book();
            }
            else
            {
                _mySQLContext?.Remove(book);
                _mySQLContext?.SaveChanges();
                return book;
            }
        }

        public bool Exists(long id)
        {
            var personInDb = _mySQLContext?.Books.Any(b => b.Id.Equals(id));

            return personInDb.HasValue;
        }
    }
}