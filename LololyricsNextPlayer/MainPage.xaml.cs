using LyricsService;
using NextPlayerExtensionsAPI;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LololyricsNextPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            test();
        }

        private async Task test()
        {
            LyricsRequest r = new LyricsRequest()
            {
                Album = "",
                Artist = "Rihanna",
                Title = "Umbrella"
            };
            LololyricsService service = new LololyricsService();
            var q1 = await service.GetLyrics(r);
            r.Artist = "fakeartist";
            r.Title = "faketitle";
            var q2 = await service.GetLyrics(r);
        }
    }
}
