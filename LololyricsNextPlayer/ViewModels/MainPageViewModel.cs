using GalaSoft.MvvmLight;
using LyricsService;
using UWPMusicPlayerExtensions.Messages;

namespace LololyricsNextPlayer.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private LololyricsService service = new LololyricsService();

        private string artist = "";
        public string Artist
        {
            get { return artist; }
            set { Set(nameof(Artist), ref artist, value); }
        }

        private string title = "";
        public string Title
        {
            get { return title; }
            set { Set(nameof(Title), ref title, value); }
        }

        private string lyrics = "";
        public string Lyrics
        {
            get { return lyrics; }
            set { Set(nameof(Lyrics), ref lyrics, value); }
        }

        private bool isSearching = false;
        public bool IsSearching
        {
            get { return isSearching; }
            set { Set(nameof(IsSearching), ref isSearching, value); }
        }

        public async void FindLyrics()
        {
            IsSearching = true;
            Lyrics = "";
            LyricsRequest r = new LyricsRequest()
            {
                Album = "",
                Artist = artist,
                Title = title
            };

            var response = await service.GetLyrics(r);

            if (response != null)
            {
                Lyrics = response.Lyrics;
            }
            if (Lyrics == "")
            {
                Lyrics = "No lyrics found";
            }
            IsSearching = false;
            Artist = "";
            Title = "";
        }
    }
}
