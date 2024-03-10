using System;

namespace _09._03._2024
{
#region Метод расширения
//Использование методов расширения Для получения 50% скинки на товар. 
    static class CostExtention
    {
        public static decimal GetCostExtention(this decimal source)
        {
            return source / 2;
        }
    }
#endregion

    class Program
    {

        static void Main()
        {
            //Пример работы с перегрузкой оператора +.
            PickPointDelivery.Point point = new PickPointDelivery.Point(6, 6);        //Растояние по X и Y до пункта выдачи.
            PickPointDelivery.Point pointDeliv = new PickPointDelivery.Point(-1, -1); //Координаты курьера.
          
            PickPointDelivery.Point pointRes = point + pointDeliv;

            int step = 0;
            string str = pointRes.ToString();
            while (str != "[0, 0]")                //Движеник курьера к точке выдачи, пока координаты не будут [0, 0].
            {
                pointRes = pointRes + pointDeliv;
                Console.WriteLine(pointRes);
                step++;
                str = pointRes.ToString();
            }
              Console.WriteLine("Курьер передал заказ в точку выдачи товара!");
              Console.WriteLine("Курьер сделал: {0} шагов", step);
            Console.WriteLine(new string('-', 20));
            //**************************************************************************************************************
            //Реализация наследования обобщений; Телефон магазина типом int(TelShop = 123244) и string (TelShop = "123244")
            ShopDelivery<int> shop1 = new ShopDelivery<int> { GetShop = true, TelShop = 123244, Address = "Волгоград" };
            ShopDelivery<string> shop2 = new ShopDelivery<string> { GetShop = true, TelShop = "123244", Address = "Волгоград" };

            //Реализация наследования обобщений;
            Sklad<int> sklad1 = new Sklad<int> { Address = "Казань", Property = 1 };
            Sklad<string> sklad2 = new Sklad<string> { Address = "Казань", Property = "Склад закрыт на ремонт" };
            //*********************************************************************************************************************

            //Для метода расширения  получение 50% скинки на товар
            //И использование агрегации классов. Каждая материнская карта имеет Процессор CPU.
            CPU cpu = new CPU { NameProduct = "Intel", SpeedMhz = 3500};

            Console.WriteLine("Метод расширения");
            MotherBoard motherExtention = new MotherBoard(cpu);
            motherExtention.Cost = 100;
            motherExtention.NameProduct = "Asus";
            motherExtention.Socket = "2011-A";
            Console.WriteLine("Было: {0}", motherExtention.Cost);
            Console.WriteLine("Стало:{0}",motherExtention.Cost.GetCostExtention());
            Console.WriteLine(new string('-', 20));
            motherExtention.DisplayCPU();
            Console.WriteLine(new string('-', 20));

            //************************************************************************************************
            //Создание экземпляра Класса Клиент.
            Console.WriteLine("Сформирован заказ:");
            Client instance = new Client("client@client.ru", "Иванов", "Иван", 0123456789);
            instance.Method();
            //************************************************************************************************
            //Создание экземпляра Класса доставки на дом.
            HomeDelivery homeDelivery = new HomeDelivery();
            homeDelivery.Address = "г.Москва, ул.Пушкина, Дом:1";
            homeDelivery.GetDelivery = true; //Товар выдан курьеру.
            homeDelivery.ArrivalDate = new DateTime(2024, 3, 8); //Дата доставки.
            //************************************************************************************************
            //Создание экземпляра Класса Заказ.
            Order<HomeDelivery, Client> order = new Order<HomeDelivery, Client>();
            order.Number = 1;
            order.Description = "Комплектующие для ПК";
            order.Delivery = homeDelivery; //Доставка на дом.
            order.tclass = instance; //Клиент.
            //************************************************************************************************
            //Создание экземпляра товара №1
            MotherBoard mother = new MotherBoard(cpu);
            mother.Cost = 100;
            mother.NameProduct = "Asus";
            mother.Socket = "2011-A";
            //************************************************************************************************
            //Создание экземпляра товара №2
            MotherBoard mother2 = new MotherBoard(cpu);
            mother2.Cost = 300;
            mother2.NameProduct = "Acer";
            mother2.Socket = "2011-A2";
            //************************************************************************************************
            //Создание экземпляра товара №3
            GraphicCard graphic = new GraphicCard();
            graphic.Cost = 2000;
            graphic.NameProduct = "GeForce";
            graphic.MemoryGraphicard = 8;
            //************************************************************************************************
            Console.WriteLine("Адрес доставки: {0}", order.Delivery.Address); //order.DisplayAddress();
            Console.WriteLine("Товар передан курьеру: {0}", order.Delivery.GetDelivery);
            Console.WriteLine("Дата доставки: {0}", order.Delivery.ArrivalDate);
            Console.WriteLine("Номер заказа: {0}", order.Number);
            Console.WriteLine("Описание заказа: {0}", order.Description);
            Console.WriteLine("Фамилия Клиента: {0}", order.tclass.FirstName);
            Console.WriteLine("Имя клиента: {0}", order.tclass.LastName);
            Console.WriteLine("Телефонный номер: {0}", order.tclass.TelNumber);
            Console.WriteLine("Электронная почта: {0}", order.tclass.Email);          
            Console.WriteLine(new string('-', 20));

//Используем ИНКАПСУЛЯЦИЮ для сокрытия типов данных с помощью VAR.

            //Создание массива и добавление товара в массив.
            var arry = order.AddinMassivProduct(mother);
            Console.WriteLine("Материнская плата: {0}", arry[0].NameProduct);

            arry = order.AddinMassivProduct(mother2);
            Console.WriteLine("Материнская плата: {0}", arry[1].NameProduct);

            arry = order.AddinMassivProduct(graphic);
            Console.WriteLine("Видеокарта: {0}", arry[2].NameProduct);

            //Вызов обобщенного метода**********************************************
            Console.WriteLine("Стоимость материнской платы №1:");
            order.Display<MotherBoard>(mother);
            Console.WriteLine("Стоимость материнской платы №2:");
            order.Display<MotherBoard>(mother2);
            Console.WriteLine("Стоимость Видеокарты №3:");
            order.Display<GraphicCard>(graphic);
            Console.WriteLine(new string('-', 20));
            //Итоговая сумма заказа используя СТАТИЧЕСКУЮ переменную****************
            decimal ResCost = order.SumProductCost(arry);
            Console.WriteLine("Итогова сумма заказа с учётом скидки: {0}", ResCost);
//********************************************************************** 
            #region Работа с Индексатором
            var array = new MotherBoard[]
            {
                new MotherBoard(cpu)
                {
                     NameProduct = "ASUS",
                     Socket = "2011-A",
                     Cost = 200
                },
                new MotherBoard(cpu)
                {
                     NameProduct = "Gigabit",
                     Socket = "2012-A",
                     Cost = 300
                }
            };

            BuyCollection collection = new BuyCollection(array);
            MotherBoard motherBoard = collection[1];
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Вывод из работы Индексатора");
            Console.WriteLine(motherBoard.NameProduct);
            #endregion
//**********************************************************************

            Console.ReadKey();
        }
        #region Persons
        class Person
        {
            public string FirstName;
            public string LastName;
            public int TelNumber;

