using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace AchievementsTracker
{
    class Http
    {
        private static readonly HttpClient client = new HttpClient();

        private static string URL;
        private static long timeOffset;

        public static void setURL(string url)
        {
            URL = url;
        }

        public static long getTimeOffset()
        {
            return timeOffset;
        }

        public static async Task<string> createRoom()
        {
            CancellationToken cancelToken = new CancellationTokenSource(new TimeSpan(0, 0, 5)).Token;
            HttpResponseMessage res = await client.PostAsync(URL + "/api/rooms", null, cancelToken);
            return await res.Content.ReadAsStringAsync();
        }

        public static async Task<HttpResponseMessage> joinRoom(string code)
        {
            CancellationToken cancelToken = new CancellationTokenSource(new TimeSpan(0, 0, 5)).Token;
            return await client.PutAsync(URL + "/api/rooms/" + code + "/join", null, cancelToken);
        }

        public static void startRoom(string code, long time)
        {
            time = time + timeOffset;

            if (code != null)
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, URL + "/api/rooms/" + code + "/start");
                message.Headers.Add("time", time.ToString());
                client.SendAsync(message);
            }
        }

        public static async Task<string> getUpdates(string code)
        {
            HttpResponseMessage res = await client.GetAsync(URL + "/api/rooms/" + code);

            return await res.Content.ReadAsStringAsync();
        }

        public static void sendUpdate(string code, long time, bool host, string body)
        {
            time = time + timeOffset;

            if (code != null)
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, URL + "/api/rooms/" + code + "/update");
                message.Headers.Add("time", time.ToString());
                message.Headers.Add("player", host ? "host" : "guest");
                message.Content = new StringContent(body, UnicodeEncoding.UTF8, "application/json");
                client.SendAsync(message);
            }
        }

        // Query NTP server for time
        public static void GetAndSetTimeOffset()
        {
            string[] ntpServers = new string[]{"pool.ntp.org", "time.windows.com", "time.nist.gov"};
            
            for (int i = 0; i < ntpServers.Length; i++)
            {
                var ntpData = new byte[48];
                ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServers[0]).AddressList;
                Log.WriteLine("Querying NTP server: " + addresses[0]);
                var ipEndPoint = new IPEndPoint(addresses[i], 123);
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                DateTime systemTime = DateTime.UtcNow;
                socket.Receive(ntpData);
                socket.Close();

                ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                DateTime networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

                Log.WriteLine(systemTime.ToString());
                Log.WriteLine(networkDateTime.ToString());

                TimeSpan offset = networkDateTime.Subtract(systemTime);
                Log.WriteLine("Difference in ms: " + offset.TotalMilliseconds);
                if (Math.Abs(offset.TotalDays) < 1)
                {
                    timeOffset = (long)offset.TotalMilliseconds;
                    return;
                }
            }

            // Failed to synchronize
            throw new Exception("Queried all NTP servers without success");
        }

    }
}
