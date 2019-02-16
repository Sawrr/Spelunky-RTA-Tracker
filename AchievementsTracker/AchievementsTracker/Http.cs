using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AchievementsTracker
{
    class Http
    {
        private static readonly HttpClient client = new HttpClient();

        private static string URL;

        public static void setURL(string url)
        {
            URL = url;
        }

        public static async Task<string> createRoomAsync()
        {
            HttpResponseMessage res = await client.PostAsync(URL + "/api/rooms", null);

            return await res.Content.ReadAsStringAsync();
        }

        public static async Task<bool> joinRoom(string code)
        {
            HttpResponseMessage res = await client.PutAsync(URL + "/api/rooms/" + code + "/join", null);

            return res.IsSuccessStatusCode;
        }

        public static void startRoom(string code, long time)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, URL + "/api/rooms/" + code + "/start");
            message.Headers.Add("time", time.ToString());
            client.SendAsync(message);
        }

        public static async Task<string> getUpdates(string code)
        {
            HttpResponseMessage res = await client.GetAsync(URL + "/api/rooms/" + code);

            return await res.Content.ReadAsStringAsync();
        }

        public static void sendUpdate(string code, long time, bool host, string body)
        {            
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, URL + "/api/rooms/" + code + "/update");
            message.Headers.Add("time", time.ToString());
            message.Headers.Add("player", host ? "host" : "guest");
            message.Content = new StringContent(body, UnicodeEncoding.UTF8, "application/json");
            client.SendAsync(message);
        }

    }
}
