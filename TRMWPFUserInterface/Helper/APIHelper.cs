using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TRMWPFUserInterface.Models;

namespace TRMWPFUserInterface.Helper
{
    public class APIHelper : IAPIHelper
    {
        string api = ConfigurationManager.AppSettings["api"];
        private HttpClient apiClient { get; set; }
        public APIHelper()
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
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
    }
}
