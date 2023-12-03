using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab12TODOLIST.Models
{
    public class TaskMasna
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }
    }

    public class CreateTaskResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public TaskMasna Data { get; set; }
    }

    public class DeleteTaskResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
