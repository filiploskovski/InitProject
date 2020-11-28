using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shared.DbInit
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //Add Code here
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var interfaceType = typeof(IModelGenerator);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()).Where(p => p.IsClass && interfaceType.IsAssignableFrom(p));

            foreach (var type in types)
            {
                IModelGenerator instance = (IModelGenerator)Activator.CreateInstance(type);
                instance?.OnModelCreating(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}