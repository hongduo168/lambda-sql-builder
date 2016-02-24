using System.Collections.Generic;

namespace LambdaSqlBuilder.Tests.Entities
{
    [SqlLambdaTable(Name = "Categories")]
    public class Category
    {
        
        public int CategoryId { get; set; }

        public int GetCategoryId()
        {
            return CategoryId;
        }
        
        [SqlLambdaColumn(Name = "Category Name")]
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
