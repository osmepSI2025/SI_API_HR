using Microsoft.IdentityModel.Tokens;
using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;

namespace SME_API_HR.Services
{
    public class BusinessUnitService : IBusinessUnitService
    {
        private readonly IMBusinessUnitRepository _businessUnitRepository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;

        public BusinessUnitService(IMBusinessUnitRepository businessUnitRepository,IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _businessUnitRepository = businessUnitRepository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        public async Task<BusinessUnitApiResponse> GetAllBusinessUnits()
        {
            var response = new BusinessUnitApiResponse();
            try
            {
                var data = await _businessUnitRepository.GetAllAsync();

                response.Results = data.Select(bu => new BusinessUnitModel
                {
                    BusinessUnitId = bu.BusinessUnitId,
                    BusinessUnitCode = bu.BusinessUnitCode,
                    BusinessUnitLevel = bu.BusinessUnitLevel ?? 0,
                    ParentId = bu.ParentId,
                    CompanyId = bu.CompanyId,
                    EffectiveDate = bu.EffectiveDate ?? DateTime.MinValue,
                    NameTh = bu.NameTh,
                    NameEn = bu.NameEn,
                    AbbreviationEn = bu.AbbreviationEn,
                    AbbreviationTh = bu.AbbreviationTh,
                    DescriptionTh = bu.DescriptionTh,
                    DescriptionEn = bu.DescriptionEn
                }).ToList();

                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }


        public async Task<BusinessUnitApiResponse> GetBusinessUnitById(string code)
        {
            //return await _businessUnitRepository.GetByIdAsync(code);
            var response = new BusinessUnitApiResponse();
            try
            {
                var data = await _businessUnitRepository.GetByIdAsync(code);

                response.Results = new List<BusinessUnitModel>
{
    new BusinessUnitModel
    {
        BusinessUnitId = data.BusinessUnitId,
        BusinessUnitCode = data.BusinessUnitCode,
        BusinessUnitLevel = data.BusinessUnitLevel ?? 0,
        ParentId = data.ParentId,
        CompanyId = data.CompanyId,
        EffectiveDate = data.EffectiveDate ?? DateTime.MinValue,
        NameTh = data.NameTh,
        NameEn = data.NameEn,
        AbbreviationEn = data.AbbreviationEn,
        AbbreviationTh = data.AbbreviationTh,
        DescriptionTh = data.DescriptionTh,
        DescriptionEn = data.DescriptionEn
    }
};

                return response;
            }
            catch (Exception)
            {
                return response;
            }

        }
        public async Task AddBusinessUnit(MBusinessUnit businessUnit)
        {
            await _businessUnitRepository.AddAsync(businessUnit);
        }

        public async Task UpdateBusinessUnit(MBusinessUnit businessUnit)
        {
            await _businessUnitRepository.UpdateAsync(businessUnit);
        }

        public async Task DeleteBusinessUnit(int id)
        {
            await _businessUnitRepository.DeleteAsync(id);
        }
        public async Task<MBusinessUnit> GetByBu(string Buid)
        {
          return  await _businessUnitRepository.GetByBu(Buid);
        }
      
        public async Task BatchEndOfDay()
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "business-units" });
              
                var apiParam = LApi.Select(x => new MapiInformationModels
                {
                    ServiceNameCode = x.ServiceNameCode,
                    ApiKey = x.ApiKey,
                    AuthorizationType = x.AuthorizationType,
                    ContentType = x.ContentType,
                    CreateDate = x.CreateDate,
                    Id = x.Id,
                    MethodType = x.MethodType,
                    ServiceNameTh = x.ServiceNameTh,
                    Urldevelopment = x.Urldevelopment,
                    Urlproduction = x.Urlproduction,
                    Username = x.Username,
                    Password = x.Password,
                    UpdateDate = x.UpdateDate
                }).First(); // ดึงตัวแรกของ List

                var apiResponse = await _serviceApi.GetDataApiAsync_Organization(apiParam);
                if (apiResponse!=null) 
                {
                    foreach (var item in apiResponse.Results) 
                    {
                        var existingBusinessUnit = await _businessUnitRepository.GetByBu(item.BusinessUnitId);
                        if (existingBusinessUnit != null)
                        {
                            existingBusinessUnit.BusinessUnitId = item.BusinessUnitId;
                            if (!string.IsNullOrEmpty(item.BusinessUnitCode))
                            {
                                existingBusinessUnit.BusinessUnitCode = item.BusinessUnitCode;
                            }
                           
                            if (item.BusinessUnitLevel!=0)
                            {
                                existingBusinessUnit.BusinessUnitLevel = item.BusinessUnitLevel;
                            }
                           
                            if (!string.IsNullOrEmpty(item.ParentId))
                            {
                                existingBusinessUnit.ParentId = item.ParentId;
                            }
                  
                            if (!string.IsNullOrEmpty(item.CompanyId))
                            {
                                existingBusinessUnit.CompanyId = item.CompanyId;
                            }
                           
                            if (item.EffectiveDate!=null)
                            {
                                existingBusinessUnit.EffectiveDate = item.EffectiveDate;
                            }
                          
                            if (!string.IsNullOrEmpty(item.NameEn))
                            {
                                existingBusinessUnit.NameEn = item.NameEn;
                            }
                        
                            if (!string.IsNullOrEmpty(item.NameTh))
                            {
                                existingBusinessUnit.NameTh = item.NameTh;
                            }
                           
                            if (!string.IsNullOrEmpty(item.AbbreviationEn))
                            {
                                existingBusinessUnit.AbbreviationEn = item.AbbreviationEn;
                            }
               
                            if (!string.IsNullOrEmpty(item.AbbreviationTh))
                            {
                                existingBusinessUnit.AbbreviationTh = item.AbbreviationTh;
                            }
                          
                            if (!string.IsNullOrEmpty(item.DescriptionEn))
                            {
                                existingBusinessUnit.DescriptionEn = item.DescriptionEn;
                            }
                    
                            if (!string.IsNullOrEmpty(item.DescriptionTh))
                            {
                                existingBusinessUnit.DescriptionTh = item.DescriptionTh;
                            }
                          
                            await _businessUnitRepository.UpdateAsync(existingBusinessUnit);
                        }
                        else
                        {
                            var businessUnit = new MBusinessUnit
                            {
                                BusinessUnitId = item.BusinessUnitId,
                                BusinessUnitCode = item.BusinessUnitCode,
                                BusinessUnitLevel = item.BusinessUnitLevel,
                                ParentId = item.ParentId,
                                CompanyId = item.CompanyId,
                                EffectiveDate = item.EffectiveDate,
                                NameEn = item.NameEn,
                                NameTh = item.NameTh,
                                AbbreviationEn = item.AbbreviationEn,
                                AbbreviationTh = item.AbbreviationTh,
                                DescriptionEn = item.DescriptionEn,
                                DescriptionTh = item.DescriptionTh

                            };
                            await _businessUnitRepository.AddAsync(businessUnit);
                        }
                    
                    }
                    
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        public async Task<IEnumerable<MBusinessUnit>> SearchBusinessUnits(MBusinessUnitModels searchModel)
        {
            return await _businessUnitRepository.SearchAsync(searchModel);
        }

    }
}
