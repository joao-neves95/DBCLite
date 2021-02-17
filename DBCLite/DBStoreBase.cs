/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System;

using Dapper;

namespace DBCLite
{
    public abstract class DBStoreBase : IDisposable
    {
        protected DBStoreBase(IDBContext dBContext)
        {
            this.DbContext = dBContext;
        }

        #region IDisposable Support

        protected readonly IDBContext DbContext;

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this.DbContext.Dispose();
                this.disposedValue = true;
            }
        }

        ~DBStoreBase()
        {
            this.Dispose( false );
        }

        public void Dispose()
        {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion IDisposable Support
    }
}
