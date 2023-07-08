using FootPrint.Domain;
using FootPrint.Infrastructure.EfCoreDAL;
using FootPrint.Infrastructure.EfCoreDAL.Config;
using FootPrint.Infrastructure.EfCoreDAL.Extentions;
using FootPrint.Infrastructure.EfCoreDAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Reflection;

public class FootPrintDbContext : DbContext
{
    private readonly IUserProvider _userProvider;
    private readonly ILogProvider<FootPrintLogDbContext> _logProvider;
    private readonly IList<EntityEntry> _addedEntities;
    public FootPrintDbContext(IUserProvider userProvider, ILogProvider<FootPrintLogDbContext> logProvider)
    {
        _userProvider = userProvider;
        _logProvider = logProvider;
        _addedEntities= new List<EntityEntry>();
    }
    public DbSet<EntityToAudit> entityToAudits { get; set; }
    public DbSet<OtherEntityToAudit> otherentityToAudits { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=.;initial Catalog=FootPrintDb; integrated security=true; Encrypt=false");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var auditableEntities = modelBuilder.Model.GetEntityTypes().Where(e => typeof(IAutitable).IsAssignableFrom(e.ClrType));
        foreach (var item in auditableEntities)
        {

            modelBuilder.Entity(item.ClrType).Property<int>("CreateBy");
            modelBuilder.Entity(item.ClrType).Property<DateTime>("CreateTime");
        }
        modelBuilder.ApplyConfiguration(new EntityToAuditConfig());
        modelBuilder.ApplyConfiguration(new OtherEntityToAuditConfig());
    }

    //TODO: Override all SaveChanges
    public override int SaveChanges()
    {
        BeforSaveChanges();
        var resualt = base.SaveChanges();
        AfterSaveChanges();
        return resualt;
    }
    private void AfterSaveChanges()
    {
        AuditingAddedEntities(_addedEntities);
    }
    private void BeforSaveChanges()
    {
        SetYeke();
        Auditing();
    }
    private void SetYeke()
    {
        var addedOrUpdated = this.GetAddedOrModifiedEntities();
        foreach (var item in addedOrUpdated)
        {
            var entity = item.Entity;
            if (item.Entity == null)
            {
                continue;
            }
            var propertyInfos = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));
            foreach (var propertyInfo in propertyInfos)
            {
                var propName = propertyInfo.Name;
                var value = propertyInfo.GetValue(entity);
                if (value != null)
                {
                    var strValue = value.ToString();
                    var newVal = strValue.SetPersianYeke();
                    if (newVal != strValue)
                    {

                        propertyInfo.SetValue(entity, newVal);
                    }
                }
            }
        }
    }
    private void Auditing()
    {
        var entities = ChangeTracker.Entries().Where(c => typeof(IAutitable).IsAssignableFrom(c.Entity.GetType()));
        foreach (var item in entities)
        {
            var temp = item.Entity as IAutitable;
            if (item.State == EntityState.Added)
            {
                item.Property("CreateBy").CurrentValue = _userProvider.getUserId();
                item.Property("CreateTime").CurrentValue = DateTime.Now;
                temp.OperationBy = _userProvider.getUserId();
                temp.OperationDateTime = DateTime.Now;
                temp.OperationType = OperationType.Add;
                _addedEntities.Add(item);
            }
            if (item.State == EntityState.Modified)
            {
                temp.OperationBy = _userProvider.getUserId();
                temp.OperationDateTime = DateTime.Now;
                temp.OperationType = OperationType.Uptate;
                LogFootPrint(temp);
            }
        }
    }
    private void AuditingAddedEntities(IEnumerable<EntityEntry> addedEntites)
    {
        foreach (var item in addedEntites)
        {
            var temp = item.Entity as IAutitable;
            LogFootPrint(temp);
        }
    }
    private void LogFootPrint(IAutitable input)
    {
        var serializedEntity = JsonConvert.SerializeObject(input);
        var logData = new LogData()
        {
            EntityTpe = input.GetType().Name,
            DateTime = DateTime.Now,
            serializedData = serializedEntity,
            EntityId = input.Id.ToString(),
            UserId = _userProvider.getUserId().ToString(),
        };
        var footPrintLogDbContext =
                    _logProvider.getLogDbContext();
        footPrintLogDbContext.Add(logData);
        footPrintLogDbContext.SaveChanges();
    }

}