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
        public string Address = "Адрес не указан"; // Адрес доставки (дома, пункта выдачи, магазина для самовывоза)
        public DateTime DeliveryDate; // дата доставки
        public decimal Cost; // стоимость доставки

        public abstract void DisplayDeliveryInfo(); // информация о доставке, для каждого вида доставки варинт вывода информации будет свой
    }

    class HomeDelivery : Delivery
    {
        public string CourierName = "Не указан"; // ФИО курьера
        public string CourierContact = "Не указан"; // Контактный номер телефона курьера

        public override void DisplayDeliveryInfo()
        {
            Console.WriteLine($"Доставка на дом по адресу: {Address}");
            Console.WriteLine($"Курьер: {CourierName}, телефон: {CourierContact}");
            Console.WriteLine($"Дата доставки: {DeliveryDate}");
            Console.WriteLine($"Стоимость: {Cost}.");
        }
    }

    class PickPointDelivery : Delivery
    {
        public string PickPointName = "Не указан"; // Название пункта выдачи

        public override void DisplayDeliveryInfo()
        {
            Console.WriteLine($"Доставка в пункт выдачи: {PickPointName}");
            Console.WriteLine($"Адрес пункта выдачи: {Address}");
            Console.WriteLine($"Дата доставки: {DeliveryDate}");
        }
    }

    class ShopDelivery : Delivery
    {
        public string ShopName = "Не указан"; // Название магазина
        public string ShopOpeningHours = "Не указан"; // Часы работы магазина

        public override void DisplayDeliveryInfo()
        {
            Console.WriteLine($"Самовывоз из магазина: {ShopName}, часы работы: {ShopOpeningHours}");
            Console.WriteLine($"Адрес магазина: {Address}");
        }
    }

    class Order<TDelivery> where TDelivery : Delivery
    {
        public TDelivery? Delivery = default;
        public int Number = 0;
        public string Description = "";

        public void DisplayOrderInfo()
        {
            Console.WriteLine($"Номер заказа: {Number}");
            Console.WriteLine($"Описание: {Description}");
            Delivery?.DisplayDeliveryInfo();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var homeDelivery = new HomeDelivery
            {
                Address         = "ул. Улица, д. 1, кв. 2",
                DeliveryDate    = DateTime.Now.AddDays(3),
                Cost            = 1_000,
                CourierName     = "Иван Иванов",
                CourierContact  = "+7 777 666 55 44"
            };

            var order = new Order<HomeDelivery>
            {
                Number      = 1,
                Description = "Заказ техники",
                Delivery    = homeDelivery
            };

            order.DisplayOrderInfo();

            Console.WriteLine("Выполнение программы завершено");
            Console.ReadKey();
        }
    }

}
