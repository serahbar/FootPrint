using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FootPrint.Infrastructure.EfCoreDAL.Extentions
{
    public static class DbSetExtention
    {
        public static DbContext GetDbContext<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
            var serviceProvider = infrastructure.Instance;
            var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext))
            as ICurrentDbContext;
            return currentDbContext.Context;
        }
        public static int Clear<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var dbContext = dbSet.GetDbContext();
            var relationalType = dbContext.Model.FindEntityType(typeof(TEntity));
            var schema = relationalType.GetSchema;
            var tableName = relationalType.GetTableName;
            string deleteCommand = $"DELET {schema}.{tableName}";
            var result=dbContext.Database.ExecuteSqlRaw(deleteCommand);
            return result;
        }
    }

}
