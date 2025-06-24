using SME_API_HR.Entities;
using SME_API_HR.Models;
using SME_API_HR.Repository;

namespace SME_API_HR.Services
{
    public class TEmployeeProfileService : ITEmployeeProfileService
    {
        private readonly ITEmployeeProfileRepository _profileRepository;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly ICallAPIService _serviceApi;

        public TEmployeeProfileService(
            ITEmployeeProfileRepository profileRepository,
            IApiInformationRepository repositoryApi,
            ICallAPIService serviceApi)
        {
            _profileRepository = profileRepository;
            _repositoryApi = repositoryApi;
            _serviceApi = serviceApi;
        }

        public async Task<IEnumerable<TEmployeeProfile>> GetAllProfiles()
        {
            return await _profileRepository.GetAllAsync();
        }

        public async Task<ApiEmployeeProfileResponse> GetProfileById(string EmpId)
        {
            try
            {
                var employee = await GetSearchProfileById(EmpId);
                if (employee == null)
                {
                    // Call API to get employee details
                    var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "employee-profile" });


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
                    if (apiParam == null)
                    {
                        return null;
                    }


                    var apiResponse = await _serviceApi.GetDataEmpProfileByEmpId(apiParam, EmpId);
                    if (apiResponse.Results == null)
                    {
                        return null;
                    }

                    // Map API response to TEmployeeProfile
                    var mEmployee = new TEmployeeProfile
                    {
                        EmployeeId = EmpId,
                        InternalPhone = apiResponse.Results.InternalPhone,
                        MailingSubdistrict = apiResponse.Results.MailingSubdistrict,
                        MailingDistrict = apiResponse.Results.MailingDistrict,
                        MailingProvince = apiResponse.Results.MailingProvince,
                        MailingPostCode = apiResponse.Results.MailingPostCode,
                        MailingCountry = apiResponse.Results.MailingCountry,
                        MailingAddrEn = apiResponse.Results.MailingAddrEn,
                        MailingAddrTh = apiResponse.Results.MailingAddrTh,
                        MailingPhoneNo = apiResponse.Results.MailingPhoneNo,
                        RegisAddrEn = apiResponse.Results.RegisAddrEn,
                        RegisAddrTh = apiResponse.Results.RegisAddrTh,
                        RegisSubdistrict = apiResponse.Results.RegisSubdistrict,
                        RegisDistrict = apiResponse.Results.RegisDistrict,
                        RegisProvince = apiResponse.Results.RegisProvince,
                        RegisPostCode = apiResponse.Results.RegisPostCode,
                        RegisCountry = apiResponse.Results.RegisCountry,
                        RegisPhoneNo = apiResponse.Results.RegisPhoneNo,
                        BloodGroup = apiResponse.Results.BloodGroup,
                        JobDetails = apiResponse.Results.JobDetails,
                        MilitaryStatus = apiResponse.Results.MilitaryStatus,
                        Nationality = apiResponse.Results.Nationality,
                        Race = apiResponse.Results.Race,
                        Religion = apiResponse.Results.Religion,
                        NickName = apiResponse.Results.NickName
                    };

                    // Add the profile to the repository
                    await AddProfile(mEmployee);

                    // Return the newly added profile
                    return  await GetSearchProfileById(EmpId);
                }

                return employee;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework like Serilog or NLog)
                Console.WriteLine($"Error in GetProfileById: {ex.Message}");
                throw;
            }
        }

        public async Task AddProfile(TEmployeeProfile profile)
        {
            await _profileRepository.AddAsync(profile);
        }

        public async Task UpdateProfile(TEmployeeProfile profile)
        {
            await _profileRepository.UpdateAsync(profile);
        }

        public async Task DeleteProfile(int id)
        {
            await _profileRepository.DeleteAsync(id);
        }
        public async Task<ApiEmployeeProfileResponse> GetSearchProfileById(string EmpId)
        {
            try
            {
                var employee = await _profileRepository.GetByIdAsync(EmpId);
                if (employee == null)
                    return null;

                var response = new ApiEmployeeProfileResponse
                {
                    Results = new EmployeeProfileResult
                    {
                        InternalPhone = employee.InternalPhone,
                        MilitaryStatus = employee.MilitaryStatus,
                        MailingAddrTh = employee.MailingAddrTh,
                        MailingAddrEn = employee.MailingAddrEn,
                        MailingSubdistrict = employee.MailingSubdistrict,
                        MailingDistrict = employee.MailingDistrict,
                        MailingProvince = employee.MailingProvince,
                        MailingCountry = employee.MailingCountry,
                        MailingPostCode = employee.MailingPostCode,
                        MailingPhoneNo = employee.MailingPhoneNo,
                        RegisAddrTh = employee.RegisAddrTh,
                        RegisAddrEn = employee.RegisAddrEn,
                        RegisSubdistrict = employee.RegisSubdistrict,
                        RegisDistrict = employee.RegisDistrict,
                        RegisProvince = employee.RegisProvince,
                        RegisCountry = employee.RegisCountry,
                        RegisPostCode = employee.RegisPostCode,
                        RegisPhoneNo = employee.RegisPhoneNo,
                        BloodGroup = employee.BloodGroup,
                        Religion = employee.Religion,
                        Race = employee.Race,
                        Nationality = employee.Nationality,
                        JobDetails = employee.JobDetails,
                        NickName = employee.NickName
                    }
                };

                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
