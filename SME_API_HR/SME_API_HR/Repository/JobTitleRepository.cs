using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly HRAPIDBContext _context;

        public JobTitleRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MJobTitle>> GetAllAsync()
        {
            return await _context.MJobTitles.ToListAsync();
        }

        public async Task<MJobTitle> GetByIdAsync(string id)
        {

            //return await _context.MJobTitles.FindAsync(id);
            try
            {
                return await _context.MJobTitles
        .FirstOrDefaultAsync(e => e.Code == id);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task AddAsync(MJobTitle jobTitle)
        {
            await _context.MJobTitles.AddAsync(jobTitle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MJobTitle jobTitle)
        {
            _context.MJobTitles.Update(jobTitle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var jobTitle = await _context.MJobTitles.FindAsync(id);
            if (jobTitle != null)
            {
                _context.MJobTitles.Remove(jobTitle);
                await _context.SaveChangesAsync();
            }
        }

       
        public async Task<IEnumerable<MJobTitle>> SearchAsync(JobTitleModels searchModel)
        {
            try
            {
                var query = _context.MJobTitles.AsQueryable();

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
        public async Task<MJobTitle> GetByJobTitle(string code)
        {
            try
            {
                return await _context.MJobTitles
        .FirstOrDefaultAsync(e => e.Code == code);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
