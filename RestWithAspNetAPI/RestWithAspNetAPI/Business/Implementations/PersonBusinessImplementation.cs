using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Data.Converter.Implementations;
using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Hypermedia.Utils;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;

namespace RestWithAspNetAPI.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _personRepository;
        private readonly PersonConverter _converter ;

        public PersonBusinessImplementation(IPersonRepository personRepository,PersonConverter converter)
        {
            _personRepository = personRepository;
            _converter= converter;
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            _personRepository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindAll()
        {
           var people = _converter.Parse(_personRepository.FindAll());

           return people;
        }

        public PersonVO FindById(long id)
        {
           var person = _converter.Parse(_personRepository.FindById(id));
           return person;
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_personRepository.FindByName(firstName, lastName));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            _personRepository.Update(personEntity);
            return person;
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _personRepository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public PersonVO Delete(int id)
        {
            var person = _personRepository.FindById(id);
            _personRepository.Delete(id);
            var personVO = _converter.Parse(person);
            return personVO;
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.first_name like '%{name}%' ";

            var persons = _personRepository.FindWithPagedSearch(query);
            int totalResults = _personRepository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}