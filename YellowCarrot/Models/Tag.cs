using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models
{
     public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public List<Recipe> Recipes { get; set; } = new();

        public override string ToString()
        {
            return Name;
        }

    }
}
