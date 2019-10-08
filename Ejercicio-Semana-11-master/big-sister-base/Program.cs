using System;
using System.Threading;

namespace big_sister_base
{
    class Program
    {
        static void Main(string[] args)
        {
            LittleGuy littleGuy = new LittleGuy();
            Market market = new Market();
            BigSister sista = new BigSister();
            littleGuy.LittleGuyTookProduct += sista.OnLittleGuyTookProduct;
            littleGuy.LittleGuyWantsToPay += sista.OnLittleGuyWantsToPay;
            bool continueCycle = true;
            littleGuy.Cart.Clear();
            while (continueCycle)
            {
                Console.Clear();
                Console.WriteLine("¿Que quieres hacer?\n");
                Console.WriteLine("\t1. Ver Receta");
                Console.WriteLine("\t2. Comprar");
                Console.WriteLine("\t3. Ver carrito");
                Console.WriteLine("\t4. Pagar");
                Console.WriteLine("\t5. Salir");
                String answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        littleGuy.ViewRecipe();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("¿Que deseas comprar? (-1 para cancelar)\n\n");
                        for (int i = 0; i < market.Storage.Count; i++)
                        {
                            Console.WriteLine($"[{i}] " + market.Storage[i].ToString() + $"\t Stock:{market.Storage[i].Stock}");
                        }
                        int index = Convert.ToInt32(Console.ReadLine());
                        if (index != -1)
                        {
                            if (market.Storage[index].Stock > 0)
                            {
                                littleGuy.AddProduct(market.Storage[index]);
                                market.removeStorage(index);
                            }
                            else
                            {
                                Console.WriteLine("Product sin stock");
                            }
                        }
                        
                        break;
                    case "3":
                        Console.Clear();
                        littleGuy.ViewCart();
                        Console.WriteLine("\n\nPresiona ENTER para volver al supermercado...");
                        ConsoleKeyInfo response = Console.ReadKey(true);
                        while (response.Key != ConsoleKey.Enter)
                        {
                            response = Console.ReadKey(true);
                        }
                        break;
                    case "4":
                        Console.Clear();
                        bool asdf = littleGuy.Pay();
                        if (asdf == false) break;
                        littleGuy.SaveData();
                        market.SaveStorage();
                        continueCycle = false;
                        break;
                    case "5":
                        Console.Clear();
                        Cart cart = new Cart();
                        littleGuy.SaveData();
                        market.SaveStorage();
                        continueCycle = false;
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
        }
    }
}
