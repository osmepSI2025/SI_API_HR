using Microsoft.Extensions.Options;
using SME_API_HR.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SME_API_HR.Services
{
    public class CallAPIService : ICallAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string Api_ErrorLog;
        private readonly string Api_SysCode;
        public CallAPIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            Api_ErrorLog = configuration["Information:Api_ErrorLog"] ?? throw new ArgumentNullException("Api_ErrorLog is missing in appsettings.json");
            Api_SysCode = configuration["Information:Api_SysCode"] ?? throw new ArgumentNullException("Api_SysCode is missing in appsettings.json");

        }
        public async Task<ApiEmployeeResponse> GetDataEmpByEmpId(MapiInformationModels apiModels, string empId)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }
                url = url.Replace("{EMPID}", empId);
                requestJson = url;
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiEmployeeResponse>(content);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
             //   throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
                return new ApiEmployeeResponse();
            }
        }
        public async Task<ApiListEmployeeResponse> GetDataApiAsync_EmpPloyee(MapiInformationModels apiModels, int page,int perPage)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }
                url = url.Replace("{page}", page.ToString()).Replace("{perPage}", perPage.ToString());
                requestJson = url;

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<ApiListEmployeeResponse>(content,options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }
        }

        public async Task<ApiEmployeeProfileResponse> GetDataEmpProfileByEmpId(MapiInformationModels apiModels, string empId)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }
                url = url.Replace("{EMPID}", empId);
                requestJson = url;

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // ไม่สน case ของ field name
                    WriteIndented = true // เวลา serialize จะสวยงามอ่านง่าย
                };
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiEmployeeProfileResponse>(content, options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }
        }

        public async Task<ApiListBusinessUnitResponse> GetDataApiAsync_Organization(MapiInformationModels apiModels)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }
            
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                requestJson = url;

                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // ไม่สน case ของ field name
                    WriteIndented = true // เวลา serialize จะสวยงามอ่านง่าย
                };
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiListBusinessUnitResponse>(content, options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }
        }
        public async Task<ApiListPositionResponse> GetDataApiAsync_Position(MapiInformationModels apiModels)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                requestJson = url;

                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // ไม่สน case ของ field name
                    WriteIndented = true // เวลา serialize จะสวยงามอ่านง่าย
                };
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiListPositionResponse>(content, options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }
        }

        public async Task<ApiListEmployeeMovmentResponse> GetDataEmpMovementByEmpId(MapiInformationModels apiModels, string empId)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }
                url = url.Replace("{EMPID}", empId);
                requestJson = url;

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // ไม่สน case ของ field name
                    WriteIndented = true // เวลา serialize จะสวยงามอ่านง่าย
                };
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiListEmployeeMovmentResponse>(content, options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }
        }
        public async Task<ApiTOrganizationTreeResponse> GetDataApiAsync_OrgTree(MapiInformationModels apiModels)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                requestJson = url;

                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // ไม่สน case ของ field name
                    WriteIndented = true // เวลา serialize จะสวยงามอ่านง่าย
                };
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiTOrganizationTreeResponse>(content, options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }
        }

        public async Task<EmployeeContractApiResponse> GetDataApiAsync_EmployeeContract(MapiInformationModels apiModels,searchEmployeeContractModels Msearch)
        {
            string requestJson = "";

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                var httpClient = new HttpClient(handler);

                var url = $"{apiModels.Urlproduction}";
                if (apiModels.MethodType == "POST")
                {
                    url = apiModels.Urldevelopment;
                }
                else if (apiModels.MethodType == "GET")
                {
                    url = $"{apiModels.Urlproduction}";
                }
                else
                {
                    throw new Exception("Method type not supported");
                }


                requestJson = url.Replace("{DateFrom}", Msearch.employmentDate.Value.Date.ToString("yyyy-MM-dd")).Replace("{DateTo}", Msearch.employmentDate.Value.Date.ToString("yyyy-MM-dd"));

                var request = new HttpRequestMessage(HttpMethod.Get, requestJson);

                if (apiModels.AuthorizationType == "Basic")
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{apiModels.Username}:{apiModels.Password}");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
                else if (apiModels.AuthorizationType == "Bearer")
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiModels.ApiKey);
                }
                else if (apiModels.AuthorizationType == "ApiKey")
                {
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);
                }
                else
                {
                    throw new Exception("Authorization type not supported");
                }
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // ไม่สน case ของ field name
                    WriteIndented = true // เวลา serialize จะสวยงามอ่านง่าย
                };
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EmployeeContractApiResponse>(content, options);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogModels
                {
                    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    ErrorDate = DateTime.Now,
                    UserName = apiModels.Username, // ดึงจาก context หรือ session
                    Path = apiModels.Urlproduction,
                    HttpMethod = apiModels.MethodType,
                    RequestData = requestJson, // serialize เป็น JSON
                    InnerException = ex.InnerException?.ToString(),
                    SystemCode = Api_SysCode,
                    CreatedBy = "system"

                };
                await RecErrorLogApiAsync(apiModels, errorLog);
                throw new Exception("Error in GetData: " + ex.Message + " | Inner Exception: " + ex.InnerException?.Message);
            }

        }


        public async Task RecErrorLogApiAsync(MapiInformationModels apiModels, ErrorLogModels eModels)
        {

            // ✅ เลือก URL ตาม _FlagDev
            string? apiUrl = Api_ErrorLog;



            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

                // ✅ ใส่ API Key ถ้ามี
                if (!string.IsNullOrEmpty(apiModels.ApiKey))
                    request.Headers.Add("X-Api-Key", apiModels.ApiKey);

                // ✅ ใส่ Basic Authentication ถ้ามี Username & Password
                if (!string.IsNullOrEmpty(apiModels.Username) && !string.IsNullOrEmpty(apiModels.Password))
                {
                    var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiModels.Username}:{apiModels.Password}"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
                }

                // ✅ แปลง SendData เป็น JSON และแนบไปกับ Body ของ Request
                var jsonData = JsonSerializer.Serialize(eModels);
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // ✅ Call API และรอผลลัพธ์
                using var response = await _httpClient.SendAsync(request);
                string responseData = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
