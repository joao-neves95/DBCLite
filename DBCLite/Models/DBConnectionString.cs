/*
 * Copyright (c) 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 * https://github.com/joao-neves95
 *
 * DBCLite and all its content are licensed under the GNU AFFERO GENERAL PUBLIC LICENSE v3.0
 * (AGPL-3.0), located in the root folder, under the name "LICENSE.md".
 *
 */

using DBCLite.Constants;

using System;

namespace DBCLite.Models
{
    /// <summary>
    /// Representation of the connection string.
    ///
    /// </summary>
    /// <see cref="https://www.connectionstrings.com/"/>
    public class DBConnectionString
    {
        #region CONSTRUCTORS

        public DBConnectionString()
        {
        }

        public DBConnectionString(string serverAddress, string port, string database, string user, string password)
        {
            this.Init( serverAddress, port, database, user, password );
        }

        public DBConnectionString(string serverAddress, string database, string user, string password)
        {
            this.Init( serverAddress, null, database, user, password );
        }

        private void Init(string serverAddress, string port, string database, string user, string password)
        {
            this.ServerAddress = serverAddress;
            this.Port = port;
            this.Database = database;
            this.User = user;
            this.Password = password;
        }

        #endregion CONSTRUCTORS

        #region PROPERTIES

        public string ServerAddress { get; set; }

        /// <summary>
        /// If <see langword="null"/>, the <see cref="DefaultDbPorts"/> are used.
        ///
        /// </summary>
        public string Port { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        #endregion PROPERTIES

        #region METHODS

        #region PostgreSQL - Npgsql

        public string ToNpgsqlConnectionString()
        {
            if (string.IsNullOrEmpty( this.Port ))
            {
                this.Port = DefaultDbPorts.POSTGRESQL;
            }

            return $"Server={this.ServerAddress}; Port={this.Port}; Database={this.Database}; User Id={this.User}; Password={this.Password};";
        }

        public string ToNpgsqlConnectionStringWithoutSSL()
        {
            return this.ToNpgsqlConnectionString() + this.NpgsqlWithoutSSL();
        }

        public string ToNpgsqlConnectionStringWithSSL()
        {
            return this.ToNpgsqlConnectionString() + this.NpgsqlWithSSL();
        }

        public string ToNpgsqlConnectionStringWithPoolingControl(int maxPoolSize, bool ssl = true, int minPoolSize = 1)
        {
            return (ssl ? this.ToNpgsqlConnectionStringWithSSL() : this.ToNpgsqlConnectionStringWithoutSSL()) +
                   $" Pooling=true; MinPoolSize={minPoolSize}; MaxPoolSize={maxPoolSize};";
        }

        private string NpgsqlWithSSL()
        {
            return " Protocol=3; SSL=true; SslMode=Require;";
        }

        private string NpgsqlWithoutSSL()
        {
            return " Protocol=3; SSL=false; SslMode=Disable;";
        }

        #endregion PostgreSQL - Npgsql

        #region MySQL

        /// <summary>
        /// NOT TESTED.
        ///
        /// </summary>
        /// <returns></returns>
        public string ToMySQLConnectionString()
        {
            if (string.IsNullOrEmpty( this.Port ))
            {
                this.Port = DefaultDbPorts.MYSQL;
            }

            return $"Server={this.ServerAddress}; Database={this.Database}; Uid={this.User}; Pwd={this.Password};";
        }

        /// <summary>
        /// NOT TESTED.
        ///
        /// </summary>
        /// <returns></returns>
        public string ToMySQLConnectionStringWithSSL()
        {
            return this.ToMySQLConnectionString() + " SslMode=Required;";
        }

        #endregion MySQL

        #region SQLServer

        public string ToSQLServerConnectionString()
        {
            if (string.IsNullOrEmpty( this.Port ))
            {
                this.Port = DefaultDbPorts.SQL_SERVER;
            }

            return $@"
                Server={
                    this.ServerAddress +
                    (
                        !String.IsNullOrEmpty( this.Port ) ? $",{this.Port}" : ""
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
