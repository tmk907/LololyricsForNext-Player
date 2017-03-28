using Newtonsoft.Json;
using NextPlayerExtensionsAPI;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace LyricsService
{
    public class LololyricsService
    {
        public async Task<string> GetLyrics(LyricsRequest request)
        {
            var response = new LyricsResponse()
            {
                Lyrics = "",
                Url = "",
            };

            string url = $"http://api.lololyrics.com/0.5/getLyric?artist={request.Artist}&track={request.Title}&rawutf8=1";

            try
            {
                var doc = await XmlDocument.LoadFromUriAsync(new Uri(url));
                var status = doc.DocumentElement.ChildNodes.FirstOrDefault(x => x.NodeName == "status");
                if (status.InnerText == "OK")
                {
                    response.Lyrics = doc.DocumentElement.ChildNodes.FirstOrDefault(x => x.NodeName == "response")?.InnerText ?? "";
                    response.Url = doc.DocumentElement.ChildNodes.FirstOrDefault(x => x.NodeName == "url")?.InnerText ?? "";
                }

            }
            catch (Exception ex)
            {

            }
            string serialized = JsonConvert.SerializeObject(response);
            return serialized;
        }

        public async Task<string> ParseDataAndGetLyrics(string data)
        {
            string result = "";
            try
            {
                LyricsRequest request = JsonConvert.DeserializeObject<LyricsRequest>(data);
                result = await GetLyrics(request);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
