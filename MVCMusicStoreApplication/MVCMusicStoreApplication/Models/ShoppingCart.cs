using MVCMusicStoreApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStoreApplication.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId;

        private MVCMusicStoreDB db = new MVCMusicStoreDB();

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.ShoppingCartId = cart.getCardId(context);

            return cart;
        }

        private string getCardId(HttpContextBase context)
        {
            const string cartSessionId = "CartId";
            string cartId;

            if (context.Session[cartSessionId] == null)
            {
                cartId = Guid.NewGuid().ToString();

                context.Session[cartSessionId] = cartId;
            }
            else
            {
                cartId = context.Session[cartSessionId].ToString();
            }

            return cartId;
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(o => o.CartId == this.ShoppingCartId).ToList();
        }

        public decimal GetCartTotal()
        {
            decimal? total = (from cartItem in db.Carts
                              where cartItem.CartId == this.ShoppingCartId
                              select cartItem.AlbumSelected.Price * (int?)cartItem.Count).Sum();


            return total ?? decimal.Zero;
        }

        public void AddToCart(int albumnId)
        {
            Cart cartItem = db.Carts.SingleOrDefault(o => o.CartId == this.ShoppingCartId && o.AlbumID == albumnId);

            if (cartItem == null)
            {
                // Create a new cart
                cartItem = new Cart()
                {
                    CartId = this.ShoppingCartId,
                    AlbumID = albumnId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                // Add the new cart to the database
                db.Carts.Add(cartItem);
            }
            else
            {
                // Item is already in the db, increase the quantity
                cartItem.Count++;
            }

            db.SaveChanges();
        }

        public int RemoveFromCart(int recordId)
        {
            Cart cartItem = db.Carts.SingleOrDefault(o => o.CartId == this.ShoppingCartId && o.RecordId == recordId);

            if (cartItem == null)
            {
                throw new NotImplementedException();
            }

            int newCount;

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                newCount = cartItem.Count;
            }
            else
            {
                db.Carts.Remove(cartItem);
                newCount = 0;
            }

            db.SaveChanges();

            return newCount;
        }
    }
}