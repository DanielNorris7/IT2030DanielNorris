using EventApplication.Models;
using System.Linq;
using System.Web.Mvc;

namespace EventApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        EventDB db = new EventDB();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            ShoppingCartViewModel vm = new ShoppingCartViewModel()
            {
                CartItems = cart.GetCartItems()
            };

            return View(vm);
        }

        // GET: ShoppingCart/AddToCart
        public ActionResult AddToCart(int id)
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(id);

            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/RemoveFromCart
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            Event cartEvent = db.Carts.SingleOrDefault(o => o.RecordId == id).EventSelected;

            int itemCount = cart.RemoveFromCart(id);

            ShoppingCartRemoveViewModel vm = new ShoppingCartRemoveViewModel()
            {
                DeleteId = id,
                ItemCount = itemCount,
                Message = $"{cartEvent.EventTitle} has been removed from the cart"
            };

            return Json(vm);
        }
    }
}