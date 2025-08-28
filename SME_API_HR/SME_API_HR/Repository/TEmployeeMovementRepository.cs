using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public class TEmployeeMovementRepository : ITEmployeeMovementRepository
    {
        private readonly HRAPIDBContext _context;

        public TEmployeeMovementRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEmployeeMovement>> GetAllAsync()
        {
            return await _context.TEmployeeMovements.ToListAsync();
        }

        public async Task<TEmployeeMovement> GetByIdAsync(string employeeId)
        {
            return await _context.TEmployeeMovements.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
        }

        public async Task AddAsync(TEmployeeMovement movement)
        {
            await _context.TEmployeeMovements.AddAsync(movement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEmployeeMovement movement)
        {
            _context.TEmployeeMovements.Update(movement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string employeeId)
        {
            var movement = await _context.TEmployeeMovements.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
            if (movement != null)
            {
                _context.TEmployeeMovements.Remove(movement);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<TEmployeeMovement>> SearchAsync(TEmployeeMovementModels searchModel)
        {
            var query = _context.TEmployeeMovements.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.EmployeeId))
            {
                query = query.Where(m => m.EmployeeId == searchModel.EmployeeId);
            }

            if (!string.IsNullOrEmpty(searchModel.MovementTypeId))
            {
                query = query.Where(m => m.MovementTypeId == searchModel.MovementTypeId);
            }

            if (!string.IsNullOrEmpty(searchModel.EmployeeCode))
            {
                query = query.Where(m => m.EmployeeCode == searchModel.EmployeeCode);
            }

            if (!string.IsNullOrEmpty(searchModel.EmployeeStatus))
            {
                query = query.Where(m => m.EmployeeStatus == searchModel.EmployeeStatus);
            }

            if (!string.IsNullOrEmpty(searchModel.EmployeeTypeId))
            {
                query = query.Where(m => m.EmployeeTypeId == searchModel.EmployeeTypeId);
            }

            if (!string.IsNullOrEmpty(searchModel.CompanyId))
            {
                query = query.Where(m => m.CompanyId == searchModel.CompanyId);
            }

            if (!string.IsNullOrEmpty(searchModel.BusinessUnitId))
            {
                query = query.Where(m => m.BusinessUnitId == searchModel.BusinessUnitId);
            }

            if (!string.IsNullOrEmpty(searchModel.PositionId))
            {
                query = query.Where(m => m.PositionId == searchModel.PositionId);
            }

            if (searchModel.EffectiveDate.HasValue)
            {
                query = query.Where(m => m.EffectiveDate == searchModel.EffectiveDate);
            }

            return await query.ToListAsync();
        }
 
        public async Task<TEmployeeMovement> GetTEmployeeMovementsById(string id)
        {
            try
            {
                return await _context.TEmployeeMovements
        .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
