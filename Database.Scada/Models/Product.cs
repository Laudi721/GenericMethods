using Database.Scada.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public Unit Unit{ get; set; }

        public int UnitId { get; set; }
    }
}
