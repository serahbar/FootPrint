using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FootPrint.Infrastructure.EfCoreDAL.Extentions
{
    public static partial class DbContextExtentions
    {

        public static int Clear<TEntity>(this DbContext dbContext) where TEntity : class
        {
            //return dbContext.ContainsEntity<TEntity>() ?
            //dbContext.Set<TEntity>().Clear() :
            // 0;
            return 0;

        }
        public static bool ContainsEntity<TEntity>(this DbContext dbContext) where TEntity : class
        {
            return dbContext.Model.FindEntityType(typeof(TEntity)) != null;

        }

        public static IEnumerable<EntityEntry> GetChangedEntities(this DbContext dbContext, EntityState? entityState = null)
        {
            var entries = dbContext.ChangeTracker.Entries();
            if (entityState.HasValue)
                entries.Where(c => c.State == entityState.Value);
            return entries;
        }
        public static IEnumerable<EntityEntry> GetAddedEntities(this DbContext dbContext)
        {
            return dbContext.GetChangedEntities(EntityState.Added);
        }
        public static IEnumerable<EntityEntry> GetModifiedEntities(this DbContext dbContext)
        {

            return dbContext.GetChangedEntities(EntityState.Modified);
        }
        public static IEnumerable<EntityEntry> GetAddedOrModifiedEntities(this DbContext dbContext)
        {
            var entries = dbContext.GetChangedEntities()
            .Where(c => c.State == EntityState.Added || c.State == EntityState.Modified);
            return entries;
        }
        public static IEnumerable<EntityEntry> GetDeletedEntities(this DbContext dbContext)
        {

            return dbContext.GetChangedEntities(EntityState.Deleted);
        }
    }
}
