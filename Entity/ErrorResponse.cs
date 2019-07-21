using Newtonsoft.Json;
using System.Net;

namespace Entity
{
    /// <summary>
    /// Classe mensagem de erro
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Id do erro
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        /// <summary>
        /// Mensagem do erro
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Status http que foi gerado para o erro
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }
        /// <summary>
        /// String de rastreio do problema
        /// </summary>
        [JsonIgnore]
        public string StackTrace { get; set; }
    }

}
