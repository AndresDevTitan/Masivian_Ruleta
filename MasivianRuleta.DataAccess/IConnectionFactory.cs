namespace MasivianRuleta.DataAccess
{
    interface IConnectionFactory<T>
    {
        /// <summary>
        /// Get connection of database 
        /// </summary>
        /// <returns></returns>
        DataConnectionFactory<T> GetConnectionMananager();
    }
}
