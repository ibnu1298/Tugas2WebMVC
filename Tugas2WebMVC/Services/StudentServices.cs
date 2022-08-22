using System.Text;
using Tugas2WebMVC.Models;
using Newtonsoft.Json;

namespace Tugas2WebMVC.Services
{
    public class StudentServices : IStudent
    {
        public async Task Delete(int id, string? token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:7062/api/Student/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"Gagal Mengahapus data");
                    }
                }
            }
        }

        public async Task<IEnumerable<Student>> GetAll(string token)
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:7062/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }

        public async Task<StudentCourse> GetById(int id, string? token)
        {
            StudentCourse student = new StudentCourse();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Student/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<StudentCourse>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<IEnumerable<StudentCourse>> GetByNameFull(string name, string? token)
        {
            List<StudentCourse> students = new List<StudentCourse>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Student/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<StudentCourse>>(apiResponse);
                }
            }
            return students;
        }

        public async Task<IEnumerable<Student>> GetByName(string name, string? token)
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Student/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }

        public async Task<Paging> GetPaging(int id, string token)
        {
            Paging student = new Paging();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Student/Paging/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Paging>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentCourse(string token)
        {
            List<StudentCourse> students = new List<StudentCourse>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:7062/api/Student/WithCourse"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<StudentCourse>>(apiResponse);
                }
            }
            return students;
        }

        public async Task<Student> Insert(Student obj, string? token)
        {
            Student student = new Student();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/Student", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<StudentCourse> Update(StudentCourse obj, string? token)
        {
            StudentCourse student = await GetById(obj.id, token);
            if (student == null)
            {
                throw new Exception($"Data dengan Id = {obj.id} Tidak ditemukan");
            }
            StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:7062/api/Student", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<StudentCourse>(apiResponse);
                }
            }
            return student;
        }
    }
}
