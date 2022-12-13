using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [MaxLength(30)]
        public string Unit { get; set; } = null!;
    }
}
