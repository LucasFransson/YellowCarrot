using EntityFrameworkCore.EncryptColumn.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        [EncryptColumn]
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;


    }
}
