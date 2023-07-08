using FootPrint.Infrastructure.EfCoreDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootPrint.EndPoint.Console
{
    public class ConsoleAppUserProvider : IUserProvider
    {
        public int getUserId()
        {
          return new Random().Next();
        }
    }
}
