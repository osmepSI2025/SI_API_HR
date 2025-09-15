using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{

    public class TEmployeeProfileRepository : ITEmployeeProfileRepository
    {
        private readonly HRAPIDBContext _context;

        public TEmployeeProfileRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEmployeeProfile>> GetAllAsync()
        {
            return await _context.TEmployeeProfiles.ToListAsync();
        }

        public async Task<TEmployeeProfile> GetByIdAsync(string empId)
        {
            return await _context.TEmployeeProfiles.FirstOrDefaultAsync(p => p.EmployeeId == empId);
        }

        public async Task AddAsync(TEmployeeProfile profile)
        {
            await _context.TEmployeeProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEmployeeProfile profile)
        {
            _context.TEmployeeProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profile = await _context.TEmployeeProfiles.FindAsync(id);
            if (profile != null)
            {
                _context.TEmployeeProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }
    }

}
