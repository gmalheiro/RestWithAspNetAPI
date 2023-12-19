using RestWithAspNetAPI.Data.VO;
using RestWithAspNetAPI.Models;

namespace RestWithAspNetAPI.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindByName(string firstName, string lastName);

        List<PersonVO> FindAll();

        PersonVO Update(PersonVO person);

        PersonVO Disable(long id);

        PersonVO Delete(int id);    
    }
}
