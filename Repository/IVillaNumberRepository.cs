using System.Linq.Expressions;
using MagicVilla_API.Models;

namespace MagicVilla_API.Repository;

public interface IVillaNumberRepository
{
    Task CreateAsync(VillaNumber entity);
    Task<VillaNumber> GetAsync(Expression<Func<VillaNumber, bool>> filter = null, bool tracked = false);
    Task<List<VillaNumber>> GetAllAsync(Expression<Func<VillaNumber, bool>> filter = null, bool tracked = false);
    Task<VillaNumber> UpdateAsync(VillaNumber entity);
    Task RemoveAsync(VillaNumber entity);
    Task SaveAsync();
}