using System;
using System.Collections.Generic;
using System.Text;

namespace big_sister_base
{
    [Serializable]
    public class Product
    {
        private string name;
        private int stock;
        private int price; //Price for one unit of the product
        private string unit;

        public Product(string name, int price, int stock, string unit)
        {
            this.name = name;
            this.stock = stock;
            this.price = price;
            this.unit = unit;
        }

        public string Name { get => name;}
        public int Stock { get => stock; set => stock = value; }
        public int Price { get => price;}
        public string Unit { get => unit;}

        public override string ToString()
        {
            return $"{Name}\n\tPrecio: ${Price}\t{Unit}";
        }
    }
}
