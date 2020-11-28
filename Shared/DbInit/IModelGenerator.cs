using Microsoft.EntityFrameworkCore;

namespace Shared.DbInit
{
    public interface IModelGenerator
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }
}