using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdaSqlBuilder.Tests.Entities
{
    [SqlLambdaTable(Name = "Orders")]
    public class Order
    {
        [SqlLambdaColumn(Name = "Order ID")]
        public int OrderId { get; set; }

        [SqlLambdaColumn(Name = "Ship Name")]
        public string ShipName { get; set; }

        [SqlLambdaColumn(Name = "Ship Region")]
        public string ShipRegion { get; set; }

        [SqlLambdaColumn(Name = "Required Date")]
        public DateTime RequiredDate { get; set; }

        [SqlLambdaColumn(Name = "Shipped Date")]
        public DateTime ShippedDate { get; set; }

        public List<Product> Products { get; set; }
    }
}
