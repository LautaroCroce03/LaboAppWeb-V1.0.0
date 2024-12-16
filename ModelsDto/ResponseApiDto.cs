using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaboAppWebV1._0._0.ModelsDto
{
    public class ResponseApiDto : ProblemDetails
    {
        public string TraceId { get; set; } = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
        
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
