using LocalApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LocalApp.Services
{
    public class ApiService
    {
        private const string url = "http://192.168.42.1:100";
        
        public static async Task<List<T>> GetItems<T>(string controller) where T : new()
        {
  
            List<T> list = new List<T>();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{url}/{controller}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync(fullUrl);
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            return list;
        }
        public static async Task<T> GetItem<T>(string controller) where T :new()
        {
            T item = new T();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{url}/{controller}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync(fullUrl);
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<T>(json);
                }
            }
            return item;
        }

        public static async Task<bool> AddItem<T>(string controller,T item)
        {
            bool success = false;
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                string fullUrl = $"{url}/{controller}";
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.PostAsync(fullUrl, content);
                success = message.IsSuccessStatusCode;
            }
            return success;
        }

        public static async Task<bool>UpdateItem<T>(string controller,T item,int id)where T :new()
        {
            bool success=false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{url}/{controller}/{id}";
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.PutAsync(fullUrl,content);
                success = message.IsSuccessStatusCode;

            }
            return success;
        }
        public static async Task<bool>DeleteItem<T>(string controller,int id)where T : new()
        {
            bool success = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string fullUrl = $"{url}/{controller}/{id}";
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.DeleteAsync(fullUrl);
                success = message.IsSuccessStatusCode;
            }
            return success;
        }
    }
}
