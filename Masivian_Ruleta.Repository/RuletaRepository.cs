namespace Masivian_Ruleta.Repository
{
    using Masivian_Ruleta.Repository.Interface;
    using MasivianRuleta.DataAccess;
    using MasivianRuleta.Entities;
    using MasivianRuleta.Entities.Cliente;
    using MasivianRuleta.Entities.Response;
    using MasivianRuleta.Entities.Ruleta;
    using Serilog;
    using System.Linq;

    /// <summary>
    /// Roulette entity repository
    /// </summary>
    public class RuletaRepository : IRuletaRepository
    {
        /// <summary>
        /// Active the roulette
        /// </summary>
        /// <param name="idRuleta"></param>
        /// <returns></returns>
        public ModeloResponse<int> ActiveRuletas(int idRuleta)
        {
            try
            {
                using (DataConnectionFactory<int> dapper = new ConnectionFactory<int>(AppConfiguration.Instance.MasivianBD).GetConnectionMananager())
                {
                    dapper.AddParameter("IdRuleta", idRuleta);
                    var list = dapper.GetList(StoreProcedures.SP_ActiveRuletas);
                    if (list != null && list.Any())
                        return ResponseManager<int>.ResponseOK(0, list);

                    return ResponseManager<int>.ResponseInternalServerError("Error al activar ruleta");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<int>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Close the bet
        /// </summary>
        /// <param name="cerrarRuletaRequest"></param>
        /// <returns></returns>
        public ModeloResponse<CerrarRuletaResponse> CloseBet(CerrarRuletaRequest cerrarRuletaRequest)
        {
            try
            {
                using (DataConnectionFactory<CerrarRuletaResponse> dapper = new ConnectionFactory<CerrarRuletaResponse>(AppConfiguration.Instance.MasivianBD).GetConnectionMananager())
                {
                    dapper.AddParameter("IdRuleta", cerrarRuletaRequest.IdRuleta);
                    dapper.AddParameter("NumeroGanador", cerrarRuletaRequest.NumeroGanador);
                    var list = dapper.GetList(StoreProcedures.SP_CloseBet);
                    if (list != null && list.Any())
                        return ResponseManager<CerrarRuletaResponse>.ResponseOK(0, list);

                    return ResponseManager<CerrarRuletaResponse>.ResponseInternalServerError("Error al cerrar ruleta");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<CerrarRuletaResponse>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Create a new roulette
        /// </summary>
        /// <returns></returns>
        public ModeloResponse<int> CreateRuleta()
        {
            try
            {
                using (DataConnectionFactory<int> dapper = new ConnectionFactory<int>(AppConfiguration.Instance.MasivianBD).GetConnectionMananager())
                {
                    var list = dapper.GetList(StoreProcedures.SP_CreateRuleta);
                    if (list != null && list.Any())
                        return ResponseManager<int>.ResponseOK(0, list);

                    return ResponseManager<int>.ResponseNoContent("No fue posible crear la ruleta");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<int>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Gets all created roulette
        /// </summary>
        /// <returns></returns>
        public ModeloResponse<Ruleta> GetRuletas()
        {
            try
            {
                using (DataConnectionFactory<Ruleta> dapper = new ConnectionFactory<Ruleta>(AppConfiguration.Instance.MasivianBD).GetConnectionMananager())
                {
                    var list = dapper.GetList(StoreProcedures.SP_GetRuletas);
                    if (list != null && list.Any())
                        return ResponseManager<Ruleta>.ResponseOK(0, list);

                    return ResponseManager<Ruleta>.ResponseNoContent("No se encontraron datos");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<Ruleta>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Login for client
        /// </summary>
        /// <param name="clienteRequest"></param>
        /// <returns></returns>
        public ModeloResponse<ClienteResponse> LoginClient(AuthorizeClient clienteRequest)
        {
            try
            {
                using (DataConnectionFactory<ClienteResponse> dapper = new ConnectionFactory<ClienteResponse>(AppConfiguration.Instance.MasivianBD).GetConnectionMananager())
                {
                    dapper.AddParameter("Usuario", clienteRequest.Usuario);
                    dapper.AddParameter("Contrasena", clienteRequest.Contrasena);
                    var list = dapper.GetList(StoreProcedures.SP_Login);
                    if (list != null && list.Any())
                        return ResponseManager<ClienteResponse>.ResponseOK(0, list);

                    return ResponseManager<ClienteResponse>.ResponseConflict("Usuario o Contrasena incorrecta");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<ClienteResponse>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Make a bet on an open roulette
        /// </summary>
        /// <param name="apuestaRequest"></param>
        /// <returns></returns>
        public ModeloResponse<int> PlaceBet(ApuestaRequest apuestaRequest)
        {
            try
            {
                using (DataConnectionFactory<int> dapper = new ConnectionFactory<int>(AppConfiguration.Instance.MasivianBD).GetConnectionMananager())
                {
                    dapper.AddParameter("Numero", apuestaRequest.Numero);
                    dapper.AddParameter("Color", apuestaRequest.Color);
                    dapper.AddParameter("Dinero", apuestaRequest.Dinero);
                    dapper.AddParameter("ID_Ruleta", apuestaRequest.IdRuleta);
                    dapper.AddParameter("ID_Usuario", apuestaRequest.IdUsuario);
                    var list = dapper.GetList(StoreProcedures.SP_PlaceBet);
                    if (list != null && list.Any())
                        return ResponseManager<int>.ResponseOK(0, list);

                    return ResponseManager<int>.ResponseInternalServerError("Error al realizar apuesta");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<int>.ResponseInternalServerError(ex.Message);
            }
        }
    }
}
