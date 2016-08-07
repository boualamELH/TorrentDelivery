using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Basics.API
{
    public sealed class TvDBWrapper
    {
        #region Constants
        private const string KEY = "EB986447614577CA";
        private const string ApiRoot = "https://api.thetvdb.com/login";
        private const string api = "https://api.thetvdb.com/";
        private const string GetSeriesURL = "https://api.thetvdb.com/search/series?name=";
        private const string apikey = "";
        private const string username = "";
        private const string userkey = "";
        private static string ApiToken = "";
        #endregion

        #region Static Methods
        /// <summary>
        /// Get series by name
        /// </summary>
        /// <param name="serieName"></param>
        /// <returns>Series list</returns>
        public static async Task<List<Serie>> GetSeriesInfoByName(string serieName)
        {
            var response = new List<Serie>();

            // check if the token is expired
            if (IsExpired())
            {
                using (var reqhttpclient = new HttpClient())
                {
                    var token = await getToken();
                    // store token//

                    // Fill request headers // 
                    reqhttpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    reqhttpclient.DefaultRequestHeaders.Add("Accept", "application/json");

                    // Do the actual request and await the response
                    var seriesResponse = await reqhttpclient.GetAsync(GetSeriesURL + serieName);
                    if (seriesResponse.Content != null)
                    {
                        var responseContent = await seriesResponse.Content.ReadAsStringAsync();
                        JToken data = JObject.Parse(responseContent);
                        // deserialize json response to Series list //
                        JArray dataArray = (JArray)data["data"];
                        response = dataArray.ToObject<List<Serie>>();
                        return response;
                    }
                    return null;
                }
            }
            else
            {
                // Refrech token
                var newToken = await refresh();
                // update token
                // get series infos
                return null;
            }
        }

        /// <summary>
        /// Get token
        /// </summary>
        /// <returns>token</returns>
        public static async Task<string> getToken()
        {
            // serialize json request api
            var stringPayload = "{\"apikey\":\"" + apikey + "\",\"username\":\"" + username + "\",\"userkey\":\"" + userkey + "\"}";
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                // requesting and await for the response
                var httpResponse = await httpClient.PostAsync(ApiRoot, httpContent);
                // If the response contains content we want to get!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    JToken tokenElement = JObject.Parse(responseContent).GetValue("token");
                    string selectedToken = tokenElement.ToString();
                    return selectedToken;
                }
            }
            return null;
        }


        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns>new Token</returns>
        public static async Task<string> refresh()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiToken);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await httpClient.GetAsync(api + "refresh_token");

                if (response.Content != null)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    JToken tokenElement = JObject.Parse(responseContent).GetValue("token");
                    ApiToken = tokenElement.ToString();
                }

                return null;
            }
        }

        /// <summary>
        /// Check if the token is expired to get an new one
        /// </summary>
        /// <returns></returns>
        public static bool IsExpired()
        {
            return true;
        }

        #endregion
    }
}
