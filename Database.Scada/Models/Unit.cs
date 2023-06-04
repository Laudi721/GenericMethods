﻿using Database.Scada.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Models
{
    [Table("Units")]
    public class Unit : IModel
    {
        public Unit()
        {
            Products = new List<Product>();
        }
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? TimeDeleted { get; set; }

        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
