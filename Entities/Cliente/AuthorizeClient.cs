namespace MasivianRuleta.Entities.Cliente
{
    /// <summary>
    /// Request for entity Cliente - Authentication
    /// </summary>
    public class AuthorizeClient
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}
