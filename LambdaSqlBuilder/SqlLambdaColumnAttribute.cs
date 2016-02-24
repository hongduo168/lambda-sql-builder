using System;

namespace LambdaSqlBuilder
{
    /// <summary>
    /// Configures the name of the column related to this property. If the attribute is not specified, the property name is used instead.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlLambdaColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public SqlLambdaColumnAttribute(string name = "")
        {
            this.Name = name;
        }
    }
}
