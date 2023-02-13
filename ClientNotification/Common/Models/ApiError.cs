using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ClientNotification.Common.Models
{
    public class ApiError
    {
        public DateTime Timestamp { get; set; }

        public int Status { get; set; }

        public string Path { get; set; }

        public string Error { get; set; } // Repeat text status code

        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<object> Errors { get; set; }

        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiError(HttpStatusCode statusCode)
        {
            Timestamp = DateTime.UtcNow;
            Status = (int)statusCode;
        }
    }
}
