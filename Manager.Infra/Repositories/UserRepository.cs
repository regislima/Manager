using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext _context;
        private readonly IUnitOfWork _unitOfWork;
        
        public UserRepository(ManagerContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> FindByEmail(string email)
        {
            var user = await _context.Users.Where(user => user.Email.ToLower().Equals(email.ToLower()))
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<List<User>> FindByName(string name)
        {
            var users = await _context.Users.Where(user => user.Name.ToLower().Contains(name.ToLower()))
                                .AsNoTracking()
                                .ToListAsync();
            
            return users;
        }
    }
}