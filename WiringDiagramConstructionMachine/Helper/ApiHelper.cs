using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Configuration;
using WiringDiagramConstructionMachine.IServices;
using WiringDiagramConstructionMachine.Settings;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Helper
{
    public class ApiHelper
    {
        public async static Task<ApiResponse> Login(string Phone, string Password,string Serial)
        {
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                return new ApiResponse(false, null, "No Internnet");
            }
            ApiResponse loginResponse = await ApiHelper.Post(ApiRouter.LOGIN, new
            {
                Phone = Phone,
                Password = Password
            });
            return loginResponse;
        }

        public async static Task<ApiResponse> Get<T>(string Path, bool Authenticate = false, bool RefreshToken = true) where T : class
        {
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                return new ApiResponse(false, null, "No Internnet");
            }

            try
            {
                var client = BsdHttpClient.Instance();
                ApiResponse res = new ApiResponse();
                if (Authenticate)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserLogged.AccessToken);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(Path);

                if (response.IsSuccessStatusCode)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    res.IsSuccess = true;
                    res.Content = JsonConvert.DeserializeObject<T>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    res.IsSuccess = true;
                    var body = await response.Content.ReadAsStringAsync();
                    res.Content = JsonConvert.DeserializeObject<T>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    if (response.Headers.Contains("Token-Expired"))
                    {

                    }
                    if (UserLogged.IsLogged && Authenticate && RefreshToken)
                    {
                        string serial = await DependencyService.Get<IGetSerialDeviceService>().GetSerial();
                        ApiResponse loginResponse;
                        loginResponse = await Login(UserLogged.Phone, UserLogged.Password, serial);

                        if (loginResponse.IsSuccess)
                        {
                            AuthenticateReponse authResponse = JsonConvert.DeserializeObject<AuthenticateReponse>(loginResponse.Content.ToString());
                            //await UserLogged.SaveLogin(authResponse);
                            res = await Get<T>(Path, true, false);
                        }
                        else
                        {
                            res.Message = "Loi he thong vui long thu laij";
                            res.IsSuccess = false;
                        }
                    }
                    else
                    {
                        // loi dang nhap, co token user pass nhung ko dang nhap duoc.
                        res.Message = "Loi he thong vui long thu lai";
                        res.IsSuccess = false;
                    }
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    res.Message = body;
                    res.IsSuccess = false;
                }
                return res;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async static Task<ApiResponse> Post(string Path, object formContent, bool Authenticate = false, bool RefreshToken = true)
        {
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                return new ApiResponse(false, null, "No Internnet");
            }
            try
            {
                var client = BsdHttpClient.Instance();
                ApiResponse res = new ApiResponse();
                if (Authenticate)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserLogged.AccessToken);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                if (formContent == null)
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, Path);
                    response = await client.SendAsync(request);
                }
                else
                {
                    string objContent = JsonConvert.SerializeObject(formContent);
                    HttpContent content = new StringContent(objContent, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(Path, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    if (UserLogged.IsLogged && Authenticate && RefreshToken)
                    {
                        string serial = await DependencyService.Get<IGetSerialDeviceService>().GetSerial();
                        ApiResponse loginResponse;
                        loginResponse = await Login(UserLogged.Phone, UserLogged.Password, serial);

                        if (loginResponse.IsSuccess)
                        {
                            AuthenticateReponse authResponse = JsonConvert.DeserializeObject<AuthenticateReponse>(loginResponse.Content.ToString());
                            //await UserLogged.SaveLogin(authResponse);
                            res = await Post(Path, formContent, true, false);
                        }
                        else
                        {
                            res.Message = "Loi he thong vui long thu lai";
                            res.IsSuccess = false;
                        }
                    }
                    else
                    {
                        // loi dang nhap, co token user pass nhung ko dang nhap duoc.
                        res.Message = "Loi he thong vui long thu lai";
                        res.IsSuccess = false;
                    }
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    res.Message = "Loi he thong vui long thu lai";
                    res.IsSuccess = false;
                }
                return res;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async static Task<ApiResponse> Put(string Path, object formContent, bool Authenticate = false, bool RefreshToken = true)
        {
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                return new ApiResponse(false, null, "No Internnet");
            }
            try
            {
                var client = BsdHttpClient.Instance();
                ApiResponse res = new ApiResponse();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (Authenticate)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserLogged.AccessToken);
                }
                HttpResponseMessage response;

                if (formContent == null)
                {
                    var request = new HttpRequestMessage(HttpMethod.Put, Path);
                    response = await client.SendAsync(request);
                }
                else
                {
                    string objContent = JsonConvert.SerializeObject(formContent);
                    HttpContent content = new StringContent(objContent, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(Path, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    if (UserLogged.IsLogged && Authenticate && RefreshToken)
                    {
                        string serial = await DependencyService.Get<IGetSerialDeviceService>().GetSerial();
                        ApiResponse loginResponse;
                        loginResponse = await Login(UserLogged.Phone, UserLogged.Password, serial);

                        if (loginResponse.IsSuccess)
                        {
                            AuthenticateReponse authResponse = JsonConvert.DeserializeObject<AuthenticateReponse>(loginResponse.Content.ToString());
                            //await UserLogged.SaveLogin(authResponse);
                            res = await Put(Path, formContent, true, false);
                        }
                        else
                        {
                            res.Message = "Loi he thong vui long thu lai";
                            res.IsSuccess = false;
                        }
                    }
                    else
                    {
                        // loi dang nhap, co token user pass nhung ko dang nhap duoc.
                        res.Message = "Loi he thong vui long thu lai";
                        res.IsSuccess = false;
                    }
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    res.Message = "Lỗi";
                    res.IsSuccess = false;
                }
                return res;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async static Task<ApiResponse> Delete(string Path, bool RefreshToken = true)
        {
            var network = Connectivity.NetworkAccess;
            if (network != NetworkAccess.Internet)
            {
                return new ApiResponse(false, null, "No Internnet");
            }
            try
            {
                var client = BsdHttpClient.Instance();
                ApiResponse res = new ApiResponse();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserLogged.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync(Path);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(body);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    if (UserLogged.IsLogged && RefreshToken)
                    {
                        string serial = await DependencyService.Get<IGetSerialDeviceService>().GetSerial();
                        ApiResponse loginResponse;
                        loginResponse = await Login(UserLogged.Phone, UserLogged.Password, serial);

                        if (loginResponse.IsSuccess)
                        {
                            AuthenticateReponse authResponse = JsonConvert.DeserializeObject<AuthenticateReponse>(loginResponse.Content.ToString());
                            //await UserLogged.SaveLogin(authResponse);
                            res = await Delete(Path, false);
                        }
                        else
                        {
                            res.Message = "Loi he thong vui long thu lai";
                            res.IsSuccess = false;
                        }
                    }
                    else
                    {
                        // loi dang nhap, co token user pass nhung ko dang nhap duoc.
                        res.Message = "Loi he thong vui long thu lai";
                        res.IsSuccess = false;
                    }
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    res.Message = "Loi he thong vui long thu lai";
                    res.IsSuccess = false;
                }
                return res;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
