using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Item
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Strength { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public string Description { get; set; }
        public Boolean OnStock { get; set; }

    }
}
