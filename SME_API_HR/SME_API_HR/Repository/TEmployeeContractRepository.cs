using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Repository
{
    public class TEmployeeContractRepository : ITEmployeeContractRepository
    {
        private readonly HRAPIDBContext _context;

        public TEmployeeContractRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEmployeeContract>> GetAllAsync()
        {
            return await _context.TEmployeeContracts.ToListAsync();
        }

        public async Task<TEmployeeContract?> GetContractById(string employeeid, DateTime? xdate)
        {
            return await _context.TEmployeeContracts.FirstOrDefaultAsync(e=>e.EmployeeId == employeeid
            && e.EmploymentDate.Value.Date == xdate.Value.Date
            );
        }

        public async Task AddAsync(TEmployeeContract contract)
        {
            await _context.TEmployeeContracts.AddAsync(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEmployeeContract contract)
        {
            _context.TEmployeeContracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contract = await _context.TEmployeeContracts.FindAsync(id);
            if (contract != null)
            {
                _context.TEmployeeContracts.Remove(contract);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<TEmployeeContract>> SearchEmployeeContract(searchEmployeeContractModels searchModel)
        {
            try
            {
                var query = _context.TEmployeeContracts
                  
                    .AsQueryable();

              

          
                if (!string.IsNullOrEmpty(searchModel.EmployeeId))
                {
                    query = query.Where(bu => bu.EmployeeId == searchModel.EmployeeId);
                }
                if (searchModel.employmentDate!=null)
                {
                    query = query.Where(bu => bu.EmploymentDate == searchModel.employmentDate);
                }

                // Apply pagination
                if (searchModel.page != 0 && searchModel.perPage != 0)
                {
                    int skip = (searchModel.page - 1) * searchModel.perPage;
                    query = query.Skip(skip).Take(searchModel.perPage);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
