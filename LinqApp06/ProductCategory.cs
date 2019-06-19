using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqApp06.CodeFirst
{
    [Table(Name = "Production.ProductCategory")]
    class ProductCategory
    {
        [Column(IsPrimaryKey = true)]
        public int ProductCategoryID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(Name = "rowguid")]
        public Guid guid { get; set; }

        [Column]
        public DateTime ModifiedDate { get; set; }

        [Association(OtherKey = "ProductCategoryID")]
        public EntitySet<ProductSubcategory> ProductSubcategories { get; set; }

    }
}
