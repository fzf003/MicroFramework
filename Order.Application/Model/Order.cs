using MicroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Model
{
    public class Order : Aggregate<string>
    {
        public Order()
            : base(Guid.NewGuid().ToString("N"))
        {

        }
        public Address Address { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