            //Конструктор с двумя параметрами.
            public Person(string FirstName, string LastName)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            //Конструктор с 3 параметрами
            public Person(string FirstName, string LastName, int TelNumber)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.TelNumber = TelNumber;
            }

            //Конструктор с 1 параметром. Вызов через this конструктор с двумя параметрами.
            public Person(int TelNumber) : this("Не указано", "Не указано")
            {
                this.TelNumber = TelNumber;

            }
            public virtual void Method()
            {
                Console.WriteLine("{0}, {1}, {2}", FirstName, LastName, TelNumber);
            }

        }
        class Client : Person
        {
            private string email;

            public string Email
            {
                get
                {
                    return email;
                }
                set
                {
                    //Проверка есть ли в Email значек "@".
                    if (value.Contains("@") == true)
                    {
                        email = value;

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Это не является электронной почтой!");
                        email = "Не верно укзана почта";
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                }
            }

            //Используем НАСЛЕДОВАНИЕ, вызываем через base консруктор из базового класса с двумя параметрами.
            public Client(string email, string FirstName, string LastName) : base(FirstName, LastName)
            {
                this.Email = email;
            }
            public Client(string email, string FirstName, string LastName, int TelNumber) : base(FirstName, LastName, TelNumber)
            {
                this.Email = email;
            }

            //Переопределение метода Method().
            public override void Method()
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", FirstName, LastName, email, TelNumber);
            }
        }
        #endregion

        #region Product
        //Абстрактный класс Товар.
        abstract class Product
        {
            public string NameProduct;
            public decimal Cost;
            
        }
        class ComputerPart : Product
        {
            protected string SerialNum;
           
