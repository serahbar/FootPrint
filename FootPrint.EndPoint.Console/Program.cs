using FootPrint.Domain;
using FootPrint.EndPoint.Console;
using FootPrint.Infrastructure.EfCoreDAL;
using FootPrint.Infrastructure.EfCoreDAL.Services;

var consoleAppUserProvider = new ConsoleAppUserProvider();
var logProvider = new LogProvider(consoleAppUserProvider);
using (var ctx = new FootPrintLogDbContext(consoleAppUserProvider))
{
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}
using (var ctx = new FootPrintDbContext(consoleAppUserProvider, logProvider))
{

    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    var entity = new EntityToAudit()
    {

        Name = "Test",
    };
    var entity1 = new EntityToAudit()
    {
        Name = "Test1",
    };
    var entity2 = new EntityToAudit()
    {
        Name = "Test2",
    };
    var otherEentity1 = new OtherEntityToAudit()
    {

        Name = "OtherTest1",
        Code = "Codel",
    };
    var otherEentity2 = new OtherEntityToAudit()
    {
        Name = "OtherTest2",
        Code = "Code2"
    };
    ctx.otherentityToAudits.Add(otherEentity1);
    ctx.entityToAudits.Add(entity);
    ctx.entityToAudits.Add(entity1);
    ctx.entityToAudits.Add(entity2);
    ctx.otherentityToAudits.Add(otherEentity2);
    ctx.SaveChanges();
}
