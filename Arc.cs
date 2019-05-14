using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymNetwork
{
    public class Arc
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Arc(int id, string name)
        {
            ID = id;
            Name = name; 
        }
    }
}
