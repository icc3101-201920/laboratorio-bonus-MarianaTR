using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace big_sister_base
{
    public class BigSister
    {
        private List<string> NecessaryToMakeLasagna;

        public void OnLittleGuyTookProduct(object source, LittleGuyProductEventArgs args)
        { 
            Console.WriteLine("\nBig Sister says: Hey! Estas agregando algo al carrito");
            Console.WriteLine($"Big Sister says: Acabas de agregar {args.cart.Products[args.cart.Products.Count - 1].Name}");
            Thread.Sleep(3000);
            Product productoAgregado = args.cart.Products[args.cart.Products.Count - 1];
            LittleGuy guy = (LittleGuy)source;
            List<Product> ShopList = (List<Product>)guy.ShopList;

            foreach (Product product in ShopList)
            {
                if (product.Name == productoAgregado.Name)
                {
                    Console.WriteLine(product.Stock);
                    if (product.Stock == 1)
                    {
                        product.Stock -= 1;
                        Console.WriteLine("Big Sister says: It is necessary...");
                        Thread.Sleep(3000);
                        break;
                    }
                    else
                    {
                        guy.RemoveProduct(productoAgregado);
                        Console.WriteLine("Big Sister says: You cant buy that product!");
                        Thread.Sleep(3000);
                    }
                }
            }

        }

        public bool OnLittleGuyWantsToPay(object source, LittleGuyProductEventArgs args)
        {
            LittleGuy guy = (LittleGuy)source;
            List<Product> ShopList = (List<Product>)guy.ShopList;
            bool right = true;
            foreach (Product product in ShopList)
            {
                if (product.Stock == 1)
                {
                    right = false;
                    break;
                }
            }

            if (right != true)
            {
                Console.WriteLine("Big Sister says: You cant exit!");
                Thread.Sleep(3000);
                return false;
            }

            return true;

        }
    }
}
