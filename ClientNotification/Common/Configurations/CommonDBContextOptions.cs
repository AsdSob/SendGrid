using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;

namespace ClientNotification.Common.Configurations
{
    public class CommonDBContextOptions : ICommonDBContextOptions
    {
        public Assembly ConfigurationAssembly { get; set; }

        public string ConnectionString { get; set; }

        public DBLogOptions LogOptions { get; set; }

        public Action<DbContext, ModelBuilder> OnPreModelCreating { get; set; }
    }
}
