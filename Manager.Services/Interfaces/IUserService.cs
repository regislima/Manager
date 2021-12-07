using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Services.DTO;

namespace Manager.Services.Interfaces
{
    public interface IUserService : IBaseService<UserDTO>
    {
        Task<UserDTO> FindByEmail(string email);
        Task<List<UserDTO>> FindByName(string name);
    }
}