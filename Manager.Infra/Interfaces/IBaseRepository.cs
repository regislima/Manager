using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Domain.entities;

namespace Manager.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Remove(long id);
        Task<T> FindById(long id);
        Task<List<T>> FindAll();
    }
}