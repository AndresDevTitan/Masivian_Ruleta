using MasivianRuleta.Entities.Cliente;
using System;

namespace MasivianRuleta.Entities.Ruleta
{
    /// <summary>
    /// Request for entity Apuesta
    /// </summary>
    public class ApuestaRequest
    {
        public int Numero { get; set; }
        public string Color { get; set; }
        public Int64 Dinero { get; set; }
        public int IdRuleta { get; set; }
        public int IdUsuario { get; set; }
        public AuthorizeClient AuthorizeClient { get; set; } = new AuthorizeClient();
    }
}
