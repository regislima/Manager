using System.Threading.Tasks;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;

namespace Manager.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManagerContext _context;

        public UnitOfWork(ManagerContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}