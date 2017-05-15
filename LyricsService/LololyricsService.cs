using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using UWPMusicPlayerExtensions.Service;
using UWPMusicPlayerExtensions.Messages;

namespace LyricsService
{
    public class LololyricsService
    {
        public async Task<ValueSet> ParseDataAndGetLyrics(ValueSet message)
        {
            ValueSet response = new ValueSet();

            LyricsExtensionService service = new LyricsExtensionService();
            var request = service.GetRequest(message);
            var result = await GetLyrics(request);
            response = service.PrepareResponse(result);

            return response;
        }

        public async Task<LyricsResponse> GetLyrics(LyricsRequest request)
        {
            var response = new LyricsResponse()
            {
                Lyrics = "",
                Url = "",
            };

            if (IsInternetAvailable)
            {
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
            }
            return response;
        }

        private bool IsInternetAvailable
        {
            get
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    return false;
                }

                var profile = NetworkInformation.GetInternetConnectionProfile();

                NetworkConnectivityLevel level = profile.GetNetworkConnectivityLevel();

                switch (level)
                {
                    case NetworkConnectivityLevel.None:
                    case NetworkConnectivityLevel.LocalAccess:
                        return false;

                    default:
                        return true;
                }
            }
        }
    }
}
