namespace Masivian_Ruleta.Repository.Interface
{
    using MasivianRuleta.Entities.Cliente;
    using MasivianRuleta.Entities.Response;
    using MasivianRuleta.Entities.Ruleta;

    public interface IRuletaRepository
    {
        /// <summary>
        /// Create a new roulette
        /// </summary>
        /// <returns></returns>
        ModeloResponse<int> CreateRuleta();

        /// <summary>
        /// Gets all created roulette
        /// </summary>
        /// <returns></returns>
        ModeloResponse<Ruleta> GetRuletas();

        /// <summary>
        /// Active the roulette
        /// </summary>
        /// <param name="idRuleta"></param>
        /// <returns></returns>
        ModeloResponse<int> ActiveRuletas(int idRuleta);

        /// <summary>
        /// Make a bet on an open roulette
        /// </summary>
        /// <param name="apuestaRequest"></param>
        /// <returns></returns>
        ModeloResponse<int> PlaceBet(ApuestaRequest apuestaRequest);

        /// <summary>
        /// Login for client
        /// </summary>
        /// <param name="clienteRequest"></param>
        /// <returns></returns>
        ModeloResponse<ClienteResponse> LoginClient(AuthorizeClient clienteRequest);

        /// <summary>
        /// Close the bet
        /// </summary>
        /// <param name="cerrarRuletaRequest"></param>
        /// <returns></returns>
        ModeloResponse<CerrarRuletaResponse> CloseBet(CerrarRuletaRequest cerrarRuletaRequest);
    }
}
