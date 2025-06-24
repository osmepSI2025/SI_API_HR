using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public class MJobLevelService : IMJobLevelService
    {
        private readonly IMJobLevelRepository _jobLevelRepository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;
        public MJobLevelService(IMJobLevelRepository jobLevelRepository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _jobLevelRepository = jobLevelRepository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        //public async Task<IEnumerable<ApiListJobLevelResponse>> GetAllJobLevels()
        //{
        //    return await _jobLevelRepository.GetAllAsync();
        //}
        public async Task<ApiListJobLevelResponse> GetAllJobLevels()
        {
            try {
                var response = new ApiListJobLevelResponse();
                var jobLevels = await _jobLevelRepository.GetAllAsync();
                response.Results = jobLevels.Select(j => new JobLevelResult
                {
                    Seq = j.Seq,
                    ProjectCode = j.ProjectCode,
                    Code = j.Code,
                    TypeCode = j.TypeCode,
                    Module = j.Module,
                    Grouping = j.Grouping,
                    Category = j.Category,
                    NameTh = j.NameTh,
                    NameEn = j.NameEn,
                    DescriptionTh = j.DescriptionTh,
                    DescriptionEn = j.DescriptionEn
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return null;
            }
         
            
        }

        public async Task<ApiJobLevelResponse> GetJobLevelById(string id)
        {
            try
            {
                var jobLevel = await _jobLevelRepository.GetByIdAsync(id);

                if (jobLevel == null)
                    return null;

                var response = new ApiJobLevelResponse
                {
                    Results = new JobLevelResult
                    {
                        Seq = jobLevel.Seq,
                        ProjectCode = jobLevel.ProjectCode,
                        Code = jobLevel.Code,
                        TypeCode = jobLevel.TypeCode,
                        Module = jobLevel.Module,
                        Grouping = jobLevel.Grouping,
                        Category = jobLevel.Category,
                        NameTh = jobLevel.NameTh,
                        NameEn = jobLevel.NameEn,
                        DescriptionTh = jobLevel.DescriptionTh,
                        DescriptionEn = jobLevel.DescriptionEn
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


        public async Task AddJobLevel(MJobLevel jobLevel)
        {
            await _jobLevelRepository.AddAsync(jobLevel);
        }

        public async Task UpdateJobLevel(MJobLevel jobLevel)
        {
            await _jobLevelRepository.UpdateAsync(jobLevel);
        }

        public async Task DeleteJobLevel(int id)
        {
            await _jobLevelRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<MJobLevel>> SearchJobLevel(MJobLevelModels searchModel)
        {
            return await _jobLevelRepository.SearchAsync(searchModel);
        }
        public async Task BatchEndOfDay()
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "job-level" });

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
                        var existingjobTitle = await _jobLevelRepository.GetByJobLevel(item.Code);
                        if (existingjobTitle != null)
                        {
                            existingjobTitle.Code = item.Code;


                            if (!string.IsNullOrEmpty(item.ProjectCode))
                            {
                                existingjobTitle.ProjectCode = item.ProjectCode;
                            }

                            if (item.Seq != 0 && item.Seq != null)
                            {
                                existingjobTitle.Seq = item.Seq;
                            }

                            if (!string.IsNullOrEmpty(item.TypeCode))
                            {
                                existingjobTitle.TypeCode = item.TypeCode;
                            }

                            if (!string.IsNullOrEmpty(item.Module))
                            {
                                existingjobTitle.Module = item.Module;
                            }

                            if (!string.IsNullOrEmpty(item.Grouping))
                            {
                                existingjobTitle.Grouping = item.Grouping;
                            }

                            if (!string.IsNullOrEmpty(item.Category))
                            {
                                existingjobTitle.Category = item.Category;
                            }

                            if (!string.IsNullOrEmpty(item.NameEn))
                            {
                                existingjobTitle.NameEn = item.NameEn;
                            }

                            if (!string.IsNullOrEmpty(item.NameTh))
                            {
                                existingjobTitle.NameTh = item.NameTh;
                            }

                            if (!string.IsNullOrEmpty(item.DescriptionEn))
                            {
                                existingjobTitle.DescriptionEn = item.DescriptionEn;
                            }

                            if (!string.IsNullOrEmpty(item.DescriptionTh))
                            {
                                existingjobTitle.DescriptionTh = item.DescriptionTh;
                            }

                            await _jobLevelRepository.UpdateAsync(existingjobTitle);
                        }
                        else
                        {
                            var ijob = new MJobLevel
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
                            await _jobLevelRepository.AddAsync(ijob);
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
