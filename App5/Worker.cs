using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
   public class Worker
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public double hours { get; set; }
      
        public double Tariff { get; set; }
        public double salary { get; set; }
        public double salaryw { get; set; }

        internal static List<Worker> FindAll(Func<Worker, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
