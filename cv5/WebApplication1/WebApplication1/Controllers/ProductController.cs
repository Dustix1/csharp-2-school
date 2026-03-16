using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index([FromServices] ProductService productService)
        {
            ViewBag.Products = productService.List();

            return View();
        }

        public IActionResult Detail(int id, string test, int num, [FromServices] ProductService productService)
        {
            Product product = productService.GetProductById(id);
            if (product == null) { return NotFound(); }

            ViewBag.Product = product;
            return View(new AddToBasketForm()
            {
                ProductId = product.Id,
                Quantity = 1
            });
        }

        [HttpPost]
        public IActionResult Detail(int id, AddToBasketForm form, [FromServices] ProductService productService, [FromServices] CartService cartService)
        {
            Product product = productService.GetProductById(id);
            if (product == null) { return NotFound(); }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < form.Quantity; i++)
                {
                    cartService.Add(product);
                }
                return RedirectToAction("Cart");
            }

            ViewBag.Product = product;
            return View(new AddToBasketForm()
            {
                ProductId = product.Id,
                Quantity = 1
            });
        }

        public IActionResult Cart([FromServices] CartService cartService)
        {
            ViewBag.Items = cartService.List();
            return View();
        }

        public IActionResult Remove(int id, [FromServices] CartService cartService)
        {
            cartService.Remove(id);
            return RedirectToAction("Cart");
        }
    }
}
