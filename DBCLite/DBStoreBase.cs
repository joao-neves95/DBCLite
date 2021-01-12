/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System;

namespace DBCLite
{
    public abstract class DBStoreBase
    {
        protected readonly IDBC _dBContext;

        public DBStoreBase(IDBC dBContext)
        {
            this._dBContext = dBContext;
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this._dBContext.Dispose();
                disposedValue = true;
            }
        }

        ~DBStoreBase()
        {
            Dispose( false );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion IDisposable Support
    }
}
