/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

using Dapper;

using DBCLite.Models;

namespace DBCLite
{
    public abstract class DBContextBase : IDBContext
    {
        protected DBContextBase()
        {
        }

        protected DBContextBase(string connectionString)
        {
            this._connectionString = connectionString;
        }

        #region PROPERTIES

        protected void DisposedEventHandler(object? sender, EventArgs e)
        {
            this._disposed = true;
        }

        internal protected string _connectionString;

        internal protected DbConnection _DbConnection;

        public DbConnection DbConnection
        {
            get
            {
                return this._DbConnection;
            }
        }

        #endregion PROPERTIES

        #region IDisposable Support

        private bool _disposed = false;

        ~DBContextBase()
        {
            Dispose( false );
        }

        public void Dispose()
        {
            Dispose( false );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed || this._DbConnection == null)
            {
                return;
            }

            if (this._DbConnection.State != ConnectionState.Closed)
            {
                this._DbConnection.Close();
            }

            this._DbConnection.Dispose();

            this._DbConnection = null;
            _disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (this._disposed || this._DbConnection == null)
            {
                return;
            }

            if (this._DbConnection.State != ConnectionState.Closed)
            {
                await this._DbConnection.CloseAsync();
            }

            await this._DbConnection.DisposeAsync();

            this._DbConnection = null;
            _disposed = true;
        }

        #endregion IDisposable Support

        public void SetConnectionString(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public abstract Task<DbConnection> OpenDBConnectionAsync();

        public abstract Task<DbConnection> OpenDBConnectionAsync(DBConnectionString dBConnectionString);

        public abstract Task<DbConnection> OpenDBConnectionAsync(string connectionString);
    }
}
