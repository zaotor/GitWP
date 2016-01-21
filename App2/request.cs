using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;


namespace App2
{
    class request
    {
        public string Response { get; set; }
        public MessageDialog msgbox { get; set; }

        public string Get(string url, string route, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = httpClient.GetAsync(url + route).Result;
            //msgbox = new MessageDialog(response.ToString());
            //msgbox.ShowAsync();
            try {
                object json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                if (json != null)
                {
                    string content = json.ToString();
                    return (content);
                }
                else
                    return ("");
            }
            catch (Exception e)
            {
                return ("");
            }
            //    if (response.IsSuccessStatusCode)
            //    {
            //        msgbox = new MessageDialog(content.ToString());
            //        msgbox.ShowAsync();
            //    }


            }

        public string Put(string url, string route, string json, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = httpClient.PutAsync(url + route, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                return (content);
            }
            return response.ToString();
        }

        public TokenAt Login(string url, string route, string json)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                response = httpClient.PostAsync(url + route, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    TokenAt content = JsonConvert.DeserializeObject<TokenAt>(response.Content.ReadAsStringAsync().Result);
                    return (content);
                }
                return (new TokenAt());
            }
            catch (Exception e)
            {
                msgbox = new MessageDialog("An error appeared  : " + e.Message);
                msgbox.ShowAsync();
                throw;
            }
        }

        public string Post(string url, string route, string json, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = httpClient.PostAsync(url + route, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
               // msgbox = new MessageDialog(content);
               // msgbox.ShowAsync();
                return (content);
            }
            else
            {
                return ("");
            }
        }

        public bool deleteAccount(string id, string token)
        {
            /*msgbox = new MessageDialog(url + token);
            msgbox.ShowAsync();*/
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response;
            try
            {
                response = httpClient.DeleteAsync("http://api.linkat.fr/api/users/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                msgbox = new MessageDialog("An error appeared  : " + e.Message);
                msgbox.ShowAsync();
                throw;
            }
        }

        public bool Logout(string url, string token)
        {
            /*msgbox = new MessageDialog(url + token);
            msgbox.ShowAsync();*/
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response;
            try
            {
                response = httpClient.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                msgbox = new MessageDialog("An error appeared  : " + e.Message);
                msgbox.ShowAsync();
                throw;
            }
        }
    }
}
