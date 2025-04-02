using Case1.Этапы;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Case1
{
    internal class AdministratorUsersWindow : SystemPart, IAppStep
    {
        public AdministratorUsersWindow(ProjectSystem s) : base(s)
        {
        }

        public IAppStep Run()
        {
            Console.Clear();
            Console.WriteLine("Здравствуйте, {0}! Вы {1}.", system.CurrentUser.UserName, system.CurrentUser.Role);
            Console.WriteLine("Список всех сотрудников:");
            for (int i = 0; i < system.Users.Count; i++)
            {
                Console.WriteLine("{0} {1}", i, system.Users[i]);
            }
            Console.WriteLine("Чтобы зарегистрировать новго сотрудника введите C/c");
            Console.WriteLine("Чтобы вернуться введите B/b");
            Console.WriteLine("Чтобы посмотреть задачи введите T/t");
            Console.WriteLine("Чтобы закончить работу введите Q/q");
            try
            {
                string input = Console.ReadLine();
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) return new Register(system);
                if (input.Equals("B", StringComparison.OrdinalIgnoreCase)) return new AdministratorWindow(system);
                if (input.Equals("T", StringComparison.OrdinalIgnoreCase)) return new AdministratorTaskWindow(system);
                if (input.Equals("C", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Логин сотрудника должен быть уникальным");
                    Console.Write("\nЛогин сотрудника:");
                    string uName = Console.ReadLine();
                    if (!system.IsUniqueName(uName)) throw new Exception("Логин сотрудника должен быть уникальным! ");
                    Console.Write("\nПароль сотрудника:");
                    string uPassowrd = Console.ReadLine();
                    Console.Write("\n(Worker, Administrator)");
                    Console.Write("\nРоль сотрудника:");
                    Role uRole;
                    string role = Console.ReadLine();
                    if (!Enum.TryParse(role, out uRole)) throw new Exception("Такой роли нет! ");
                    system.AddUser(new User { UserName = uName, Password = uPassowrd, Role = uRole });
                }
                else throw new Exception("Некорректный ввод! ");
            }
            catch (Exception e)
            {
                system.ErrorMessage(e.Message);
                return this;
            }
            return this;
        }
    }
}