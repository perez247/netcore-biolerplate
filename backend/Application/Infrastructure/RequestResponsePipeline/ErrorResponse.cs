using Newtonsoft.Json;

namespace Application.Infrastructure.RequestResponsePipeline
{
    /// <summary>
    /// This class is responsible for sending back server-side error
    /// This should be used as the fundamental.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// This is basically used for for sending validation errors
        /// This contains multiple errors and will be stored as a list of error
        /// </summary>
        /// <value>list{objects}</value>
        [JsonProperty("errors")]
        public object Errors { get; set; }

        /// <summary>
        /// This should contain only a single error 
        /// This is any other error
        /// </summary>
        /// <value>string</value>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// This is used for debugging and should show only in development or staging environment
        /// </summary>
        /// <value></value>
        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }
    }
}