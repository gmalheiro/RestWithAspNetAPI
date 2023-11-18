using Microsoft.EntityFrameworkCore;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Base;
using RestWithAspNetAPI.Models.Context;

namespace RestWithAspNetAPI.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly MySQLContext _context;

        private DbSet<T> dataset;    

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
               dataset?.Add(item);
               _context?.SaveChanges();
               return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return item;
            }
        }

        public T Delete(int id)
        {
            var itemToBeFound = dataset.FirstOrDefault(x => x.Id == id); 
            
            if (itemToBeFound != null)
            {
                try
                {
                    dataset?.Remove(itemToBeFound);
                    _context?.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return itemToBeFound;
                }
                return itemToBeFound;
            }
            else
            {
                return itemToBeFound;
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(x => x.Id == id);
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindById(long id)
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
