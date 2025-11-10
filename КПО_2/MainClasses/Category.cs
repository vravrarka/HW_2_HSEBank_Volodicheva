using Bank.App.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank.MainClasses
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoryType 
    {
        Income,
        Expense
    }

    public class Category : ObjectId, IVisitable
    {  
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public void Accept(DataVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
