using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Hypermedia.Utils;
using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindByName(string firstName, string lastName);

        List<PersonVO> FindAll();

        PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page);

        PersonVO Update(PersonVO person);

        PersonVO Disable(long id);

        PersonVO Delete(int id);    
    }
}
