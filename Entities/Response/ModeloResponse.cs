namespace MasivianRuleta.Entities.Response
{
    using System.Collections.ObjectModel;
    using System.Net;

    /// <summary>
    /// HTTP response model class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModeloResponse<T>
    {
        /// <summary>
        /// Request response message
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Request status code
        /// </summary>
        public HttpStatusCode CodigoRespuesta { get; set; }
        /// <summary>
        /// Request data collection
        /// </summary>
        public Collection<T> Data { get; set; }
    }
}
