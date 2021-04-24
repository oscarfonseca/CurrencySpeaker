using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CurrencyClient.SpeakerClient;

namespace CurrencyClient.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private readonly ISpeakerClient _speaker;

        public MainViewModel(ISpeakerClient speaker)
        {
            _speaker = speaker;
            ConvertCurrencyCommand = new DelegateCommand(async _ => await ConvertCurrency());
        }

        private async Task ConvertCurrency() => Result = await _speaker.ConvertCurrency(Input);

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