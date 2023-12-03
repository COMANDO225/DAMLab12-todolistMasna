using Lab12TODOLIST.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Lab12TODOLIST.Services
{

    public class ApiService
    {

        HttpClient fetch = new HttpClient();
        private string apiURL = "https://api-masna.vercel.app/api/tasks";



        public async Task<List<TaskMasna>> GetTasksAsync()
        {
            try
            {
                var res = await fetch.GetAsync(apiURL);
                if (res.IsSuccessStatusCode)
                {
                    var jsonResponse = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<TaskMasna>>(jsonResponse);
                    return data;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return new List<TaskMasna>();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                return new List<TaskMasna>();
            }
        }


        public async Task<TaskMasna> CreateTaskAsync(TaskMasna task)
        {
            try
            {
                var jsonTask = JsonConvert.SerializeObject(task);
                var payload = new StringContent(jsonTask, Encoding.UTF8, "application/json");
                var res = await fetch.PostAsync(apiURL, payload);
                if (res.IsSuccessStatusCode)
                {
                    var jsonResponse = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CreateTaskResponse>(jsonResponse);
                    return data.Data;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                return null;
            }
        }   

        public async Task<TaskMasna> UpdateTaskAsync(TaskMasna task)
        {
            try
            {
                var jsonTask = JsonConvert.SerializeObject(task);
                Console.WriteLine("jsonTask: " + jsonTask);
                var payload = new StringContent(jsonTask, Encoding.UTF8, "application/json");
                var res = await fetch.PutAsync(apiURL, payload);
                if (res.IsSuccessStatusCode)
                {
                    var jsonResponse = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CreateTaskResponse>(jsonResponse);
                    return data.Data;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return null;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                return null;
            }
        }

        public async Task<string> DeleteTaskAsync(string id)
        {
            try
            {
                var idObject = new Dictionary<string, string>
                {
                    { "id", id }
                };

                var jsonId = JsonConvert.SerializeObject(idObject);
                var payload = new StringContent(jsonId, Encoding.UTF8, "application/json");
                // se google bastante pero se pudo encontrar la solucion
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(apiURL),
                    Content = payload
                };

                var res = await fetch.SendAsync(request);
                if (res.IsSuccessStatusCode)
                {
                    var jsonResponse = await res.Content.ReadAsStringAsync();
                    var deleteRes = JsonConvert.DeserializeObject<DeleteTaskResponse> (jsonResponse);
                    return deleteRes.Message;
                }
                else
                {
                    Console.WriteLine("Error: " + res.StatusCode);
                    return "Error al eliminar la tarea" + res.StatusCode;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                return "Error al eliminar la tarea" + error.Message;
            }
        }
    }
}
