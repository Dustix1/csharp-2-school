namespace ProductsApp
{

    delegate string MyFirstDelegate(string x, int y);

    delegate bool Condition(int n);

    static class MyExtensionMethods
    {
        public static int Pow(this int num, int n)
        {
            return (int)Math.Pow(num, n);
        }

        /*public static int[] Even(this int[] arr)
        {
            List<int> tmp = new List<int>();
            for (int i = 0; i < arr.Length; i += 2)
            {
                tmp.Add(arr[i]);
            }

            return tmp.ToArray();
        }*/

        public static IEnumerable<int> Even(this IEnumerable<int> arr)
        {
            int i = 0;
            foreach (int n in arr)
            {
                if (i % 2 == 0)
                {
                    yield return n;
                }
                i++;
            }
        }

        public static IEnumerable<int> Filter(this IEnumerable<int> arr, Condition condition)
        {
            int i = 0;
            foreach (int n in arr)
            {
                if (condition(n))
                {
                    yield return n;
                }
                i++;
            }
        }
    }

    internal class Program
    {
        private static string Test(string a, int b)
        {
            Console.Write("Test1");
            return $"{a}: {b}!";
        }

        private static string Test2(string a, int b)
        {
            Console.WriteLine("Test2");
            return $"{a}: {b}!!";
        }

        static void Main(string[] args)
        {
            /*MyFirstDelegate x = null;

            string result = x?.Invoke("Číslo", 5);*/

            /*MyFirstDelegate x = Test;
            x += Test2;*/

            /* MyFirstDelegate x = Test;
             x += Test2;
             x -= Test;

             string result = x.Invoke("Číslo", 5);

             Console.WriteLine(result);

             */

            /*int Sum(int a, int b)
            {
                return a + b;
            }

            Calculator calc = new Calculator();

            calc.onSetXY += (sender, args) => Console.WriteLine("Nastaveno");
            calc.onCompute += (sender, args) => Console.WriteLine($"Výsledek: {args.Result}");

            calc.SetXY(10, 5);

            calc.Execute(Sum);

            calc.Execute((x, y) => x * y);
            /*calc.Execute((int x, int y) => {
                return x * y;                         //   same shit just longer :3
            });*/

            /*
                        int x = 3;

                        int result = x.Pow(2).Pow(3).Pow(4);

                        int result2 = (int)Math.Pow((int)Math.Pow((int)Math.Pow(x, 2), 3), 4);     //  same shit just looks like overseer code

                        Console.WriteLine(result);
                        Console.WriteLine(result2);

                        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 };

                        /*Console.WriteLine(string.Join(", ", arr.Even().Even()));
                        Console.WriteLine(string.Join(", ", arr.Filter(x => x < 5).Filter(x => x % 2 == 0).Even()));*/

            //Console.WriteLine(string.Join(", ", arr.Where(x => x < 5)));            //   Extension metoda už v C# (LINQ)






            /**
             *
             *      LINQ shit   (Extension metody n shit)
             * 
             **/

            IEnumerable<Product> products = GetProducts();
            double avgPrice = products.Where(x => x.Price != null).Average(x => x.Price.Value);     // díky kontrole null můžu použít .Value a nemusím dávat double? kvůli nullable typu
            Console.WriteLine(avgPrice);

            Console.WriteLine();

            double avgPriceSkladem = products.Where(x => (x.Price != null && x.Quantity > 0)).Average(x => x.Price.Value);
            Console.WriteLine(avgPriceSkladem);

            Console.WriteLine();

            List<string> names = products.Select(x => x.Name).ToList();
            foreach (string name in names) { Console.WriteLine(name); }

            Console.WriteLine();

            Product firstProduct = products.First();
            Console.WriteLine(firstProduct.Name);

            Console.WriteLine();

            Product lastProduct = products.Last();      // lepší je použít products[index] protože .Last() prochází celé pole
            Product lastProduct2 = products.LastOrDefault();      // vrátí null pokud neexistuje (.Last() spadne pokud neexistuje)
            Console.WriteLine(lastProduct.Name);

            Console.WriteLine();

            foreach (var group in products.GroupBy(x => x.Quantity))
            {
                Console.WriteLine(group.Key); // x.Quantity
                foreach(Product pr in group)
                {
                    Console.WriteLine(pr.Name);
                }
            }


            // něco navíc

            /*List<int> a = [1, 2];
            int c = a.Count;
            int d = a.Count();*/

            /*var tmp = products.Select(x =>
            {
                Console.WriteLine("Test");
                return x.Name;
            });         // bez .tueráj() se to pouští víckrát

            int c = tmp.Count();
            int c = tmp.Count();*/
        }



        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product(){ Id = 1, Name = "Auto", Price = 700_000, Quantity = 10 },
                new Product(){ Id = 1, Name = "Slon", Price = 1_500_000, Quantity = 0 },
                new Product(){ Id = 1, Name = "Kolo", Price = 26_000, Quantity = 5 },
                new Product(){ Id = 1, Name = "Brusle", Price = 2_800, Quantity = 30 },
                new Product(){ Id = 1, Name = "Hodinky", Price = 18_500, Quantity = 2 },
                new Product(){ Id = 1, Name = "Mobil", Price = 24_000, Quantity = 0 }
            };
        }
    }
}
