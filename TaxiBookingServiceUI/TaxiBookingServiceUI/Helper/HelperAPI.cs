using System.Net.Http.Headers;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Helper
{
    public class HelperAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7194/");
            return client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpClient client = Initial();      
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);
            HttpResponseMessage message = await client.GetAsync(url);
            return message;
        }

        public HttpResponseMessage PostAsJsonAsync<TEntity>(string url , TEntity model , string token) where TEntity : class
        {
            using (HttpClient client = Initial()) 
            {
                if(token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
                var postTask = client.PostAsJsonAsync<TEntity>(url, model);
                postTask.Wait();
                return postTask.Result;
            } 
           
            
        }

        public HttpResponseMessage PutAsJsonAsync<TEntity>(string url, TEntity model, int id, string token) where TEntity : class
        {
            using (HttpClient client = Initial())
            {
                if (token != null)
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
                var putTask = client.PutAsJsonAsync<TEntity>(url+$"/{id}", model);
                putTask.Wait();
                return putTask.Result;
            }


        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, int id)
        {
            using (HttpClient client = Initial())
            {
                HttpResponseMessage message = await client.DeleteAsync(url + id);
                return message;
            }
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

        }
        
    }
}
