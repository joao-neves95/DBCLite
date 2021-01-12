/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using System;

namespace DBCLite.Models
{
    /// <summary>
    ///
    /// Representation of the connection string.
    ///
    /// </summary>
    /// <see cref="https://www.connectionstrings.com/"/>
    public class DBConnectionString
    {
        #region PROPERTIES

        public string ServerAddress { get; set; }

        public string Port { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        #endregion PROPERTIES

        #region METHODS

        #region PostgreSQL

        public string ToPostreSQLConnectionString()
        {
            return $"Server={this.ServerAddress}; Port={this.Port}; Database={this.Database}; User Id={this.User}; Password={this.Password};";
        }

        public string BuildPostreSQLConnectionStringWithSSL()
        {
            return this.ToPostreSQLConnectionString() + " SslMode=Require;";
        }

        #endregion PostgreSQL

        #region MySQL

        public string ToMySQLConnectionString()
        {
            return $"Server={this.ServerAddress}; Database={this.Database}; Uid={this.User}; Pwd={this.Password};";
        }

        public string ToMySQLConnectionStringWithSSL()
        {
            return this.ToMySQLConnectionString() + " SslMode=Required;";
        }

        #endregion MySQL

        #region SQLServer

        public string ToSQLServerConnectionString()
        {
            return $@"
                Server={
                    this.ServerAddress +
                    (
                        !String.IsNullOrEmpty(this.Port) ? $",{this.Port}" : ""
                    )
                }; Database={this.Database}; User Id={this.User}; Password={this.Password};";
        }

        public string ToSQLServerTrustedConnectionString()
        {
            return this.ToSQLServerConnectionString() + " Trusted_Connection=True;";
        }

        public string ToSQLServerIPConnectionString()
        {
            return $"Data Source={this.ServerAddress}; Network Library=DBMSSOCN; Initial Catalog={this.Database}; User ID={this.User}; Password={this.Password};";
        }

        #endregion SQLServer

        #endregion METHODS
    }
}
