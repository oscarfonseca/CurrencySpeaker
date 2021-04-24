using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyClient.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        public MainViewModel()
        {
            ConvertCurrencyCommand = new DelegateCommand(async _ => await ConvertCurrency());
        }

        private async Task ConvertCurrency()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:50827/currencyspeaker?amount={Input}");
            if (response.IsSuccessStatusCode)
                Result = await response.Content.ReadAsStringAsync();
            else
                Result = $"Error: {response.StatusCode}";
        }
        public ICommand ConvertCurrencyCommand { get; set; }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                if (value.Equals(_result))
                    return;

                _result = value;
                OnPropertyChanged();
            }
        }

        private string _input;
        public string Input
        {
            get => _input;
            set
            {
                if (value.Equals(_input))
                    return;

                _input = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}