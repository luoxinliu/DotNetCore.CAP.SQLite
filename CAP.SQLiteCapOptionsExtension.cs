// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using DotNetCore.CAP.Persistence;
using DotNetCore.CAP.SQLite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace DotNetCore.CAP
{
    internal class SQLiteCapOptionsExtension : ICapOptionsExtension
    {
        private readonly Action<SQLiteOptions> _configure;

        public SQLiteCapOptionsExtension(Action<SQLiteOptions> configure)
        {
            _configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<CapStorageMarkerService>();
            services.AddSingleton<IDataStorage, SQLiteDataStorage>();
            
            services.TryAddSingleton<IStorageInitializer, SQLiteStorageInitializer>();
            services.AddTransient<ICapTransaction, SQLiteCapTransaction>();

            //Add SQLiteOptions
            services.Configure(_configure);
            services.AddSingleton<IConfigureOptions<SQLiteOptions>, ConfigureSQLiteOptions>();
        } 
    }
}