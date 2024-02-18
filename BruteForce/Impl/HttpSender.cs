using BruteForce.Contracts;
using BruteForce.HttpClients;
using BruteForce.Models;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace BruteForce.Impl
{
    internal class HttpSender : ISender<HttpRequest, HttpResponse>
    {
        public readonly IApiHttpClient _apiHttpClient;

        public HttpSender(IApiHttpClient apiHttpClient)
        {
            _apiHttpClient = apiHttpClient;    
        }

        public async Task<HttpResponse> Send(HttpRequest request)
        {
            var response = await _apiHttpClient.SignIn(request);

            if (response.Content == "Вы вошли в аккаунт!")
                return new HttpResponse { Success = true };

            return new HttpResponse { Success = false };
        }
    }
}
