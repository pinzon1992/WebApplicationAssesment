using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace WebApplicationAssesment.Common.Models
{
    /// <summary>
    /// Modelo base
    /// </summary>
    public class ResponseBaseModel: Controller
    { 
        /// <summary>
        /// Response Code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }

        /// <summary>
        /// StackTrace only visible in dev or test environments
        /// </summary>
        [JsonProperty(PropertyName = "stacktrace")]
        public string? StackTrace { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object? Data { get; set; }

    }
}
