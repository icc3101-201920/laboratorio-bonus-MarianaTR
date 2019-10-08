using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace big_sister_base
{

    public class LittleGuy
    {
        public delegate void LittleGuyTookProductEventHandler(object source, LittleGuyProductEventArgs args);

        public event LittleGuyTookProductEventHandler LittleGuyTookProduct;

        protected virtual void OnLittleGuyTookProduct()
        {
            //3.1- Revisar si existen suscriptores
            if (LittleGuyTookProduct != null)
            {
                //3.2- Se dispara el evento. La fuente es este objeto y EventArgs.Empty ya que no queremos pasar parametros adicionales
                LittleGuyTookProduct(this, new LittleGuyProductEventArgs(this.Cart));
            }
        }

        public delegate bool LittleGuyWantsToPayEventHandler(object source, LittleGuyProductEventArgs args);

        public event LittleGuyWantsToPayEventHandler LittleGuyWantsToPay;

        protected virtual bool OnLittleGuyWantsToPay()
        {
            //3.1- Revisar si existen suscriptores
            if (LittleGuyWantsToPay != null)
            {
                //3.2- Se dispara el evento. La fuente es este objeto y EventArgs.Empty ya que no queremos pasar parametros adicionales
                return LittleGuyWantsToPay(this, new LittleGuyProductEventArgs(this.Cart));
            }
            return false;
        }

        private Cart cart;
        private List<Product> shopList = new List<Product>();

        public LittleGuy()
        {
            if (!LoadData())
            {
                Cart = new Cart();
                Generatelist();
            }
           
        }

        public Cart Cart { get => cart; private set => cart = value; }
        public List<Product> ShopList { get => shopList; set => shopList = value; }

        public void Generatelist()
        {
            ShopList.Add(new Product("Leche Entera", 820, 1, "1L"));
            ShopList.Add(new Product("Gomitas Flipy", 720, 0, "100g"));
            ShopList.Add(new Product("Mantequilla", 850, 1, "125g"));
            ShopList.Add(new Product("Crema para hemorroides", 4990, 0, "300cc"));
            ShopList.Add(new Product("Pimienta", 430, 1, "15g"));
            ShopList.Add(new Product("Vino Sauvignon Blanc Reserva Botella", 4150, 1, "750cc"));
            ShopList.Add(new Product("Sal Lobos", 330, 1, "1kg"));
            ShopList.Add(new Product("Cuaderno Mi Pequeño Pony", 1290, 0, "1un"));
            ShopList.Add(new Product("Láminas de Lasaña", 1250, 1, "400g"));
            ShopList.Add(new Product("Tomate", 1290, 0, "1kg"));
            ShopList.Add(new Product("Harina", 890, 1, "1kg"));
            ShopList.Add(new Product("Audifonos Samsung", 5990, 0, "1un"));
            ShopList.Add(new Product("Pisco Alto del Carmen", 5990, 0, "1L"));
            ShopList.Add(new Product("Carne Molida", 4390, 1, "500g"));
            ShopList.Add(new Product("Aceite de Oliva", 1790, 1, "250g"));
            ShopList.Add(new Product("Sal parrillera", 840, 0, "750g"));
            ShopList.Add(new Product("Cable HDMI 1m", 3990, 0, "1un"));
            ShopList.Add(new Product("Queso Rallado Parmesano", 499, 1, "40g"));
            ShopList.Add(new Product("Vino Blanco Caja", 2790, 0, "2L"));
            ShopList.Add(new Product("Malla de Cebollas", 1090, 1, "1kg"));
            ShopList.Add(new Product("Tomates Pelados en lata", 700, 1, "540g"));
            ShopList.Add(new Product("Queso Parmesano", 3790, 0, "200g"));
            ShopList.Add(new Product("Bolsa de Zanahorias", 890, 1, "1un"));
        }

        public void ViewRecipe()
        {
            Console.Clear();
            Console.WriteLine("\t\t===> Lasagne alla bolognese <===\n");
            Console.WriteLine("Ingredientes básicos:");
            Console.WriteLine("\t12 láminas de Lasaña");
            Console.WriteLine("\t40 gramos de parmesano rallado");
            Console.WriteLine("\tMantequilla\n");
            Console.WriteLine("Para el relleno:");
            Console.WriteLine("\t300 gramos de carne molida");
            Console.WriteLine("\tMedio vaso de vino blanco");
            Console.WriteLine("\t250 gramos de tomate entero pelado de lata");
            Console.WriteLine("\t1 zanahoria");
            Console.WriteLine("\t1 cebolla");
            Console.WriteLine("\tAceite de oliva");
            Console.WriteLine("\tSal");
            Console.WriteLine("\tPimienta\n");
            Console.WriteLine("Para la bechamel:");
            Console.WriteLine("\t50 gramos de mantequilla");
            Console.WriteLine("\t500 gramos de harina");
            Console.WriteLine("\tMedio litro de leche");
            Console.WriteLine("\tSal");
            Console.WriteLine("\tPimienta\n\n");
            Console.WriteLine("Presiona ENTER para volver al supermercado...");
            ConsoleKeyInfo response = Console.ReadKey(true);
            while (response.Key != ConsoleKey.Enter)
            {
                response = Console.ReadKey(true);
            }
        }

        public void AddProduct(Product product)
        {
            Cart.Products.Add(product);
            OnLittleGuyTookProduct();
        }

        public void RemoveProduct(Product product)
        {
            Cart.Products.Remove(product);
        }

        public bool Pay()
        {
            bool continueP = OnLittleGuyWantsToPay();
            if (continueP == false) return false; 
            int total = 0;
            foreach (Product p in Cart.Products)
            {
                total += p.Price;
            }
            Console.WriteLine("El total de tu compra es: $" + total.ToString());
            Console.Write("Este programa se cerrará en ");
            for (int i = 5; i > 0; i--)
            {
                Console.Write(i.ToString() + " ");
                Thread.Sleep(1000);
            }
            ShopList = new List<Product>();
            Generatelist();
            Cart.Clear();
            return true;
        }

        public void ViewCart()
        {
            Console.WriteLine(Cart.ToString()); 
        }


        public bool LoadData()
        {
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cart.txt");
            if (!File.Exists(fileName))
            {
                return false;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open);
            IFormatter formatter = new BinaryFormatter();
            Cart = formatter.Deserialize(fs) as Cart;
            fs.Close();

            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shopList.txt");
            if (!File.Exists(fileName))
            {
                return false;
            }
            fs = new FileStream(fileName, FileMode.Open);
            ShopList = formatter.Deserialize(fs) as List<Product>;
            fs.Close();
            return true;
        }

        public void SaveData()
        {
            // Creamos el Stream donde guardaremos la informacion
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cart.txt");
            FileStream fs = new FileStream(fileName, FileMode.Create);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Cart);
            fs.Close();
            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shopList.txt");
            fs = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(fs, ShopList);
            fs.Close();
        }
    }
}
