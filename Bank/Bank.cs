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
        private string[] menuList = { "Зарегестрироватся", "Авторизироватся", "Кабинет", "Список пользователей" };
        private int select = 0;
        private int id = 0;
        private int actualId = 0;

        List<User> Users = new List<User>();

        private ConsoleColor colorConsole = ConsoleColor.Green;
        private ConsoleColor colorSelect = ConsoleColor.Yellow;
        public void MainMenu()
        {
            SiginUp();
            ConsoleKeyInfo key = Console.ReadKey();
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
                            case 2:
                                room();
                                break;
                            case 3:
                                showUsers();
                                break;
                            default:

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
            Console.WriteLine(usr.Money);
            Press();
            id++;
        }
        public int[] Authorization()
        {
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
            }
            actualId = temporaryId;
            Press();
            return new int[] { 1, temporaryId };

        }
        public void showUsers()
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
            int selectID = 0;
            string[] operations = { "Пополнить баланс", "Перевести" };
            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                User actualUser = Users[actualId];
                Console.WriteLine($"ФИО: {actualUser.SubName} {actualUser.Name} {actualUser.LastName}");
                Console.WriteLine(string.Format("Сосотяние: {0:N2}р", actualUser.Money));

                for (int i = 0; i < operations.Length; i++)
                {
                    if (i == selectID)
                    {
                        Console.ForegroundColor = colorConsole;
                        Console.Write(">");
                        Console.ForegroundColor = colorSelect;
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
                                SiginUp();
                                break;
                            default:

                                break;
                        }
                        break;
                    default:
                        break;
                }


            }
        }
        void pay(int id1, int id2)
        {
            Console.WriteLine("Введите сумму: ");
            double num = double.Parse(Console.ReadLine());
            double balance1, balance2;
            User user1, user2 = null;
            foreach (var user in Users)
            {
                if (user.Id == id1)
                    user1 = user;
                if (user.Id == id2)
                    user2 = user;
            }
        }


    }
}