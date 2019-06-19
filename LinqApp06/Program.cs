using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqApp06
{
    class Program
    {
        static void Main(string[] args)
        {
            int op = 1;
            while (op != 0)
            {
                Console.Clear();
                Console.WriteLine("Dentre as alternativas abaixo:");
                Console.WriteLine("1 - Consultar");
                Console.WriteLine("2 - Incluir");
                Console.WriteLine("3 - Alteração");
                Console.WriteLine("4 - Excluir");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("Selecione uma opcao:");

                if (!Int32.TryParse(Console.ReadLine().ToString(), out op))
                    op = -1;

                switch (op)
                {
                    case 1:
                        RealizarConsultas();
                        break;
                    case 2:
                        RealizarInclusao();
                        break;
                    case 3:
                        RealizarAtualizacao();
                        break;
                    case 4:
                        RealizarExclusao();
                        break;
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("Opcao invalida!\n\n");
                        Console.ReadKey();
                        break;
                }

            }

        }

        private static void RealizarConsultas()
        {
            AdventureWorks dc = new AdventureWorks(@"Server = localhost\SQLEXPRESS; Database = AdventureWorks2017; User Id = sa; Password = root;");

            // Consultando categorias
            var result = from pc in dc.ProductCategories
                         orderby pc.Name
                         select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name);

            foreach (var item in result)
            {
                Console.WriteLine("{0} - {1}", item.ProductCategoryID, item.Name);
            }

            /*
            //Consultando categorias e subcategorias
            var resultSubCategorias = from pc in dc.ProductCategories
                                      orderby pc.Name
                                      select new { Nome = pc.Name, SubCategorias = pc.ProductSubcategories };


            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name).Select(pc => new {Nome = pc.Name, SubCategorias = pc.ProductSubcategories });

            foreach (var item in resultSubCategorias)
            {
                Console.WriteLine("******************************");
                Console.WriteLine("Subcategorias de {0}:", item.Nome);
                foreach (var sub in item.SubCategorias)
                {
                    Console.WriteLine("{0}", sub.Name);
                }
                Console.WriteLine("******************************");
                Console.WriteLine("");
            }

            //Contexto e classes criadas com a ferramenta do Visual Studios Linq to SQL
            AdventureWorksByToolDataContext dct = new AdventureWorksByToolDataContext();
            //COnfigurando para carregar subcategorias juntamente com  as categorias
            DataLoadOptions options = new DataLoadOptions();
            options.LoadWith<ProductCategory>(pc => pc.ProductSubcategories);
            dct.LoadOptions = options;

            dct.Log = Console.Out;

            var resultLinqToSQL = from pc in dct.ProductCategories
                                  orderby pc.Name
                                  select pc;

            //Em sintaxe de método ficaria da seguinte forma:
            //var result = dc.ProductCategories.OrderBy(pc => pc.Name);

            foreach (var item in resultLinqToSQL)
            {
                Console.WriteLine(item.Name);
                // Exibo quantas SubCategorias estão associadas
                Console.WriteLine(item.ProductSubcategories.Count());
            }
            */

                Console.ReadKey();
        }

        private static void RealizarInclusao()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
            string categoria;
            Console.WriteLine("Informe a categoria");
            categoria = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(categoria))
            {
                ProductCategory pc = new ProductCategory
                {
                    Name = categoria,
                    rowguid = new Guid(),
                    ModifiedDate = DateTime.Now
                };

                dc.ProductCategories.InsertOnSubmit(pc);
                dc.SubmitChanges();

                Console.WriteLine("O registro {0}, incluído com sucesso\n", pc.ProductCategoryID);
            }
        }

        private static void RealizarAtualizacao()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
            string categoria;
            int id;

            Console.Clear();
            Console.Write("Informe o id categoria que será alterada:");
            if (Int32.TryParse(Console.ReadLine(), out id))
            {
                ProductCategory pc = dc.ProductCategories.Single(p => p.ProductCategoryID == id);
                Console.WriteLine("\n\nInforme um novo nome para a categoria {0}:", pc.Name);
                categoria = Console.ReadLine();


                if (!String.IsNullOrWhiteSpace(categoria))
                {
                    pc.Name = categoria;
                    pc.ModifiedDate = DateTime.Now;

                    dc.SubmitChanges();

                    Console.WriteLine("O registro {0}, alterado com sucesso\n", pc.ProductCategoryID);
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            }
        }

        private static void RealizarExclusao()
        {
            AdventureWorksByToolDataContext dc = new AdventureWorksByToolDataContext();
            int id;

            Console.Clear();
            Console.Write("Informe o id categoria que será excluida:");
            if (Int32.TryParse(Console.ReadLine(), out id))
            {
                ProductCategory pc = dc.ProductCategories.Single(p => p.ProductCategoryID == id);

                //Em sintaxe de pesquisa ficaria da seguinte forma:
                //
                //ProductCategory pc2 = (from p in dc.ProductCategories
                //                       where p.ProductCategoryID == id
                //                       select p).Single();

                dc.ProductCategories.DeleteOnSubmit(pc);
                dc.SubmitChanges();

                Console.WriteLine("Registro excluio com sucesso\n");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
