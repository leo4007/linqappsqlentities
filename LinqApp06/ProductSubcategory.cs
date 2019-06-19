using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqApp06.CodeFirst
{

    [Table(Name = "Production.ProductSubcategory")]
    class ProductSubcategory
    {
        [Column(IsPrimaryKey = true)]
        public int ProductSubcategoryID { get; set; }

        [Column]
        public int ProductCategoryID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(Name = "rowguid")]
        public Guid guid { get; set; }

        [Column]
        public DateTime ModifiedDate { get; set; }

        private EntityRef<ProductCategory> _ProductCategory;

        [Association(Storage = "_ProductCategory", ThisKey = "ProductCategoryID", IsForeignKey = true)]
        public ProductCategory ProductCategory
        {
            get { return this._ProductCategory.Entity; }
            set { this._ProductCategory.Entity = value; }
        }
    }

}
