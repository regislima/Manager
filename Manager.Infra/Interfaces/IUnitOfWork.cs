using System.Threading.Tasks;

namespace Manager.Infra.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}