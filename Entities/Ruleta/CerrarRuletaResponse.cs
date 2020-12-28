namespace MasivianRuleta.Entities.Ruleta
{
    using System;

    /// <summary>
    /// Response for Entity CloseBet
    /// </summary>
    public class CerrarRuletaResponse
    {
        public string Usuario { get; set; }
        public int NumeroGanador { get; set; }
        public Int64 Recompensa { get; set; }
    }
}
