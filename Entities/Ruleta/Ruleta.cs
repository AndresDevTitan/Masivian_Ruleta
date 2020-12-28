namespace MasivianRuleta.Entities.Ruleta
{
    using System;

    /// <summary>
    /// Response model entity - Ruleta
    /// </summary>
    public class Ruleta
    {
        public int ID { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
