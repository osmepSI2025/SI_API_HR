using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public class MPositionRepository : IMPositionRepository
    {
        private readonly HRAPIDBContext _context;

        public MPositionRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPosition>> GetAllAsync()
        {
            return await _context.MPositions.ToListAsync();
        }

        public async Task<MPosition> GetByIdAsync(string code)
        {
            try
            {
                return await _context.MPositions
        .FirstOrDefaultAsync(e => e.Code == code);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task AddAsync(MPosition position)
        {
            await _context.MPositions.AddAsync(position);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MPosition position)
        {
            _context.MPositions.Update(position);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var position = await _context.MPositions.FindAsync(id);
            if (position != null)
            {
                _context.MPositions.Remove(position);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<MPosition>> SearchAsync(MPositionModels searchModel)
        {
            try
            {
                var query = _context.MPositions.AsQueryable();

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
        public async Task<MPosition> GetByPostion(string code)
        {
            try
            {
                return await _context.MPositions
        .FirstOrDefaultAsync(e => e.Code == code);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
