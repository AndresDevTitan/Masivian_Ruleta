namespace MasivianRuleta.DataAccess
{
    using Dapper;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.Common;
    using static Dapper.SqlMapper;

    /// <summary>
    /// Abstract class for data connection
    /// </summary>
    public abstract class DataConnectionFactory<T> : IDisposable
    {
        internal DbConnection connection;
        internal IDynamicParameters parameters;

        /// <summary>
        /// Add parameters to the execution
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pValue"></param>
        public abstract void AddParameter(string pName, object pValue);

        /// <summary>
        /// Execute the sentence
        /// </summary>
        /// <param name="pStoreProcedure"></param>
        public abstract int Execute(string pStoreProcedure);

        /// <summary>
        /// Get the results of a query
        /// </summary>
        /// <param name="pSotreProcedure"></param>
        /// <returns></returns>
        public abstract Collection<T> GetList(string pSotreProcedure);

        /// <summary>
        /// Open the connection 
        /// </summary>
        /// <returns></returns>
        internal bool OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                parameters = parameters ?? new DynamicParameters();
            }

            return true;
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public void Dispose()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            parameters = null;
        }
    }
}
