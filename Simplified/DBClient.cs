using System;
using System.Data;
using System.Data.SqlClient;
using Simplified.Exceptions;
using System.Collections.Generic;

namespace Simplified
{
    public class DBClient<TEntity>
    {
        private readonly string ConnectionString;
        private StoredProcedure<TEntity> SP;

        public DBClient(string ConnectionString) {
            this.ConnectionString = ConnectionString;
        }

        public StoredProcedure<TEntity> StoredProcedure()
        { 
            if (SP == null)
             SP = new StoredProcedure<TEntity>(ConnectionString);

            return SP;
        }
    }
}
