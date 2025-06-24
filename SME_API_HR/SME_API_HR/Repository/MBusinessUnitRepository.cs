using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Repository
{
    public class MBusinessUnitRepository : IMBusinessUnitRepository
    {
        private readonly HRAPIDBContext _context;

        public MBusinessUnitRepository(HRAPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MBusinessUnit>> GetAllAsync()
        {
            return await _context.MBusinessUnits.ToListAsync();
        }

        public async Task<MBusinessUnit> GetByIdAsync(string id)
        {
            try
            {
                return await _context.MBusinessUnits
        .FirstOrDefaultAsync(e => e.BusinessUnitId == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         public async Task<MBusinessUnit> GetByBu(string BuId)
        {
            try
            {
                return await _context.MBusinessUnits
        .FirstOrDefaultAsync(e => e.BusinessUnitId == BuId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task AddAsync(MBusinessUnit businessUnit)
        {
            await _context.MBusinessUnits.AddAsync(businessUnit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MBusinessUnit businessUnit)
        {
            _context.MBusinessUnits.Update(businessUnit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var businessUnit = await _context.MBusinessUnits.FindAsync(id);
            if (businessUnit != null)
            {
                _context.MBusinessUnits.Remove(businessUnit);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<MBusinessUnit>> SearchAsync(MBusinessUnitModels searchModel)
        {
            try
            {
                var query = _context.MBusinessUnits.AsQueryable();

                if (!string.IsNullOrEmpty(searchModel.BusinessUnitId))
                {
                    query = query.Where(bu => bu.BusinessUnitId==searchModel.BusinessUnitId);
                }

                if (!string.IsNullOrEmpty(searchModel.BusinessUnitCode))
                {
                    query = query.Where(bu => bu.BusinessUnitCode.Contains(searchModel.BusinessUnitCode));
                }

                if (searchModel.BusinessUnitLevel.HasValue)
                {
                    query = query.Where(bu => bu.BusinessUnitLevel == searchModel.BusinessUnitLevel);
                }

                if (!string.IsNullOrEmpty(searchModel.ParentId))
                {
                    query = query.Where(bu => bu.ParentId.Contains(searchModel.ParentId));
                }

                if (!string.IsNullOrEmpty(searchModel.CompanyId))
                {
                    query = query.Where(bu => bu.CompanyId.Contains(searchModel.CompanyId));
                }

                if (!string.IsNullOrEmpty(searchModel.NameTh))
                {
                    query = query.Where(bu => bu.NameTh.Contains(searchModel.NameTh));
                }

                if (!string.IsNullOrEmpty(searchModel.NameEn))
                {
                    query = query.Where(bu => bu.NameEn.Contains(searchModel.NameEn));
                }

                if (!string.IsNullOrEmpty(searchModel.AbbreviationTh))
                {
                    query = query.Where(bu => bu.AbbreviationTh.Contains(searchModel.AbbreviationTh));
                }

                if (!string.IsNullOrEmpty(searchModel.AbbreviationEn))
                {
                    query = query.Where(bu => bu.AbbreviationEn.Contains(searchModel.AbbreviationEn));
                }

                if (!string.IsNullOrEmpty(searchModel.DescriptionTh))
                {
                    query = query.Where(bu => bu.DescriptionTh.Contains(searchModel.DescriptionTh));
                }

                if (!string.IsNullOrEmpty(searchModel.DescriptionEn))
                {
                    query = query.Where(bu => bu.DescriptionEn.Contains(searchModel.DescriptionEn));
                }

                return await query.ToListAsync();
            } catch (Exception ex) {
                return null;
            }
         
        }

    }
}
