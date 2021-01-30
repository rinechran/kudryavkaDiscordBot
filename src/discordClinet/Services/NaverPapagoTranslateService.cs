using kudryavkaDiscordBot.discondClinet.Config;
using kudryavkaDiscordBot.discondClinet.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace kudryavkaDiscordBot.discondClinet.Services
{


    public class NaverPapagoTranslateService
    {
        static readonly Configuration configuration = Singleton<Configuration>.Instance;
        static readonly string papagoURL = "https://openapi.naver.com/v1/papago/n2mt";
        private static readonly HttpClient client = new HttpClient();

        static NaverPapagoTranslateService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            client.BaseAddress = new Uri(papagoURL);
            client.DefaultRequestHeaders.Add("X-Naver-Client-Id", configuration.NAVER_CLIENT_ID);
            client.DefaultRequestHeaders.Add("X-Naver-Client-Secret", configuration.NAVER_CLINET_SECRET);
        }

        public async Task<string> TranslateAsync(string source,string target, string text)
        {
            var pocoObject = new
            {
                source,
                target,
                text
            };
            string json = JsonConvert.SerializeObject(pocoObject);

            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", data);
            string result = await response.Content.ReadAsStringAsync();
            JObject returnjson = JObject.Parse(result);
            return (string)returnjson["message"]["result"]["translatedText"];
        }
    }
}