            public virtual void Work() { }   //Виртуальный метод.
        }
        class GraphicCard : ComputerPart
        {
            public int MemoryGraphicard;

            public new void Work() { }        //Перекрытие метода.
        }

        class CPU : ComputerPart
        {
            public int SpeedMhz;
            public override void Work() { }  //Переопределений метода.
        }
        //Использование АГРЕГАЦИИ: Материнская плата "имеет" CPU.
        class MotherBoard : ComputerPart
        {
            private CPU cpu;
            public string Socket;
            public MotherBoard(CPU cpu)
            {
                this.cpu = cpu;
                
            }

            public void DisplayCPU()
            {
                Console.WriteLine("Название процессора: {0}, Частота: {1}", cpu.NameProduct, cpu.SpeedMhz);
            }
          
            public override void Work() { }  //Переопределений метода.


        }
        //Использование КОМПОЗИЦИИ: Системный блок "имеет" Видеокарту.
        class SystemBox : ComputerPart
        {
            private GraphicCard graphicCard;

            public SystemBox()
            {
                graphicCard = new GraphicCard();
            }
            public override void Work() { }   //Переопределение метода.
        }

        #endregion

        #region Delivery
        abstract class Delivery
        {
            public string Address;

            public DateTime ArrivalDate; // Дата и время выдачи заказа.
        }

        class HomeDelivery : Delivery
        {
            public bool GetDelivery;     //Получение курьером заказ.
        }

        class PickPointDelivery : Delivery
        {
            public bool GetPickPoint;    //Поступление заказа в пункт выдачи.

            //Структура Point для доставки и перегрузки оператора.
            public struct Point
            {
                //Координаты точки.
                private int x, y;

                //Конструктор
                public Point(int xPos, int yPos)
                {
                    x = xPos;
                    y = yPos;
                }

                //Используем перегрузку оператора +.  //Для определения прибытия курьера к точке выдачи.
                public static Point operator +(Point p1, Point p2)
                {
                    return new Point(p1.x + p2.x, p1.y + p2.y);
                }
                //Переопределим метод ToString для вывода координат по X и Y.
                public override string ToString()
                {
                    return string.Format("[{0}, {1}]", this.x, this.y);
                }
            }

             
        }

        class ShopDelivery<T> : Delivery
        {
            public bool GetShop;         //Поступление заказа в магазин.
            public T TelShop;            //Телефон магазина будет с типом int и string
        }
        class Sklad<T> : ShopDelivery<T>
        {
            public T Property { get; set; }
        }

        #endregion

        #region Order
        class Order<TDelivery, TClass> where TDelivery : Delivery where TClass : Person
        {
            public TDelivery Delivery;
            public TClass tclass;

            //Использование статического поля
            public static int Sale = 5; //Скидка всем покупателям 5% на заказ.

            int i = 0;
            Product[] mass = new Product[3];    //Массив товаров. 

            public Product[] AddinMassivProduct(Product arg)
            {
                mass[i] = arg;
                i++;
                return mass;
            }

            public int Number;
            public string Description;

//Использование обобщенного метода для вывода стоимости товара
            public void Display<T>(T param) where T : Product
            {
                Console.WriteLine(param.Cost);
            }
//************************************************************
            public void DisplayAddress()
            {
                Console.WriteLine(Delivery.Address);
            }



            //Подсчёт суммы заказа.
            public decimal SumProductCost(Product[] arry)
            {
                decimal allcost = 0;
                for (int i = 0; i < mass.Length; i++)
                    allcost += mass[i].Cost;

                return allcost - allcost * Sale / 100;    //Итоговая сумма / на скидку 5 % от заказа

            }

        }
        #endregion

        #region Индексатор
        class BuyCollection
        {
            // Закрытое поле, хранящее Материнских карт в виде массива
            private MotherBoard[] collection;

            // Конструктор с добавлением массива Материнских плат
            public BuyCollection(MotherBoard[] collection)
            {
                this.collection = collection;
            }

            // Индексатор по массиву
            public MotherBoard this[int index]
            {
                get
                {
                    // Проверяем, чтобы индекс был в диапазоне для массива
                    if (index >= 0 && index < collection.Length)
                    {
                        return collection[index];
                    }
                    else
                    {
                        return null;
                    }
                }
                private set
                {
                    // Проверяем, чтобы индекс был в диапазоне для массива
                    if (index >= 0 && index < collection.Length)
                    {
                        collection[index] = value;
                    }
                }

            }
        }
#endregion
    }
}
