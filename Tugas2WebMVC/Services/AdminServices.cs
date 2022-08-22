using System.Text;
using Newtonsoft.Json;
using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public interface IAdmin
    {
        Task<IEnumerable<Role>> GetAll(string token);
        Task<IEnumerable<UserToRole>> GetUserRole(string token);
        Task Delete(string id, string token);
        Task<Role> Insert(Role obj, string token);
        Task<Role> GetByName(string name, string token);
        Task<UserToRole> GetByEmail(string email, string token);
        Task<Admin> Admin(Admin obj, string token);
        Task<Role> Update(Role obj, string token);
    }
    public class AdminServices : IAdmin
    {
        public async Task<Role> GetByName(string name, string token)
        {
            Role role = new Role();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Admin/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    role = JsonConvert.DeserializeObject<Role>(apiResponse);
                }
            }
            return role;
        }

        public async Task Delete(string id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:7062/api/Admin/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"Gagal Mengahapus data");
                    }
                }
            }
        }

        public async Task<IEnumerable<Role>> GetAll(string token)
        {
            List<Role> role = new List<Role>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:7062/api/Admin"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    role = JsonConvert.DeserializeObject<List<Role>>(apiResponse);
                }
            }
            return role;
        }

        public async Task<Role> Insert(Role obj, string token)
        {
            Role role = new Role();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/Admin", content))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    {
                        throw new Exception("Data Sudah Ada");
                    }
                    else
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        role = JsonConvert.DeserializeObject<Role>(apiResponse);
                    }
                }
            }
            return role;
        } 
        public async Task<Admin> Admin(Admin obj, string token)
        {
            Admin admin = new Admin();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/Admin/UserRole", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        admin = JsonConvert.DeserializeObject<Admin>(apiResponse);
                    }
                    //else
                    //{
                    //    throw new Exception("Gagal Mendaftarkan Role");
                    //}
                }
            }
            return admin;
        }

        public async Task<Role> Update(Role obj, string token)
        {
            Role student = await GetByName(obj.Name, token);
            if (student == null)
            {
                throw new Exception($"Data {obj.Name} Tidak ditemukan");
            }
            StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:7062/api/Admin", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Role>(apiResponse);
                }
            }
            return student;
        }

        public async Task<IEnumerable<UserToRole>> GetUserRole(string token)
        {
            List<UserToRole> users = new List<UserToRole>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:7062/api/Admin/UserRole"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<UserToRole>>(apiResponse);
                }
            }
            return users;
        }

        public async Task<UserToRole> GetByEmail(string email, string token)
        {
            UserToRole users = new UserToRole();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Admin/User/{email}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<UserToRole>(apiResponse);
                }
            }
            return users;
        }
    }
}
