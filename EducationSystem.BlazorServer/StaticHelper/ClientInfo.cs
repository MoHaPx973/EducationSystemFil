using EducationSystem.Shared.Auth;
using EducationSystem.Shared.Models;
using System.Net.Http.Headers;

namespace EducationSystem.BlazorServer.StaticHelpers
{
    static public class ClientInfo
    {
        static public HttpClient Http { get; set; } = new();
        static public string FullName { get; set; } = string.Empty;
        static public UserDto UserInfo { get; set; } = new();
        static public bool IsAuthenticated { get; set; } = false;
        static public void Login(string token)
        {
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Http.BaseAddress = new Uri("http://localhost:9730/");
        }
        static public void Logout()
        {
            //Http.Dispose();
            Http = new();
            FullName = string.Empty;
            IsAuthenticated = false;
            UserInfo = new();
        }
        static public void AddClientInfo(UserDto user, int priority)
        {
            IsAuthenticated = true;
            FullName = $"{user.LinkPerson.Surname} {user.LinkPerson.Name}";
            UserInfo = user;
        }
    }
}
