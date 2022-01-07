using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Core.Extensions;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly ManagerContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(ManagerContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _unitOfWork.CompleteAsync();

            return obj;
        }

        public virtual async Task<List<T>> FindAll()
        {
            var obj = await _context.Set<T>()
                                .AsNoTracking()
                                .ToListAsync();

            return obj;
        }

        public virtual async Task<T> FindById(long id)
        {
            var obj = await _context.Set<T>()
                                .AsNoTracking()
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();
            
            return obj;
        }

        public virtual async Task Remove(long id)
        {
            var obj = await FindById(id);

            if (!obj.IsNull())
            {
                _context.Remove(obj);
                await _unitOfWork.CompleteAsync();
            }
        }

        public virtual async Task<T> Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _unitOfWork.CompleteAsync();

            return obj;
        }
    }
}