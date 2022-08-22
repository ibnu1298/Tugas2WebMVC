using System.Text;
using Newtonsoft.Json;
using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public class EnrollServices : IEnrollment
    {
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrollmentCS>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrollmentCS>> GetByGrade(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<EnrollmentCS> Insert(EnrollmentCS obj,string token)
        {
            EnrollmentCS student = new EnrollmentCS();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PostAsync("https://localhost:7062/api/Enrollment", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<EnrollmentCS>(apiResponse);
                    }
                }
            }
            return student;
        }

        public Task<EnrollmentCS> Update(EnrollmentCS obj)
        {
            throw new NotImplementedException();
        }
    }
}
