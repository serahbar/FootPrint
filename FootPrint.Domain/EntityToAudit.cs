using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootPrint.Domain
{
    public class EntityToAudit:Autitable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
