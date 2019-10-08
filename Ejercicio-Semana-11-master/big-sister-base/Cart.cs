using System;
using System.Collections.Generic;
using System.Text;

namespace big_sister_base
{  
    [Serializable]
    public class Cart
    {
        private List<Product> products;

        public List<Product> Products { get => products; set => products = value; }

        public Cart()
        {
            this.products = new List<Product>();
        }

        public void Clear()
        {
            this.products = new List<Product>();
        }

        public override string ToString()
        {
            string printString = "Su carrito:\n\n";
            foreach(Product p in products)
            {
                printString += p.ToString() + "\n";
            }
            return printString;
        }
    }
}
