namespace MasivianRuleta.DataAccess
{
    using Dapper;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;

    public class DataConnectionFactoryBase<T> : DataConnectionFactory<T>
    {
        public DataConnectionFactoryBase(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Add parameter to request
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pValue"></param>
        public override void AddParameter(string pName, object pValue)
        {
            parameters = parameters ?? new DynamicParameters();
            (this.parameters as DynamicParameters).Add(name: pName, value: pValue, direction: ParameterDirection.Input);
        }

        /// <summary>
        /// Execute store procedures
        /// </summary>
        /// <param name="pStoreProcedure"></param>
        public override int Execute(string pStoreProcedure)
        {
            this.OpenConnection();
            var rowsAffected = connection.Execute(sql: pStoreProcedure, param: this.parameters, commandType: CommandType.StoredProcedure);
            this.Dispose();
            
            return rowsAffected;
        }

        public override Collection<T> GetList(string pSotreProcedure)
        {
            this.OpenConnection();
            if(parameters == null)
            {
                parameters = new DynamicParameters();
            }
            var response = new ObservableCollection<T>(collection: connection.Query<T>(sql: pSotreProcedure, param: parameters, commandType: CommandType.StoredProcedure, commandTimeout: 30));
            this.Dispose();

            return response;
        }
    }
}
