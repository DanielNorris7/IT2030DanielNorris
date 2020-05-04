using EventFinder.Data;
using EventFinder.Models;
using System.Linq;
using System.Web.Mvc;

namespace EventFinder.Controllers
{
    public class ShoppingCartController : Controller
    {
        EventFinderContext db = new EventFinderContext();

        [Authorize]
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

        // GET: Event/FindEvent
        public ActionResult TicketsOrdered()
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            ShoppingCartViewModel vm = new ShoppingCartViewModel()
            {
                CartItems = cart.GetCartItems()
            };

            return View(vm);
        }

        [Authorize]
        // GET: ShoppingCart/AddToCart
        public ActionResult AddToCart(int id)
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(id);

            return RedirectToAction("Index");
        }

        [Authorize]
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