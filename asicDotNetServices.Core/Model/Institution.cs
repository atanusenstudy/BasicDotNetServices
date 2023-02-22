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
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        public short Established { get; set; }
        public string Address { get; set; }
    }
}
