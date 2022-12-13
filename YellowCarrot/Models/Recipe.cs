using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public List<Ingredient> Ingredients { get; set; } = new();
        public int? TagID { get; set; }   

    }
}
