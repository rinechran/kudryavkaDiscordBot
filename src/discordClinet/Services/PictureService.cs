using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
namespace kudryavkaDiscordBot.discondClinet.Services
{
    class PictureService
    {
        private readonly HttpClient http = new HttpClient();

        public async Task<Stream> GetPictureAsync(string url)
        {
            var res = await http.GetAsync(url);
            return await res.Content.ReadAsStreamAsync();
        }
    }
}
