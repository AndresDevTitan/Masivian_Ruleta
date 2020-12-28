namespace MasivianApi.Controllers
{
    using Masivian_Ruleta.Business.Interface;
    using MasivianRuleta.Entities.Cliente;
    using MasivianRuleta.Entities.Ruleta;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for main roulette functionality
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RuletaController : ControllerBase
    {
        private readonly IRuletaBusiness managerBusiness;

        public RuletaController(IRuletaBusiness managerBusiness)
        {
            this.managerBusiness = managerBusiness;
        }

        /// <summary>
        /// Create a new roulette
        /// </summary>
        /// <returns>ModeloResponse</returns>
        /// <response code="200">Succesfully</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("[action]")]
        [Produces("application/json")]
        [ActionName("CreateRuleta")]
        public IActionResult CreateRuleta()
        {
            var response = managerBusiness.CreateRuleta();
            return Ok(response);
        }

        /// <summary>
        /// Gets all created roulette
        /// </summary>
        /// <returns>ModeloResponse</returns>
        /// <response code="200">Succesfully</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("[action]")]
        [Produces("application/json")]
        [ActionName("GetRuletas")]
        public IActionResult GetRuletas()
        {
            var response = managerBusiness.GetRuletas();
            return Ok(response);
        }

        /// <summary>
        /// Active the roulette
        /// </summary>
        /// <returns>ModeloResponse</returns>
        /// <response code="200">Succesfully</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("[action]")]
        [Produces("application/json")]
        [ActionName("ActiveRuleta")]
        public IActionResult ActiveRuleta(int idRuleta)
        {
            var response = managerBusiness.ActiveRuletas(idRuleta);
            return Ok(response);
        }

        /// <summary>
        /// Place the bet
        /// </summary>
        /// <returns>ModeloResponse</returns>
        /// <response code="200">Succesfully</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("[action]")]
        [Produces("application/json", Type = typeof(ApuestaRequest))]
        [ActionName("PlaceBet")]
        public IActionResult PlaceBet([FromBody] ApuestaRequest apuestaRequest)
        {
            var response = managerBusiness.PlaceBet(apuestaRequest);
            return Ok(response);
        }

        /// <summary>
        /// Close bet
        /// </summary>
        /// <returns>ModeloResponse</returns>
        /// <response code="200">Succesfully</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("[action]")]
        [Produces("application/json")]
        [ActionName("CloseBet")]
        public IActionResult CloseBet(int idRuleta)
        {
            var response = managerBusiness.CloseBet(idRuleta);
            return Ok(response);
        }
    }
}
