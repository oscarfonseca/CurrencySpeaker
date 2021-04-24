using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyClient.SpeakerClient
{
    public class SpeakerClient:ISpeakerClient
    {
        public async Task<string> ConvertCurrency(string input)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:50827/currencyspeaker?amount={input}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return $"Error: {response.StatusCode}";
        }
    }
}