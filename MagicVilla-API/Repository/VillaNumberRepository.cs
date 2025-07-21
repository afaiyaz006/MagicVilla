using MagicVilla_API.Data;
using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
    public class VillaNumberRepository : IVillaNumberRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaNumberRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(VillaNumber entity)
        {
            await _db.VillaNumbers.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<VillaNumber> GetAsync(Expression<Func<VillaNumber, bool>> filter = null, bool tracked = false)
        {
            IQueryable<VillaNumber> query = _db.VillaNumbers;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<VillaNumber>> GetAllAsync(Expression<Func<VillaNumber, bool>> filter = null, bool tracked = false)
        {
            IQueryable<VillaNumber> query = _db.VillaNumbers;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(entity);
            await SaveAsync();
            return entity;
        }

        public async Task RemoveAsync(VillaNumber entity)
        {
            _db.VillaNumbers.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}