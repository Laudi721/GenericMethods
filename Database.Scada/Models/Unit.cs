using Database.Scada.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Models
{
    public class Unit : BaseModel
    {
        public Unit()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}
