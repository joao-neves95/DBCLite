/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System;
using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using DBCLite.Models;

namespace DBCLite.PostgreSQL
{
    public class PostgresDBC : DBCBase, IDBC
    {
        #region CONTRUCTORS

        public PostgresDBC()
        {
        }

        public PostgresDBC(string connectionString) : base( connectionString )
        {
        }

        #endregion CONTRUCTORS

        #region DB CONNECTION

        public async Task<DbConnection> OpenDBConnectionAsync()
        {
            return await this.OpenDBConnectionAsync( this._connectionString );
        }

        public async Task<DbConnection> OpenDBConnectionAsync(DBConnectionString dBConnectionString)
        {
            return await this.OpenDBConnectionAsync( dBConnectionString.ToPostreSQLConnectionString() );
        }

        public async Task<DbConnection> OpenDBConnectionAsync(string connectionString)
        {
            try
            {
                base._dbConnection = new NpgsqlConnection( connectionString );
                base._connectionString = connectionString;
                await base._dbConnection.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
                throw;
            }

            return base._dbConnection;
        }

        #endregion DB CONNECTION
    }
}
