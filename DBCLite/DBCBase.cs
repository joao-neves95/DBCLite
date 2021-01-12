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

namespace DBCLite
{
    public abstract class DBCBase : IDisposable
    {
        protected DBCBase()
        {
        }

        protected DBCBase(string connectionString)
        {
            this._connectionString = connectionString;
        }

        internal protected string _connectionString;

        internal protected DbConnection _dbConnection;

        public DbConnection DbConnection
        {
            get
            {
                return this._dbConnection;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && this._dbConnection != null)
            {
                if (this._dbConnection.State == ConnectionState.Open)
                {
                    this._dbConnection.Close();
                }

                if (this._dbConnection.State != ConnectionState.Connecting &&
                    this._dbConnection.State != ConnectionState.Executing &&
                    this._dbConnection.State != ConnectionState.Fetching
                )
                {
                    this._dbConnection.Dispose();
                }

                this._dbConnection = null;
                disposedValue = true;
            }
        }

        ~DBCBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}
