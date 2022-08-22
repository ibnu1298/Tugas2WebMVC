using System.Text;
using Newtonsoft.Json;
using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public interface IUser
    {
        Task<User> Register(User obj);
        Task<UserData> Login(Login obj);
        Task<UserData> GetEmail(string email);
    }
    public class UserServices : IUser
    {
        public async Task<UserData> Login(Login obj)
        {
            UserData user = await GetEmail(obj.Email);
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/User/Login", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserData>(apiResponse);
                    }
                    else
                    {
                        throw new Exception("Login Gagal, Email atau Password Salah");
                    }
                }
            }
            return user;
        }

        public async Task<User> Register(User obj)
        {
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/User", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                }
            }
            return user;
        }

        public async Task<UserData> GetEmail(string email)
        {
            UserData user = new UserData();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/User/{email}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserData>(apiResponse);
                    }
                }
            }
            return user;
        }

    }
}
