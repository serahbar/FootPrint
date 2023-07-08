using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootPrint.Infrastructure.EfCoreDAL.Services
{
    public interface ILogProvider<T> where T : class
    {
        T getLogDbContext();   
    }

}
