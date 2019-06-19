using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqApp06
{
    class AdventureWorks : DataContext
    {
        public AdventureWorks(string connection) : base(connection) { }

        public Table<CodeFirst.ProductCategory> ProductCategories
        {
            get { return this.GetTable<CodeFirst.ProductCategory > (); }
        }

        public Table<CodeFirst.ProductSubcategory> ProductSubcategories
        {
            get { return this.GetTable<CodeFirst.ProductSubcategory > (); }
        }
    }
}
