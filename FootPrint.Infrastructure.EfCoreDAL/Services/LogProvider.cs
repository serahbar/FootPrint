namespace FootPrint.Infrastructure.EfCoreDAL.Services
{
    public class LogProvider : ILogProvider<FootPrintLogDbContext>
    {
        private readonly IUserProvider _userPovider;

        public LogProvider(IUserProvider userPovider)
        {
           _userPovider = userPovider;
        }
        public FootPrintLogDbContext getLogDbContext()
        {
            return new FootPrintLogDbContext(_userPovider);
        }
    }

}
