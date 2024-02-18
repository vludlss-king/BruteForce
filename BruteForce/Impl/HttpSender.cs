using BruteForce.Contracts;
using BruteForce.Models;
using Newtonsoft.Json;

namespace BruteForce.Impl
{
    internal class HttpSender : ISender<HttpRequest, HttpResponse>
    {
        public async Task<HttpResponse> Send(HttpRequest request)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7250");

            var requestContent = new StringContent(JsonConvert.SerializeObject(request));
            var response = await client.PostAsync("/Auth/signIn", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent == "Вы вошли в аккаунт!")
                return new HttpResponse { Success = true };

            return new HttpResponse { Success = false };
        }
    }
}
