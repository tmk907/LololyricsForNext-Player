using LyricsService;
using Newtonsoft.Json;
using NextPlayerExtensionsAPI;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LololyricsNextPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LololyricsService service;

        public MainPage()
        {
            this.InitializeComponent();
            service = new LololyricsService();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            Search.IsEnabled = false;
            LyricsTB.Text = "";
            scrollViewer.Visibility = Visibility.Collapsed;
            progressRing.Visibility = Visibility.Visible;
            progressRing.IsActive = true;
            LyricsRequest r = new LyricsRequest()
            {
                Album = "",
                Artist = ArtistTB.Text,
                Title = TitleTB.Text
            };

            var serialized = await service.GetLyrics(r);

            if (!string.IsNullOrEmpty(serialized))
            {
                var response = JsonConvert.DeserializeObject<LyricsResponse>(serialized);
                LyricsTB.Text = response.Lyrics;
            }
            if (LyricsTB.Text == "")
            {
                LyricsTB.Text = "No lyrics found";
            }
            scrollViewer.Visibility = Visibility.Visible;
            progressRing.Visibility = Visibility.Collapsed;
            progressRing.IsActive = false;
            Search.IsEnabled = true;
        }
    }
}
