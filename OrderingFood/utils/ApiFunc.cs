using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using OrderingFood.pages;

namespace OrderingFood.utils
{
    internal class ApiFunc
    {
        public static string json;

        public async static Task getItemDish()
        {
            var client = new HttpClient();
            var uri = new Uri("http://localhost:8080/getList");
            var content = await client.GetStringAsync(uri);
            json = content;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var r = ser.Deserialize<List<Dish>>(json);
            ItemPage.dishList = r;
        }
        public static async Task<List<Dish>> GetAll()
        {
            var httpClient = new HttpClient();
            HttpResponseMessage r;
            try { r = await httpClient.GetAsync($"").ConfigureAwait(false); }
            catch { return null; }

            string result = await r.Content.ReadAsStringAsync().ConfigureAwait(false);

            try { return JsonConvert.DeserializeObject<List<Dish>>(result); }
            catch { return null; }
        }
    }


    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }

    public static class HttpClientExtensions
    {
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer();

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent httpContent)
        {
            using (var stream = await httpContent.ReadAsStreamAsync())
            {
                var jsonReader = new JsonTextReader(new StreamReader(stream));

                return JsonSerializer.Deserialize<T>(jsonReader);
            }
        }
    }



}
