using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_CS_007
{
    abstract class Delivery
    {
        public string Address = "";

    }

    class HomeDelivery : Delivery
    {
        /* ... */
    }

    class PickPointDelivery : Delivery
    {
        /* ... */
    }

    class ShopDelivery : Delivery
    {
        /* ... */
    }

    class Order<TDelivery> where TDelivery : Delivery?
    {
        public TDelivery? Delivery = default;

        public int Number;

        public string Description = "";

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery?.Address ?? "Неизвестный адрес");
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выполнение программы завершено.");
        }
    }
}
