using LololyricsNextPlayer.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LololyricsNextPlayer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel ViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = (MainPageViewModel)DataContext;
        }
    }
}
