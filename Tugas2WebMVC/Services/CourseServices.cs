using System.Text;
using Newtonsoft.Json;
using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public class CourseServices : ICourse
    {
        public async Task Delete(int id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:7062/api/Course/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"Gagal Mengahapus data");
                    }
                }
            }
        }

        public async Task<IEnumerable<Course>> GetAll(string token)
        {
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:7062/api/Course"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                }
            }
            return courses;
        }

        public async Task<CourseStudent> GetById(int id, string token)
        {
            CourseStudent course = new CourseStudent();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Course/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<CourseStudent>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<IEnumerable<Course>> GetByName(string name, string token)
        {
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Course/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                }
            }
            return courses;
        }
        public async Task<IEnumerable<CourseStudent>> GetByNameFull(string name, string token)
        {
            List<CourseStudent> courses = new List<CourseStudent>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Course/ByName/{name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<CourseStudent>>(apiResponse);
                }
            }
            return courses;
        }

        public async Task<PagingCourse> GetPaging(int id, string token)
        {
            PagingCourse courses = new PagingCourse();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync($"https://localhost:7062/api/Course/Paging/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        courses = JsonConvert.DeserializeObject<PagingCourse>(apiResponse);
                    }
                }
            }
            return courses;
        }

        public async Task<IEnumerable<CourseStudent>> GetCourseStudent(string token)
        {
            List<CourseStudent> courses = new List<CourseStudent>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:7062/api/Course/WithStudent"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<CourseStudent>>(apiResponse);
                }
            }
            return courses;
        }

        public async Task<Course> Insert(Course obj, string token)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/Course", content))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    {
                        throw new Exception ("Data Sudah Ada");
                    }
                    else
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<CourseStudent> Update(CourseStudent obj, string token)
        {
            CourseStudent student = await GetById(obj.courseID,token);
            if (student == null)
            {
                throw new Exception($"Data dengan Id = {obj.courseID} Tidak ditemukan");
            }
            StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:7062/api/Course", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<CourseStudent>(apiResponse);
                }
            }
            return student;
        }
    }
}
