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
    public interface IDBC : IDisposable
    {
        Task<DbConnection> OpenDBConnectionAsync();

        Task<DbConnection> OpenDBConnectionAsync(DBConnectionString dBConnectionString);

        Task<DbConnection> OpenDBConnectionAsync(string connectionString);

        DbConnection DbConnection { get; }
    }
}
