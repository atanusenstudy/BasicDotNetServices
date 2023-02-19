using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDotNetServices.Core.Model
{
    public class Institution
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public short Established { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
