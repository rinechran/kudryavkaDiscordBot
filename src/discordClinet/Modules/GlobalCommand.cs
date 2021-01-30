using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using kudryavkaDiscordBot.discondClinet.Services;
using Discord.WebSocket;
using System.IO;

namespace kudryavkaDiscordBot.discondClinet.Modules
{
    public class GlobalCommand : ModuleBase<SocketCommandContext>
    {
        static readonly PictureService pictureService = new PictureService();
        NaverPapagoTranslateService translateService = new NaverPapagoTranslateService();

        [Command("kj")]
        public async Task TranslateKoreaToJapen([Remainder] string text)
        {
            string trimText = text.Trim();
            if (string.IsNullOrEmpty(trimText))
            {
                return;
            }

            string translateText = await translateService.TranslateAsync("ko", "ja", text);
            string result = $"> {Context.User} : {text}\n {translateText}";
            await ReplyAsync(result);
            return;
        }
        [Command("jk")]
        public async Task TranslateJapenTokorea([Remainder] string text)
        {
            string trimText = text.Trim();
            if (string.IsNullOrEmpty(trimText))
            {
                return;
            }

            string translateText = await translateService.TranslateAsync("ja", "ko", text);
            string result = $"> {Context.User} : {text}\n {translateText}";
            await ReplyAsync(result);
            return;
        }


        [Command("love")]
        public async Task HttpImageDownlaod()
        {
            Stream stream = await pictureService.GetPictureAsync("https://user-images.githubusercontent.com/15512801/89726891-d6919b80-da5a-11ea-80d9-2262e3712030.jpg");
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "test.jpg");

        }


    }
}
