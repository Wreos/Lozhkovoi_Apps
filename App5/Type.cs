using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
     public class Type
    {
        public int Id { get; set; }
        public string NameOfType { get; set; }
        public int tarif { get; set; }
        public List <Worker> Workers { get; set; }
        public override string ToString()
        {
            return NameOfType; 
        }
    }
}
