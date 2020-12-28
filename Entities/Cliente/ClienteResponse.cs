namespace MasivianRuleta.Entities.Cliente
{
    using System;

    /// <summary>
    /// Response for entity Cliente
    /// </summary>
    public class ClienteResponse
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public Int64 Dinero { get; set; }
    }
}
