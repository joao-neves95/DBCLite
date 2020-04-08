/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * DBCLite and all its content are licensed under the GNU Lesser General Public License v3.0
 * (LGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System;
using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using DBCLite.Models;

namespace DBCLite.Providers
{
    public class PostgreDBC : DBCBase, IDBC
    {
        #region CONTRUCTORS

        public PostgreDBC()
        {
        }

        public PostgreDBC(string connectionString) : base( connectionString )
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
