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
            Console.WriteLine($"Курьер: {CourierName}, Контакт: {CourierContact}");
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

    class Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public TDelivery? Delivery = default;

        public int Number;

        public string Description = "";

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery?.Address ?? "Неизвестный адрес");
        }

        // ... Другие поля
    }
}
