namespace MasivianRuleta.Entities
{
    /// <summary>
    /// Configuration class
    /// </summary>
    public class AppConfiguration
    {
        public string MasivianBD { get; set; }

        /// <summary>
        /// Instance of class
        /// </summary>
        public static AppConfiguration Instance { get; } = new AppConfiguration();
    }
}
