using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public class MJobLevelRepository : IMJobLevelRepository
    {
        private readonly HRAPIDBContext _context;

        public MJobLevelRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MJobLevel>> GetAllAsync()
        {
            return await _context.MJobLevels.ToListAsync();
        }

        public async Task<MJobLevel> GetByIdAsync(string id)
        {
         //   return await _context.MJobLevels.FindAsync(id);
            try
            {
                return await _context.MJobLevels
        .FirstOrDefaultAsync(e => e.Code == id);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task AddAsync(MJobLevel jobLevel)
        {
            await _context.MJobLevels.AddAsync(jobLevel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MJobLevel jobLevel)
        {
            _context.MJobLevels.Update(jobLevel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var jobLevel = await _context.MJobLevels.FindAsync(id);
            if (jobLevel != null)
            {
                _context.MJobLevels.Remove(jobLevel);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<MJobLevel>> SearchAsync(MJobLevelModels searchModel)
        {
            try
            {
                var query = _context.MJobLevels.AsQueryable();

                if (!string.IsNullOrEmpty(searchModel.ProjectCode))
                {
                    query = query.Where(bu => bu.ProjectCode == searchModel.ProjectCode);
                }

                if (!string.IsNullOrEmpty(searchModel.Code))
                {
                    query = query.Where(bu => bu.Code == searchModel.Code);
                }



                if (!string.IsNullOrEmpty(searchModel.NameEn))
                {
                    query = query.Where(bu => bu.NameEn.Contains(searchModel.NameEn));
                }

                if (!string.IsNullOrEmpty(searchModel.NameTh))
                {
                    query = query.Where(bu => bu.NameTh.Contains(searchModel.NameTh));
                }

                if (!string.IsNullOrEmpty(searchModel.NameTh))
                {
                    query = query.Where(bu => bu.NameTh.Contains(searchModel.NameTh));
                }



                if (!string.IsNullOrEmpty(searchModel.Module))
                {
                    query = query.Where(bu => bu.Module == searchModel.Module);

                }


                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<MJobLevel> GetByJobLevel(string code)
        {
            try
            {
                return await _context.MJobLevels
        .FirstOrDefaultAsync(e => e.Code == code);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
