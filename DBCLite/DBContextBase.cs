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
            if (!_disposed && this._DbConnection != null && this._DbConnection.State != ConnectionState.Closed)
            {
                if (this._DbConnection.State == ConnectionState.Open)
                {
                    this._DbConnection.Close();
                }

                if (this._DbConnection.State != ConnectionState.Connecting &&
                    this._DbConnection.State != ConnectionState.Executing &&
                    this._DbConnection.State != ConnectionState.Fetching
                )
                {
                    this._DbConnection.Dispose();
                }

                this._DbConnection = null;
                _disposed = true;
            }
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
