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

        public Person FindById(long id)
        {
            var person = new Person();

            person = _mySQLContext?.Persons.FirstOrDefault(p => p.Id == id);

            if (person is null)
                return new Person();
            else return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _mySQLContext?.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    // set changes and save
                    _mySQLContext?.Entry(result).CurrentValues.SetValues(person);
                    _mySQLContext?.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;

        }

        public Person Delete(int id)
        {
            var person = new Person();

            person = _mySQLContext?.Persons.FirstOrDefault(p => p.Id == id);

            if (person is null)
            {
                return new Person();
            }
            else
            {
                _mySQLContext?.Remove(person);
                _mySQLContext?.SaveChanges();
                return person;
            }
        }

        public bool Exists(long id)
        {
            var personInDb = _mySQLContext?.Persons.Any(p => p.Id.Equals(id));

            return personInDb.HasValue;
        }
    }
}