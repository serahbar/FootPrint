using FootPrint.Domain;
using FootPrint.Infrastructure.EfCoreDAL.Services;
using Microsoft.EntityFrameworkCore;

namespace FootPrint.Infrastructure.EfCoreDAL
{
    public class FootPrintLogDbContext : DbContext
    {
        private readonly IUserProvider _userPovider;
        public FootPrintLogDbContext(IUserProvider userPovider)
        {

            _userPovider = userPovider;
        }
        public DbSet<LogData> logDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseSqlServer ("Server=.; initial Catalog=FootPrintLogDb; integrated security = true; Encrypt=false");
            base.OnConfiguring (optionsBuilder);
        }
}
}
