
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

using DBCLite.Models;

namespace DBCLite.SQLServer
{
    public class SQLServerDBC : DBContextBase
    {
        #region CONSTRUCTORS

        public SQLServerDBC()
        {
        }

        public SQLServerDBC(string connectionString) : base(connectionString)
        {
        }

        #endregion CONSTRUCTORS

        public override async Task<DbConnection> OpenDBConnectionAsync()
        {
            return await this.OpenDBConnectionAsync( this._connectionString );
        }

        public override async Task<DbConnection> OpenDBConnectionAsync(DBConnectionString dBConnectionString)
        {
            return await this.OpenDBConnectionAsync( dBConnectionString.ToSQLServerConnectionString() );
        }

        public override async Task<DbConnection> OpenDBConnectionAsync(string connectionString)
        {
            base._DbConnection = new SqlConnection( connectionString );
            base._connectionString = connectionString;
            await base._DbConnection.OpenAsync().ConfigureAwait( false );
            this._DbConnection.Disposed += base.DisposedEventHandler;

            return base._DbConnection;
        }
    }
}
