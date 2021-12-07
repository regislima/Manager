using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> Create(T objDTO);
        Task<T> Update(T objDTO);
        Task Remove(long id);
        Task<T> FindById(long id);
        Task<List<T>> FindAll();
    }
}