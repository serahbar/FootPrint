using FootPrint.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootPrint.Infrastructure.EfCoreDAL.Config
{
    public class OtherEntityToAuditConfig : IEntityTypeConfiguration<OtherEntityToAudit>
    {
        public void Configure(EntityTypeBuilder<OtherEntityToAudit> entity)
        {
            entity.HasKey(e => e.Id);
            // add this shadow property on fly by OnModelCreatiog 
            //entity.Property<int>("CreateBy"); 
            //entity.Property<DateTime>("CreateTime");
        }
    }
}
