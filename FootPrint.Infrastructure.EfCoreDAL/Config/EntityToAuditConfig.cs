using FootPrint.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootPrint.Infrastructure.EfCoreDAL.Config
{
    public class EntityToAuditConfig : IEntityTypeConfiguration<EntityToAudit>
    {
        public void Configure(EntityTypeBuilder<EntityToAudit> entity)
        {
            entity.HasKey(e => e.Id);
            // add this shadow property on fly by OnModelCreatiog 
            //entity.Property<int>("CreateBy"); 
            //entity.Property<DateTime>("CreateTime");
        }
    }
}
