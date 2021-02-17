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
using DBCLite.Models;

namespace DBCLite
{
    public interface IDBContext : IDisposable
    {
        DbConnection DbConnection { get; }

        void SetConnectionString(string connectionString);

        /// <summary>
        /// Opens the inner <see cref="DbConnection"/> using the already existing inner connection string.
        ///
        /// </summary>
        /// <returns></returns>
        Task<DbConnection> OpenDBConnectionAsync();

        /// <summary>
        /// Opens the inner <see cref="DbConnection"/> using the provided connection model.
        ///
        /// </summary>
        /// <param name="dBConnectionString"></param>
        Task<DbConnection> OpenDBConnectionAsync(DBConnectionString dBConnectionString);

        /// <summary>
        /// Opens the inner <see cref="DbConnection"/> using the provided connection string.
        ///
        /// </summary>
        /// <param name="connectionString"> Use <see cref="DBConnectionString"/> </param>
        /// <returns></returns>
        Task<DbConnection> OpenDBConnectionAsync(string connectionString);
    }
}
