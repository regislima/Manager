using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Domain.entities;

namespace Manager.Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindByEmail(string email);
        Task<List<User>> FindByName(string name);
    }
}