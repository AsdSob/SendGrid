using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;

namespace ClientNotification.Common.Configurations
{
    public interface ICommonDBContextOptions
    {
        Assembly ConfigurationAssembly { get; }

        string ConnectionString { get; }

        /// <summary>
        /// If Null then log disabled
        /// </summary>
        DBLogOptions LogOptions { get; }

        Action<DbContext, ModelBuilder> OnPreModelCreating { get; }
    }
}
