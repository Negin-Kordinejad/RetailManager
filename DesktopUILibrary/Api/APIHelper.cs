using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DesktopUILibrary.Models;
using System.Net.Http.Formatting;

namespace DesktopUILibrary.Api
{
    public class APIHelper : IAPIHelper
    {
        string api = ConfigurationManager.AppSettings["api"];
        private ILoggedInUserModel _loggedUserInfo;
        private HttpClient _apiClient;
        public APIHelper(ILoggedInUserModel loggedUserInfo)
        {
            InitializeClient();
            _loggedUserInfo = loggedUserInfo;
        }
        public HttpClient AppClient
        {
            get
            {
                return _apiClient;
            }
        }
        private void InitializeClient()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string,string>("grant_type","password"),
               new KeyValuePair<string,string>("username",username),
               new KeyValuePair<string,string>("password",password)
            }
            );
            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
      public void LogOffUsser()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }
        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/user"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedUserInfo.FirstName = result.FirstName;
                    _loggedUserInfo.LastName = result.LastName;
                    _loggedUserInfo.CreateDate = result.CreateDate;
                    _loggedUserInfo.EmailAddress = result.EmailAddress;
                    _loggedUserInfo.Token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
