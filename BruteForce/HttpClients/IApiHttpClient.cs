using BruteForce.Models;
using Refit;

namespace BruteForce.HttpClients
{
    internal interface IApiHttpClient
    {
        [Post("/Auth/signIn")]
        public Task<ApiResponse<string>> SignIn(HttpRequest request);
        [Get("/Profile/passport")]
        public Task<ApiResponse<PassportModel>> GetPassportData();
    }
}
