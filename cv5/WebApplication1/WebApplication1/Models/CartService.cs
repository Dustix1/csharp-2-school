using System.Text.Json;

namespace WebApplication1.Models
{
    public class CartService
    {
        private readonly IHttpContextAccessor accessor;

        public CartService(IHttpContextAccessor accessor) // protot ojesem si rehistroval hattetépé akcesor v program.cs
        {
            this.accessor = accessor;
        }
        public void Add(Product product)
        {
            HttpContext ctx = accessor.HttpContext;

            List<Product> products = List();
            products.Add(product);
            ctx.Session.SetString("cart", JsonSerializer.Serialize(products));  // TaDy VáS mŮžE naPadNoUt sErIaLIzAcE   no určitě vole už hodinu nevím co se děje
        }

        public List<Product> List()
        {
            HttpContext ctx = accessor.HttpContext;
            string json = ctx.Session.GetString("cart") ?? "[]";
            return JsonSerializer.Deserialize<List<Product>>(json);
        }

        public void Remove(int id)
        {
            HttpContext ctx = accessor.HttpContext;

            List<Product> products = List();
            products.Remove(products.First(x => x.Id == id));
            ctx.Session.SetString("cart", JsonSerializer.Serialize(products));
        }
    }
}
