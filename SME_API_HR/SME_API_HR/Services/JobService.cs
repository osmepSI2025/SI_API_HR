using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME_API_HR.Services
{
    public class JobService : IJobService
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;
        public JobService(IJobTitleRepository jobTitleRepository, IApiInformationRepository repositoryApi, ICallAPIService serviceApi)
        {
            _jobTitleRepository = jobTitleRepository;
            _repositoryApi = repositoryApi;
             _serviceApi = serviceApi;
        }

        public async Task<ApiListJobTitleResponse> GetAllJobTitles()
        {
            var jobTitles = await _jobTitleRepository.GetAllAsync();

            var response = new ApiListJobTitleResponse
            {
                Results = jobTitles.Select(j => new JobTitleResult
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
                }).ToList()
            };

            return response;
        }


        public async Task<ApiJobTitleResponse> GetJobTitleById(string code)
        {
            var jobTitle = await _jobTitleRepository.GetByIdAsync(code);

            if (jobTitle == null)
                return null;

            var response = new ApiJobTitleResponse
            {
                Results = new JobTitleResult
                {
                    Seq = jobTitle.Seq,
                    ProjectCode = jobTitle.ProjectCode,
                    Code = jobTitle.Code,
                    TypeCode = jobTitle.TypeCode,
                    Module = jobTitle.Module,
                    Grouping = jobTitle.Grouping,
                    Category = jobTitle.Category,
                    NameTh = jobTitle.NameTh,
                    NameEn = jobTitle.NameEn,
                    DescriptionTh = jobTitle.DescriptionTh,
                    DescriptionEn = jobTitle.DescriptionEn
                }
            };

            return response;
        }


        public async Task AddJobTitle(MJobTitle jobTitle)
        {
            await _jobTitleRepository.AddAsync(jobTitle);
        }

        public async Task UpdateJobTitle(MJobTitle jobTitle)
        {
            await _jobTitleRepository.UpdateAsync(jobTitle);
        }

        public async Task DeleteJobTitle(int id)
        {
            await _jobTitleRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<MJobTitle>> SearchJobTitles(JobTitleModels searchModel)
        {
            return await _jobTitleRepository.SearchAsync(searchModel);
        }
        public async Task BatchEndOfDay()
        {
            try
            {
                var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "job-titles" });

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
                        var existingjobTitle = await _jobTitleRepository.GetByJobTitle(item.Code);
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

                            await _jobTitleRepository.UpdateAsync(existingjobTitle);
                        }
                        else
                        {
                            var ijob = new MJobTitle
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
                            await _jobTitleRepository.AddAsync(ijob);
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