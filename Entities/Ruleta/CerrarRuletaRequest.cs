namespace MasivianRuleta.Entities.Ruleta
{
    /// <summary>
    /// Request for Entity CloseBet
    /// </summary>
    public class CerrarRuletaRequest
    {
        public int IdRuleta { get; set; }
        public int NumeroGanador { get; set; }
    }
}
