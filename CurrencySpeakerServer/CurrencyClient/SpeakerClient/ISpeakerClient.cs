using System.Threading.Tasks;

namespace CurrencyClient.SpeakerClient
{
    public interface ISpeakerClient
    {
        Task<string> ConvertCurrency(string input);
    }
}