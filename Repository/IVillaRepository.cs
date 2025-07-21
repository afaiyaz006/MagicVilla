using MagicVilla_API.Models;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null,bool tracked = true);

        Task<Villa> GetAsync(Expression<Func<Villa,bool>> filter = null,bool tracked = true);

        Task CreateAsync(Villa entity);
        Task<Villa> UpdateAsync(Villa entity);
        Task RemoveAsync(Villa entity);
        Task SaveAsync();
    }
}
