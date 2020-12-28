namespace MasivianRuleta.Entities
{
    /// <summary>
    /// Container class for stored procedures
    /// </summary>
    public static class StoreProcedures
    {
        public static readonly string SP_CreateRuleta = "SP_INS_Ruleta";
        public static readonly string SP_GetRuletas = "SP_QRY_ConsultarRuletas";
        public static readonly string SP_ActiveRuletas = "SP_UPD_AbrirRuleta";
        public static readonly string SP_Login = "SP_OPE_Login";
        public static readonly string SP_PlaceBet = "SP_INS_RealizarApuesta";
        public static readonly string SP_CloseBet = "SP_OPE_CerrarRuleta";
    }
}
