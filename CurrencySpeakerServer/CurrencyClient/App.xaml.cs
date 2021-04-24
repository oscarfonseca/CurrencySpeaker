using System.Windows;
using CurrencyClient.ViewModel;

namespace CurrencyClient
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var speaker = new SpeakerClient.SpeakerClient();
            var mainViewModel = new MainViewModel(speaker);

            var mainView = new MainWindow(mainViewModel);
            mainView.Show();
        }
    }
}
