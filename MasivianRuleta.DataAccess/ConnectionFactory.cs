namespace MasivianRuleta.DataAccess
{
    public class ConnectionFactory<T> : IConnectionFactory<T>
    {
        private readonly string connectionString;

        public ConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Get connection with DataConnectionFactoryBase
        /// </summary>
        /// <returns></returns>
        public DataConnectionFactory<T> GetConnectionMananager()
        {
            return new DataConnectionFactoryBase<T>(connectionString);
        }
    }
}
