using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRAPIDBContext _context;

        public EmployeeRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MEmployee>> GetAllAsync(searchEmployeeModels searchModel)
        {
            try 
            {
                // return await _context.MEmployees.ToListAsync();
                var query = _context.MEmployees.AsQueryable();


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

        public async Task<MEmployee> GetByIdAsync(string EmpId)
        {
            try
            {
                return await _context.MEmployees
        .FirstOrDefaultAsync(e => e.EmployeeId == EmpId);
            }
            catch (Exception ex) 
            {
                return null;
            }
         
        }

        public async Task AddAsync(MEmployee employee)
        {
            await _context.MEmployees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MEmployee employee)
        {
            _context.MEmployees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.MEmployees.FindAsync(id);
            if (employee != null)
            {
                _context.MEmployees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<MEmployee>?> GetEmployeeByOrganization(string? businessUnitId)
        {
            try
            {
                var employees = await _context.MEmployees.ToListAsync();

                if (string.IsNullOrEmpty(businessUnitId))
                    return employees;

                var filteredEmployees = employees
                    .Where(e => !string.IsNullOrEmpty(e.BusinessUnitId) && e.BusinessUnitId.Contains(businessUnitId))
                    .ToList();

                return filteredEmployees;
            }
            catch (Exception ex)
            {
                // คุณอาจ log ex ได้ด้วย
                return null;
            }
        }

        public async Task<IEnumerable<MEmployee>> SearchEmployee(MEmployeeModels searchModel)
        {
            try
            {
                var query = _context.MEmployees.AsQueryable();

                if (!string.IsNullOrEmpty(searchModel.BusinessUnitId))
                {
                    query = query.Where(bu => bu.BusinessUnitId.Contains(searchModel.BusinessUnitId));
                }

                if (!string.IsNullOrEmpty(searchModel.NameTh))
                {
                    query = query.Where(bu => bu.NameTh.Contains(searchModel.NameTh));
                }
                if (!string.IsNullOrEmpty(searchModel.NameEn))
                {
                    query = query.Where(bu => bu.NameEn.Contains(searchModel.NameEn));
                }


                if (!string.IsNullOrEmpty(searchModel.EmployeeCode))
                {
                    query = query.Where(bu => bu.EmployeeCode.Contains(searchModel.EmployeeCode));
                }

                if (!string.IsNullOrEmpty(searchModel.FirstNameEn))
                {
                    query = query.Where(bu => bu.FirstNameEn.Contains(searchModel.FirstNameEn));
                }
                if (!string.IsNullOrEmpty(searchModel.FirstNameTh))
                {
                    query = query.Where(bu => bu.FirstNameTh.Contains(searchModel.FirstNameTh));
                }

                if (!string.IsNullOrEmpty(searchModel.LastNameEn))
                {
                    query = query.Where(bu => bu.LastNameEn.Contains(searchModel.LastNameEn));
                }

                if (!string.IsNullOrEmpty(searchModel.LastNameTh))
                {
                    query = query.Where(bu => bu.LastNameTh.Contains(searchModel.LastNameTh));
                }

                if (!string.IsNullOrEmpty(searchModel.SupervisorId))
                {
                    query = query.Where(bu => bu.SupervisorId ==searchModel.SupervisorId);
                }
                if (!string.IsNullOrEmpty(searchModel.EmployeeId))
                {
                    query = query.Where(bu => bu.EmployeeId == searchModel.EmployeeId);
                }

                if (!string.IsNullOrEmpty(searchModel.PositionId))
                {
                    query = query.Where(bu => bu.PositionId == searchModel.PositionId);
                  
                }

                if (!string.IsNullOrEmpty(searchModel.EmployeeType))
                {
                    query = query.Where(bu => bu.EmployeeType == searchModel.EmployeeType);
                 
                }

                if (!string.IsNullOrEmpty(searchModel.Email))
                {
                    query = query.Where(bu => bu.Email.ToUpper() == searchModel.Email.ToUpper());
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
