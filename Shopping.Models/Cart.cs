using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Models
{
    public class Cart
    {
        public string ProductName { get; set; }
        public string Username { get; set; }
        public int Quantity { get; set; }
        public int FinalPrice { get; set; }
    }
}
