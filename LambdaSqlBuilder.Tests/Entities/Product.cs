
namespace LambdaSqlBuilder.Tests.Entities
{
    [SqlLambdaTable(Name = "Products")]
    public class Product
    {
        [SqlLambdaColumn(Name = "Product ID")]
        public int ProductId { get; set; }

        public int GetProductId()
        {
            return ProductId;
        }
        
        [SqlLambdaColumn(Name = "Product Name")]
        public string ProductName { get; set; }

        [SqlLambdaColumn(Name = "English Name")]
        public string EnglishName { get; set; }
        
        [SqlLambdaColumn(Name = "Category ID")]
        public int CategoryId { get; set; }

        [SqlLambdaColumn(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        [SqlLambdaColumn(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        [SqlLambdaColumn(Name = "Reorder Level")]
        public int? NullableReorderLevel { get { return ReorderLevel; } }

        [SqlLambdaColumn(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        public Category Category { get; set; }
    }
}
