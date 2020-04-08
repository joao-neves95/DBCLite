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
