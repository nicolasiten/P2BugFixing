using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private List<CartLine> _cartLineList;

        public Cart()
        {
            _cartLineList = new List<CartLine>();
        }

        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();
        
        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return _cartLineList;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            List<CartLine> cartLineList = GetCartLineList();
            if (cartLineList.Any(l => l.Product.Id == product.Id))
            {
                cartLineList.First(l => l.Product.Id == product.Id).Quantity += quantity;
            }
            else
            {
                GetCartLineList().Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double totalValue = 0;
            foreach (CartLine item in GetCartLineList())
            {
                totalValue += item.Product.Price * item.Quantity;
            }

            return totalValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            int numberOfProducts = 0;
            foreach (CartLine item in GetCartLineList())
            {
                numberOfProducts += item.Quantity;
            }
            
            return numberOfProducts > 0 ? GetTotalValue() / numberOfProducts : 0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            CartLine cartLine = GetCartLineList().FirstOrDefault(l => l.Product.Id == productId);

            return cartLine?.Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
