using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace big_sister_base
{
    [Serializable]
    public class Market
    {
        private List<Product> storage;

        public Market()
        {
            if (!LoadStorage())
            {
                SupplyStore();
            }
        }
        public List<Product> Storage { get => storage; private set => storage = value; }


        public void SupplyStore()
        {
            Storage = new List<Product>();
            Storage.Add(new Product("Leche Entera", 820, 89, "1L"));
            Storage.Add(new Product("Gomitas Flipy", 720, 12, "100g"));
            Storage.Add(new Product("Mantequilla", 850, 12, "125g"));
            Storage.Add(new Product("Crema para hemorroides", 4990, 7, "300cc"));
            Storage.Add(new Product("Pimienta", 430, 84, "15g"));
            Storage.Add(new Product("Vino Sauvignon Blanc Reserva Botella", 4150, 23, "750cc"));
            Storage.Add(new Product("Sal Lobos", 330, 150, "1kg"));
            Storage.Add(new Product("Cuaderno Mi Pequeño Pony", 1290, 50, "1un"));
            Storage.Add(new Product("Láminas de Lasaña", 1250, 85, "400g"));
            Storage.Add(new Product("Tomate", 1290, 200, "1kg"));
            Storage.Add(new Product("Harina", 890, 43, "1kg"));
            Storage.Add(new Product("Audifonos Samsung", 5990, 40, "1un"));
            Storage.Add(new Product("Pisco Alto del Carmen", 5990, 120, "1L"));
            Storage.Add(new Product("Carne Molida", 4390, 15, "500g"));
            Storage.Add(new Product("Aceite de Oliva", 1790, 77, "250g"));
            Storage.Add(new Product("Sal parrillera", 840, 50, "750g"));
            Storage.Add(new Product("Cable HDMI 1m", 3990, 25, "1un"));
            Storage.Add(new Product("Queso Rallado Parmesano", 499, 102, "40g"));
            Storage.Add(new Product("Vino Blanco Caja", 2790, 84, "2L"));
            Storage.Add(new Product("Malla de Cebollas", 1090, 91, "1kg"));
            Storage.Add(new Product("Tomates Pelados en lata", 700, 48, "540g"));
            Storage.Add(new Product("Queso Parmesano", 3790, 41, "200g"));
            Storage.Add(new Product("Bolsa de Zanahorias", 890, 74, "1un"));
        }

        public void AddStorage(int index)
        {
            Storage[index].Stock += 1;
        }
        public void removeStorage(int index)
        {
            Storage[index].Stock -= 1;
        }

        public void SaveStorage()
        {
            // Creamos el Stream donde guardaremos la informacion
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "market.txt");
            FileStream fs = new FileStream(fileName, FileMode.Create);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Storage);
            fs.Close();
        }

        public bool LoadStorage()
        {
            String fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "market.txt");
            if (!File.Exists(fileName))
            {
                return false;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open);
            IFormatter formatter = new BinaryFormatter();
            Storage = formatter.Deserialize(fs) as List<Product>;
            fs.Close();
            return true;
        }
    }
}
