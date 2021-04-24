using System.Windows;
using CurrencyClient.ViewModel;

namespace CurrencyClient
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainViewModel = new MainViewModel();

            var mainView = new MainWindow(mainViewModel);
            mainView.Show();
        }
    }
}
