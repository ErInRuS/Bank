using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Threading;

namespace Bank
{
    public class User
    {
        public int Id { get; set; }
        public double Money { get; private set; }
        public double GetMoney
        {
            get { return Money; }
        }
        public double PayMoney
        {
            set { Money += value; }
        }
        public string Password { private get; set; }
        public string GetPassword
        {
            get { return Password; }
        }
        public string SetPassword
        {
            set { Password = value; }
        }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; private set; }

    }
    public class Bank
    {
        private string[] menuList = { "Зарегестрироватся", "Авторизироватся", "Кабинет", "Список пользователей", "Сгенерировать пользователей" };
        private int select = 0;
        private int id = 0;
        private int actualId = 0;

        List<User> Users = new List<User>();

        private ConsoleColor colorConsole = ConsoleColor.Green;
        private ConsoleColor colorSelect = ConsoleColor.Yellow;
        public void MainMenu(string[] x, string[] y, string[] z)
        {
            SiginUp();
            Console.Clear();
            ConsoleKeyInfo key = Press();
            while (key.Key != ConsoleKey.Escape)
            {
                User actualUser = Users[actualId];
                Console.Clear();
                //debug
                //User actualUser = Users[actualId];
                Console.WriteLine($"Авторизирован: {actualUser.SubName} {actualUser.Name}");

                for (int i = 0; i < menuList.Length; i++)
                {
                    if (i == select)
                    {
                        Console.ForegroundColor = colorConsole;
                        Console.Write(">");
                        Console.ForegroundColor = colorSelect;
                        Console.WriteLine(menuList[i]);
                        Console.ResetColor();
                    }
                    else if (i == 4)
                    {
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(menuList[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.WriteLine(menuList[i]);
                    }
                }

                key = Console.ReadKey();

                switch (key.Key)
                {

                    case ConsoleKey.W:
                        if (select <= 0)
                        {
                            select = menuList.Length - 1;
                        }
                        else
                        {
                            select--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (select >= menuList.Length - 1)
                        {
                            select = 0;
                        }
                        else
                        {
                            select++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (select)
                        {
                            case 0:
                                SiginUp();
                                break;
                            case 1:
                                Authorization();
                                break;
                            case 2:
                                room();
                                break;
                            case 3:
                                ShowUsers();
                                break;
                            case 4:
                                GenerateUsers(x,y,z);
                                break;
                            default:
                                Press();
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        public void SiginUp()
        {
            User usr = new User();
            usr.Id = id;
            Console.Clear();
            Console.Write("Введите Имя: ");
            usr.Name = Console.ReadLine();
            Console.Write("Введите Фамилию: ");
            usr.SubName = Console.ReadLine();
            Console.Write("Введите Отчество: ");
            usr.LastName = Console.ReadLine();
            Console.Write("Придумайте пароль: ");
            usr.SetPassword = Console.ReadLine();

            Users.Add(usr);
            Console.WriteLine($"{usr.LastName} {usr.Name} {usr.SubName} , вы успешно зарегестрировались!");
            Press();
            id++;
        }
        public int[] Authorization()
        {
            Console.Clear();
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();
            bool exsist = false;
            int temporaryId = -1;

            foreach (var user in Users)
            {
                if (user.Name == name & user.GetPassword == password)
                {
                    exsist = true;
                    temporaryId = user.Id;
                    break;
                }
            }
            if (exsist == true)
            {
                Console.WriteLine($"Вы успешно вошли!");
            }
            else
            {
                Console.WriteLine("Профиля не существует!");
                temporaryId = actualId;
            }
            actualId = temporaryId;
            Press();
            return new int[] { 1, temporaryId };

        }
        public void ShowUsers()
        {
            Console.Clear();

            var str = new StringBuilder();
            str.Append(String.Format(" {0,4}  | {1,-45} | {2,-30}\n", "ID", "Name:", "Money:"));
            foreach (var user in Users)
            {
                string name = $"{user.LastName} {user.Name} {user.SubName}";
                str.Append(String.Format(" {0:0-0-0} | {1,-45} - {2,-30:N2}\n", user.Id, name, user.Money));
            }
            Console.WriteLine(str);
            Console.ReadKey();
        }
        ConsoleKeyInfo Press()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Нажмите любую кнопку");
            Console.ResetColor();
            ConsoleKeyInfo key = Console.ReadKey();
            return key;
        }
        void room()
        {
            Console.Clear();
            User actualUser = Users[actualId];
            Console.WriteLine($"ФИО: {actualUser.SubName} {actualUser.Name} {actualUser.LastName}");
            Console.WriteLine(string.Format("Сосотяние: {0:N2}р", actualUser.Money));
            int selectID = 0;
            string[] operations = { "Перевести", "Пополнить баланс" };
            ConsoleKeyInfo key = Press();
            while (key.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine($"ФИО: {actualUser.SubName} {actualUser.Name} {actualUser.LastName}");
                Console.WriteLine(string.Format("Сосотяние: {0:N2}р", actualUser.Money));

                for (int i = 0; i < operations.Length; i++)
                {
                    if (i == selectID)
                    {
                        Console.ForegroundColor = colorConsole;
                        Console.Write(">");
                        Console.ForegroundColor = colorSelect;
                        Console.WriteLine(operations[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.WriteLine(operations[i]);
                    }
                }

                key = Console.ReadKey();

                switch (key.Key)
                {

                    case ConsoleKey.W:
                        if (selectID <= 0)
                        {
                            selectID = operations.Length - 1;
                        }
                        else
                        {
                            selectID--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (selectID >= operations.Length - 1)
                        {
                            selectID = 0;
                        }
                        else
                        {
                            selectID++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (selectID)
                        {
                            case 0:
                                pay();
                                break;
                            case 1:
                                BalanceUp();
                                break;
                            default:
                                Press();
                                break;
                        }
                        break;
                    default:
                        break;
                }


            }
        }
        void BalanceUp()
        {
            Console.WriteLine("Введите сумму: ");
            double sum = double.Parse(Console.ReadLine());
            User usr = null;
            foreach (var user in Users)
            {
                if (user.Id == actualId)
                {
                    usr = user;
                }
            }
            if (sum < 0)
            {
                Console.WriteLine("Ошибка ввода!");
                Console.WriteLine("Вы ввели отрицательное значение");
                Press();

            } else
            {
                usr.PayMoney = sum;
            }
        }
        void pay()
        {
            Console.WriteLine("Введите ID зачесления: ");
            double idOutPay = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите сумму: ");
            double summ = double.Parse(Console.ReadLine());
            User user1 = null;
            User user2 = null;

            foreach (var user in Users)
            {
                if(user.Id == idOutPay)
                {
                    user2 = user;
                } else if (user.Id == actualId)
                {
                    user1 = user;
                }
            }
            if(summ < 0)
            {
                Console.WriteLine("Ошибка ввода!");
                Console.WriteLine("Вы ввели отрицательное значение");
                Press();
                return;
            } else if(user1.Money - summ < 0)
            {
                Console.WriteLine("У вас недостаточно средств!");
                Press();
                return;
            } else
            {
                user1.PayMoney = -summ;
                user2.PayMoney = summ;
            }
        }
        void GenerateUsers(string[] name, string[] subName, string[] lastName)
        {
            Console.WriteLine("Введите количество: ");
            int num = int.Parse(Console.ReadLine());
            Random rand = new Random();
            for (int i = 0; i < num; i++)
            {
                User usr = new User();
                usr.Id = id;
                usr.Name = name[rand.Next(0,name.Length)];
                usr.SubName = subName[rand.Next(0, subName.Length)];
                usr.LastName = lastName[rand.Next(0, lastName.Length)];
                usr.PayMoney = rand.Next(1,1000000) + rand.NextDouble();
                Users.Add(usr);
                id++;
            }
            Console.ForegroundColor = colorSelect;
            Console.WriteLine("Успешно!");
            Console.ResetColor();
        }

    }
}