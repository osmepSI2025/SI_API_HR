using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;

namespace SME_API_HR.Repository
{
    public class MEmployeeByIdRepository : IMEmployeeByIdRepository
    {
        private readonly HRAPIDBContext _context;

        public MEmployeeByIdRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MEmployeeById>> GetAllAsync()
        {
            return await _context.MEmployeeByIds.ToListAsync();
        }

        public async Task<MEmployeeById> GetByIdAsync(string id)
        {
            return await _context.MEmployeeByIds.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task AddAsync(MEmployeeById employeeById)
        {
            await _context.MEmployeeByIds.AddAsync(employeeById);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MEmployeeById employeeById)
        {
            _context.MEmployeeByIds.Update(employeeById);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var employeeById = await _context.MEmployeeByIds.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employeeById != null)
            {
                _context.MEmployeeByIds.Remove(employeeById);
                await _context.SaveChangesAsync();
            }
        }
    }
}
