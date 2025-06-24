using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;

namespace SME_API_HR.Repository
{
    public class TOrganizationTreeRepository : ITOrganizationTreeRepository
    {
        private readonly HRAPIDBContext _context;

        public TOrganizationTreeRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TOrganizationTree>> GetAllAsync()
        {
            try
            {
                return await _context.TOrganizationTrees.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception("Error fetching all organization trees", ex);
            }
        }

        public async Task<TOrganizationTree> GetByIdAsync(string id)
        {
            try
            {
                return await _context.TOrganizationTrees
    .FirstOrDefaultAsync(e => e.BusinessUnitId == id);

            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception($"Error fetching organization tree with ID {id}", ex);
            }
        }

        public async Task AddAsync(TOrganizationTree organizationTree)
        {
            try
            {
                await _context.TOrganizationTrees.AddAsync(organizationTree);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception("Error adding organization tree", ex);
            }
        }

        public async Task UpdateAsync(TOrganizationTree organizationTree)
        {
            try
            {
                _context.TOrganizationTrees.Update(organizationTree);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception("Error updating organization tree", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var organizationTree = await _context.TOrganizationTrees.FindAsync(id);
                if (organizationTree != null)
                {
                    _context.TOrganizationTrees.Remove(organizationTree);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception($"Error deleting organization tree with ID {id}", ex);
            }
        }
        public async Task DeleteAllAsync()
        {
            try
            {
                // Remove all records from the T_Organization_Tree table
                _context.TOrganizationTrees.RemoveRange(_context.TOrganizationTrees);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception("Error deleting all records from T_Organization_Tree", ex);
            }
        }
        public async Task SaveBusinessUnitsAsync(ApiTOrganizationTreeResponse units)
        {
            try
            {
                foreach (var unit in units.Results)
                {
                    var organizationTree = new TOrganizationTree
                    {
                        BusinessUnitId = unit.BusinessUnitId,
                      
                        BusinessUnitNameTh = unit.BusinessUnitNameTh,
                        BusinessUnitNameEn = unit.BusinessUnitNameEn,
                        IsActive = true,
                        CreateDate = DateTime.Now
                    };

                    await _context.TOrganizationTrees.AddAsync(organizationTree);
                    await _context.SaveChangesAsync();

                    // Recursively save children
                    if (unit.Children != null && unit.Children.Any())
                    {
                        await SaveSunBusinessUnitsAsync(unit.Children, unit.BusinessUnitId);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception("Error saving business units", ex);
            }
        }
      
        public async Task SaveSunBusinessUnitsAsync(List<BusinessUnit> units, string? parentId = null)
        {
            try
            {
                foreach (var unit in units)
                {
                    // Create a new TOrganizationTree entity
                    var organizationTree = new TOrganizationTree
                    {
                        BusinessUnitId = unit.BusinessUnitId,
                        ParentBusinessUnitId = parentId,
                        BusinessUnitNameTh = unit.BusinessUnitNameTh,
                        BusinessUnitNameEn = unit.BusinessUnitNameEn,
                        IsActive = true,
                        CreateDate = DateTime.Now
                    };

                    // Add the entity to the database
                    await _context.TOrganizationTrees.AddAsync(organizationTree);
                    await _context.SaveChangesAsync();

                    // Recursively save children
                    if (unit.Children != null && unit.Children.Any())
                    {
                        await SaveSunBusinessUnitsAsync(unit.Children, unit.BusinessUnitId);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error saving business units", ex);
            }
        }
    }
}
