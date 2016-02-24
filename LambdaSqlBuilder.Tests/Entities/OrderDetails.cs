using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdaSqlBuilder.Tests.Entities
{
    [SqlLambdaTable(Name = "Order Details")]
    public class OrderDetails
    {
        [SqlLambdaColumn(Name = "Order ID")]
        public int OrderId { get; set; }

        [SqlLambdaColumn(Name = "Product ID")]
        public int ProductId { get; set; }
    }
}
