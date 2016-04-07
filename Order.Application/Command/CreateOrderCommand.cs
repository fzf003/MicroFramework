using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command
{
    public class CreateOrderCommand:BaseCommand<Order.Application.Model.Order>
    {

        public CreateOrderCommand() { }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 购买个数
        /// </summary>
        public int Quantity { get; set; }
    }
}
