using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Repository
{
    public class ApiInformationRepository : IApiInformationRepository
    {
        private readonly HRAPIDBContext _context;

        public ApiInformationRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MApiInformation>> GetAllAsync(MapiInformationModels param)
        {
            try
            {
                var query = _context.MApiInformations.AsQueryable();

                if (!string.IsNullOrEmpty(param.ServiceNameCode) && param.ServiceNameCode != "")
                    query = query.Where(p => p.ServiceNameCode == param.ServiceNameCode);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MApiInformation> GetByIdAsync(int id)
            => await _context.MApiInformations.FindAsync(id);

        public async Task AddAsync(MApiInformation service)
        {
            await _context.MApiInformations.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MApiInformation service)
        {
            _context.MApiInformations.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _context.MApiInformations.FindAsync(id);
            if (service != null)
            {
                _context.MApiInformations.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}
