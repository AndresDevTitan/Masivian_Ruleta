namespace MasivianRuleta.Entities.Response
{
    using System.Collections.ObjectModel;
    using System.Net;

    public class ResponseManager<T>
    {
        /// <summary>
        /// Response OK
        /// </summary>
        /// <param name="rowsAffected"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        public static ModeloResponse<T> ResponseOK(int rowsAffected, Collection<T> resultData)
        {
            return new ModeloResponse<T>
            {
                CodigoRespuesta = HttpStatusCode.OK,
                Mensaje = "Operacion Exitosa",
                Data = resultData
            };
        }

        /// <summary>
        /// Response OK
        /// </summary>
        /// <param name="rowsAffected"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        public static ModeloResponse<T> ResponseOK()
        {
            return new ModeloResponse<T>
            {
                CodigoRespuesta = HttpStatusCode.OK,
                Mensaje = "Operacion Exitosa",
                Data = null
            };
        }

        /// <summary>
        /// Response InternalServerError
        /// </summary>
        /// <returns></returns>
        public static ModeloResponse<T> ResponseInternalServerError(string message)
        {
            return new ModeloResponse<T>
            {
                CodigoRespuesta = HttpStatusCode.InternalServerError,
                Mensaje = message,
                Data = null
            };
        }

        /// <summary>
        /// Response NoContent
        /// </summary>
        /// <returns></returns>
        public static ModeloResponse<T> ResponseNoContent(string message)
        {
            return new ModeloResponse<T>
            {
                CodigoRespuesta = HttpStatusCode.NoContent,
                Mensaje = message,
                Data = null
            };
        }

        /// <summary>
        /// Response Conflict
        /// </summary>
        /// <returns></returns>
        public static ModeloResponse<T> ResponseConflict(string message)
        {
            return new ModeloResponse<T>
            {
                CodigoRespuesta = HttpStatusCode.Conflict,
                Mensaje = message,
                Data = null
            };
        }
    }
}
