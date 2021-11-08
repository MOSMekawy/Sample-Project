using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Simplified.Exceptions;
using System.Threading.Tasks;

namespace Simplified
{
    public class StoredProcedure<TEntity>
    {
        private readonly string ConnectionString;
        private SqlConnection ContextConnection;
        private SqlCommand CurrentCommand;
        private SqlDataReader Reader;
        private dynamic ReturnValue;

        public StoredProcedure(string connection_string)
        {
            this.ConnectionString = connection_string;
        }

        public StoredProcedure<TEntity> Define(string procedure_name)
        {
            ContextConnection = new SqlConnection(ConnectionString);
            CurrentCommand = new SqlCommand(procedure_name, ContextConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            Reader = null;

            return this;
        }

        public StoredProcedure<TEntity> Apply(SqlParameter parameter)
        {
            if (CurrentCommand == null) throw new InvalidChainOrderException();

            CurrentCommand.Parameters.Add(parameter);

            return this;
        }

        public StoredProcedure<TEntity> Apply(dynamic[,] parameters)
        {

            if (CurrentCommand == null) throw new InvalidChainOrderException();

            for (int i = 0; i < parameters.GetLength(0); i++)
            {
                CurrentCommand.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
            }

            return this;
        }

        public dynamic ExecuteNonQuery()
        {
            if (ContextConnection == null || CurrentCommand == null) throw new InvalidChainOrderException();

            ContextConnection.Open();
        
            int affected = CurrentCommand.ExecuteNonQuery();

            this.Terminate();

            return affected;
        }

        public StoredProcedure<TEntity> ExecuteReader()
        {
            if (ContextConnection == null || CurrentCommand == null) throw new InvalidChainOrderException();

            ContextConnection.Open();
            Reader = CurrentCommand.ExecuteReader();

            return this;
        }

        public TEntity Map(Func<SqlDataReader, TEntity> map)
        {
            if (Reader == null) throw new InvalidChainOrderException();

            TEntity Retrieved = default;

            if (Reader.Read())
            {
                Retrieved = map(Reader);
            }

            this.Terminate();

            return Retrieved;
        }

        public List<TEntity> MultiMap(Func<SqlDataReader, TEntity> map)
        {
            if (Reader == null) throw new InvalidChainOrderException();

            List<TEntity> Retrieved = new List<TEntity>();

            while (Reader.Read())
            {
                Retrieved.Add(map(Reader));
            }

            this.Terminate();

            return Retrieved;
        }

        public void Terminate()
        {
            Reader?.DisposeAsync();
            ContextConnection.Close();
            CurrentCommand.Dispose();
        }
    }
}
