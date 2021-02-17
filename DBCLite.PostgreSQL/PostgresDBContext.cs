/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System.Threading.Tasks;
using System.Data.Common;

using Npgsql;

using DBCLite.Models;

namespace DBCLite.PostgreSQL
{
    /// <summary>
    /// Implementation of a PostgreSQL DBContext, using Npgsql.
    ///
    /// </summary>
    public class PostgresDBContext : DBContextBase
    {
        #region CONTRUCTORS

        public PostgresDBContext()
        {
        }

        public PostgresDBContext(string connectionString) : base( connectionString )
        {
        }

        #endregion CONTRUCTORS

        #region DB CONNECTION

        public override async Task<DbConnection> OpenDBConnectionAsync()
        {
            return await this.OpenDBConnectionAsync( this._connectionString ).ConfigureAwait( false );
        }

        public override async Task<DbConnection> OpenDBConnectionAsync(DBConnectionString dBConnectionString)
        {
            return await this.OpenDBConnectionAsync( dBConnectionString.ToNpgsqlConnectionString() ).ConfigureAwait( false );
        }

        public override async Task<DbConnection> OpenDBConnectionAsync(string connectionString)
        {
            base._DbConnection = new NpgsqlConnection( connectionString );
            base._connectionString = connectionString;
            await base._DbConnection.OpenAsync().ConfigureAwait( false );
            this._DbConnection.Disposed += base.DisposedEventHandler;

            return base._DbConnection;
        }

        #endregion DB CONNECTION
    }
}
