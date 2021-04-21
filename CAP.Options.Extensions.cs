// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using DotNetCore.CAP;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class CapOptionsExtensions
    {
        public static CapOptions UseSQLite(this CapOptions options, string connectionString)
        {
            return options.UseSQLite(opt => { opt.ConnectionString = connectionString; });
        }

        public static CapOptions UseSQLite(this CapOptions options, Action<SQLiteOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            configure += x => x.Version = options.Version;

            options.RegisterExtension(new SQLiteCapOptionsExtension(configure));

            return options;
        }

    }
}