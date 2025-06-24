using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public class MPositionService : IMPositionService
    {
        private readonly IMPositionRepository _positionRepository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;
        public MPositionService(IMPositionRepository positionRepository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _positionRepository = positionRepository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        public async Task<ApiListPositionResponse> GetAllPositions()
        {
            var positions = await _positionRepository.GetAllAsync();

            var response = new ApiListPositionResponse
            {
                Results = positions.Select(p => new PositionResult
                {
                    Code = p.Code,
                    Seq = p.Seq,
                    ProjectCode = p.ProjectCode,
                    TypeCode = p.TypeCode,
                    Module = p.Module,
                    Grouping = p.Grouping,
                    Category = p.Category,
                    NameTh = p.NameTh,
                    NameEn = p.NameEn,
                    DescriptionTh = p.DescriptionTh,
                    DescriptionEn = p.DescriptionEn
                }).ToList()
            };

            return response;
        }

        public async Task<ApiPositionResponse> GetPositionById(string id)
        {
            try
            {
                var position = await _positionRepository.GetByIdAsync(id);

                if (position == null)
                    return null;

                var response = new ApiPositionResponse
                {
                    Results = new PositionResult
                    {
                        Code = position.Code,
                        Seq = position.Seq,
                        ProjectCode = position.ProjectCode,
                        TypeCode = position.TypeCode,
                        Module = position.Module,
                        Grouping = position.Grouping,
                        Category = position.Category,
                        NameTh = position.NameTh,
                        NameEn = position.NameEn,
                        DescriptionTh = position.DescriptionTh,
                        DescriptionEn = position.DescriptionEn
                    }
                };

                return response;
            }
            catch (Exception)
            {
                // Optionally log the exception here
                return null;
            }
        }


        public async Task AddPosition(MPosition position)
        {
            await _positionRepository.AddAsync(position);
        }

        public async Task UpdatePosition(MPosition position)
        {
            await _positionRepository.UpdateAsync(position);
        }

        public async Task DeletePosition(int id)
        {
            await _positionRepository.DeleteAsync(id);
        }
        public async Task BatchEndOfDay()
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "position-all" });

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

                var apiResponse = await _serviceApi.GetDataApiAsync_Position(apiParam);
                if (apiResponse != null)
                {
                    foreach (var item in apiResponse.Results)
                    {
                        var existingBusinessUnit = await _positionRepository.GetByPostion(item.Code);
                        if (existingBusinessUnit != null)
                        {
                            existingBusinessUnit.Code = item.Code;


                            if (!string.IsNullOrEmpty(item.ProjectCode))
                            {
                                existingBusinessUnit.ProjectCode = item.ProjectCode;
                            }

                            if (item.Seq!=0&& item.Seq!=null)
                            {
                                existingBusinessUnit.Seq = item.Seq;
                            }

                            if (!string.IsNullOrEmpty(item.TypeCode))
                            {
                                existingBusinessUnit.TypeCode = item.TypeCode;
                            }

                            if (!string.IsNullOrEmpty(item.Module))
                            {
                                existingBusinessUnit.Module = item.Module;
                            }

                            if (!string.IsNullOrEmpty(item.Grouping))
                            {
                                existingBusinessUnit.Grouping = item.Grouping;
                            }

                            if (!string.IsNullOrEmpty(item.Category))
                            {
                                existingBusinessUnit.Category = item.Category;
                            }

                            if (!string.IsNullOrEmpty(item.NameEn))
                            {
                                existingBusinessUnit.NameEn = item.NameEn;
                            }

                            if (!string.IsNullOrEmpty(item.NameTh))
                            {
                                existingBusinessUnit.NameTh = item.NameTh;
                            }

                            if (!string.IsNullOrEmpty(item.DescriptionEn))
                            {
                                existingBusinessUnit.DescriptionEn = item.DescriptionEn;
                            }

                            if (!string.IsNullOrEmpty(item.DescriptionTh))
                            {
                                existingBusinessUnit.DescriptionTh = item.DescriptionTh;
                            }

                            await _positionRepository.UpdateAsync(existingBusinessUnit);
                        }
                        else
                        {
                            var iposition = new MPosition
                            {
                                Seq = item.Seq,
                                Module = item.Module,
                                NameTh = item.NameTh,
                                 NameEn = item.NameEn,
                                Code = item.Code,
                                DescriptionTh = item.DescriptionTh,
                                DescriptionEn = item.DescriptionEn,
                              
                                ProjectCode = item.ProjectCode,
                                TypeCode = item.TypeCode
                                ,
                                Grouping = item.Grouping,
                                Category = item.Category

                            };
                            await _positionRepository.AddAsync(iposition);
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IEnumerable<MPosition>> SearchPosition(MPositionModels searchModel)
        {
            return await _positionRepository.SearchAsync(searchModel);
        }
    }
}
