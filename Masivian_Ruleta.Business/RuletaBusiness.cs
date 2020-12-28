namespace Masivian_Ruleta.Business
{
    using Masivian_Ruleta.Business.Interface;
    using Masivian_Ruleta.Repository.Interface;
    using MasivianRuleta.Entities.Cliente;
    using MasivianRuleta.Entities.Response;
    using MasivianRuleta.Entities.Ruleta;
    using Serilog;
    using System;
    using System.Linq;

    public class RuletaBusiness : IRuletaBusiness
    {
        private readonly IRuletaRepository managerRepository;
        private readonly int maxNumber = 36;
        private readonly int minNumber = 0;
        private readonly int maxBet = 10000;

        public RuletaBusiness(IRuletaRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        /// <summary>
        /// Validate the bet restrictions
        /// </summary>
        /// <param name="apuestaRequest"></param>
        private ModeloResponse<string> ValidateBet(ApuestaRequest apuestaRequest, ModeloResponse<ClienteResponse> authorizeClient)
        {
            if (authorizeClient.Data[0].Dinero < apuestaRequest.Dinero)
                return ResponseManager<string>.ResponseConflict("No posee el dinero suficiente, para realizar la apuesta");
            if (apuestaRequest.Numero != null)
                if (apuestaRequest.Numero < minNumber || apuestaRequest.Numero > maxNumber)
                    return ResponseManager<string>.ResponseConflict($"El rango de apuesta es entre {minNumber} y {maxNumber}");
            if (apuestaRequest.Color != "" && (apuestaRequest.Color.ToLower() != "negro" && apuestaRequest.Color.ToLower() != "rojo"))
                return ResponseManager<string>.ResponseConflict("Solo puede seleccionar rojo o negro");
            if (apuestaRequest.Dinero > maxBet)
                return ResponseManager<string>.ResponseConflict($"La apuesta no puede superar el monto maximo: {maxBet}");

            return ResponseManager<string>.ResponseOK();
        }

        /// <summary>
        /// Validate the roulette status
        /// </summary>
        /// <param name="apuestaRequest"></param>
        private ModeloResponse<string> ValidateRoulette(int idRoulette)
        {
            var response = managerRepository.GetRuletas();
            if (response.CodigoRespuesta != System.Net.HttpStatusCode.OK)
                return ResponseManager<string>.ResponseConflict(response.Mensaje);
            if(response.Data.Where(x => x.ID == idRoulette).First().Estado == "CERRADA")
                return ResponseManager<string>.ResponseConflict("La ruleta se encuentra cerrada");

            return ResponseManager<string>.ResponseOK();
        }

        /// <summary>
        /// Active the roulette
        /// </summary>
        /// <param name="idRuleta"></param>
        /// <returns></returns>
        public ModeloResponse<int> ActiveRuletas(int idRuleta)
        {
            try
            {
                var response = managerRepository.ActiveRuletas(idRuleta);
                if (response.Data[0] == 0)
                    ResponseManager<int>.ResponseConflict("No fue posible activar la ruleta");

                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<int>.ResponseInternalServerError(ex.Message);
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
                return managerRepository.CreateRuleta();
            }
            catch (Exception ex)
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
                return managerRepository.GetRuletas();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<Ruleta>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Make a bet on an open roulette
        /// </summary>
        /// <param name="apuestaRequest"></param>
        /// <returns></returns>
        public ModeloResponse<string> PlaceBet(ApuestaRequest apuestaRequest)
        {
            try
            {
                var authenticationClient = managerRepository.LoginClient(apuestaRequest.AuthorizeClient);
                if (authenticationClient.CodigoRespuesta != System.Net.HttpStatusCode.OK)
                    return ResponseManager<string>.ResponseConflict(authenticationClient.Mensaje);
                apuestaRequest.IdUsuario = authenticationClient.Data[0].Id;
                var responseValidateBet = ValidateBet(apuestaRequest, authenticationClient);
                if (responseValidateBet.CodigoRespuesta != System.Net.HttpStatusCode.OK)
                     return ResponseManager<string>.ResponseConflict(responseValidateBet.Mensaje);
                var responseValidateRoulette = ValidateRoulette(apuestaRequest.IdRuleta);
                if (responseValidateRoulette.CodigoRespuesta != System.Net.HttpStatusCode.OK)
                    return ResponseManager<string>.ResponseConflict(responseValidateRoulette.Mensaje);
                var response = managerRepository.PlaceBet(apuestaRequest);
                if(response.CodigoRespuesta != System.Net.HttpStatusCode.OK)
                    return ResponseManager<string>.ResponseConflict(response.Mensaje);

                return ResponseManager<string>.ResponseOK();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<string>.ResponseInternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// Close the bet
        /// </summary>
        /// <param name="idRuleta"></param>
        /// <returns></returns>
        public ModeloResponse<CerrarRuletaResponse> CloseBet(int idRuleta)
        {
            try
            {
                var responseValidateRoulette = ValidateRoulette(idRuleta);
                if (responseValidateRoulette.CodigoRespuesta != System.Net.HttpStatusCode.OK)
                    return ResponseManager<CerrarRuletaResponse>.ResponseConflict(responseValidateRoulette.Mensaje);
                Random random = new Random();
                int numeroGanador = random.Next(0, 36);
                CerrarRuletaRequest cerrarRuletaRequest = new CerrarRuletaRequest()
                {
                    IdRuleta = idRuleta,
                    NumeroGanador = numeroGanador
                };

                return managerRepository.CloseBet(cerrarRuletaRequest);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return ResponseManager<CerrarRuletaResponse>.ResponseInternalServerError(ex.Message);
            }
        }
    }
}
