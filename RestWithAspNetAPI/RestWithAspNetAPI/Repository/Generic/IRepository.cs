using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Base;

namespace RestWithAspNetAPI.Repository
{
    public interface IRepository <T> where T : BaseEntity
    {
        T Create(T item);

        T FindById(long id);

        List<T> FindAll();

        T Update(T item);

        T Delete(int id);

        bool Exists(long id);

        List<T> FindWithPagedSearch(string query);
        
        int GetCount(string query);
    }
}
