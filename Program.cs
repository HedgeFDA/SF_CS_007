using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SF_CS_007
{
    abstract class Delivery
    {
        public string Address = "";     // Адрес доставки (дома, пункта выдачи, магазина для самовывоза)
        public DateTime DeliveryDate;   // дата доставки
        public decimal Cost = 0;        // стоимость доставки

        public abstract void DisplayDeliveryInfo(); // информация о доставке, для каждого вида доставки варинт вывода информации будет свой
    }

    class HomeDelivery : Delivery
    {
        public string CourierName = "Не указан";    // ФИО курьера
        public string CourierContact = "Не указан"; // Контактный номер телефона курьера

        public override void DisplayDeliveryInfo()
        {
            Console.WriteLine("Доставка на дом по адресу: {0}", Address);
            Console.WriteLine("Курьер: {0}, номер телефона: {1}", CourierName, CourierContact);
            Console.WriteLine("Дата доставки: {0}", DeliveryDate);
            Console.WriteLine("Стоимость доставки: {0}", Cost);
        }
    }

    class PickPointDelivery : Delivery
    {
        public string PickPointName = "Не указан"; // Название пункта выдачи

        public override void DisplayDeliveryInfo()
        {
            Console.WriteLine("Доставка в пункт выдачи: {0}", PickPointName);
            Console.WriteLine("Адрес пункта выдачи: {0}", Address);
            Console.WriteLine("Дата доставки: {0}", PickPointName);
        }
    }

    class ShopDelivery : Delivery
    {
        public string ShopName = "Не указан";           // Название магазина
        public string ShopOpeningHours = "Не указан";   // Часы работы магазина

        public override void DisplayDeliveryInfo()
        {
            Console.WriteLine("Самовывоз из магазина: {0}, часы работы: {1}", ShopName, ShopOpeningHours);
            Console.WriteLine("Адрес магазина: {0}", Address);
        }
    }

    class Product
    {
        public string Name;     // Название товара
        public decimal Price;   // Цена
        public int Quantity;    // Количество

        public Product() // Конструктор 0
        {
            Name = "";
            Price = 0;
            Quantity = 1;
        }

        public Product(string _Name) // Конструктор 1
        {
            Name = _Name;
            Price = 0;
            Quantity = 1;
        }

        public Product(string _Name, decimal _Price) // Конструктор 2
        {
            Name = _Name;
            Price = _Price;
            Quantity = 1;
        }

        public Product(string _Name, decimal _Price, int _Quantity) // Конструктор 3
        {
            Name = _Name;
            Price = _Price;
            Quantity = _Quantity;
        }

        public virtual void Describe()
        {
            Console.WriteLine("Товар {0}, кол. {1}, цена {2}, сумма: {3}", Name, Quantity, Price, (Quantity * Price));
        }
    }

    class Service : Product
    {
        public bool IsAService = true;

        public override void Describe()
        {
            Console.WriteLine("Услуга {0}, кол. {1}, цена {2}, сумма: {3}", Name, Quantity, Price, (Quantity * Price));
        }

        public Service() : base()
        {
            Quantity = 1;
        }

        public Service(string _Name) : base(_Name) // Конструктор 1
        {
            Quantity = 1;
        }

        public Service(string _Name, decimal _Price) : base(_Name, _Price) // Конструктор 2
        {
            Quantity = 1;
        }
    }

    class Order<TDelivery> where TDelivery : Delivery
    {
        public TDelivery? Delivery = default;                   // Тип доставки
        public int Number = 0;                                  // Номер заказа
        public string Description = "";                         // Описание заказа
        public List<Product> Products = new List<Product>();    // Список позиций в заказе

        public void DisplayOrderInfo()
        {
            Console.WriteLine("Номер заказа: {0}", Number);
            Console.WriteLine("Описание: {0}", Description);

            Console.WriteLine("");
            if (Delivery != null)
            {
                Delivery.DisplayDeliveryInfo();
            }
            else
            {
                Console.WriteLine("Доставка: Нет информации");
            }

            decimal OrderAmount = Delivery?.Cost ?? 0;

            if (Products != null)
            {
                Console.WriteLine("\nСодержание заказа:");

                foreach (var product in Products)
                {
                    product.Describe();

                    OrderAmount += product.Price * product.Quantity;
                }

                Console.WriteLine("Всего заказано позиций: {0}", Products.Count);
            }
            Console.WriteLine("\nОбщая сумма заказа с учетом доставки: {0}", OrderAmount);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var homeDelivery = new HomeDelivery
            {
                Address = "ул. Улица, д. 1, кв. 2",
                DeliveryDate = DateTime.Now.AddDays(3),
                Cost = 1_000,
                CourierName = "Иван Иванов",
                CourierContact = "+7 777 666 55 44"
            };

            var order = new Order<HomeDelivery>
            {
                Number = 1,
                Description = "Заказ товаров",
                Delivery = homeDelivery
            };

            order.Products.Add(new Product("Телевизор", 1_000));
            order.Products.Add(new Product("Холодильник", 2_000, 2));
            order.Products.Add(new Product("Стиральная машина"));

            order.Products.Add(new Service("Установка", 3_000));

            order.DisplayOrderInfo();

            Console.WriteLine("\nВыполнение программы завершено.");
        }
    }

}
