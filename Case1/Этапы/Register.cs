using Case1.Этапы;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case1
{
    internal class Register :SystemPart, IAppStep
    {
        public Register(ProjectSystem s) : base(s) { }
        public IAppStep Run()
        {
            Console.Clear();
            Console.Write("Введите логин и пароль\nЛогин:");
            string login = Console.ReadLine();
            StringBuilder password = new StringBuilder();
            Console.Write("Пароль:");
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.Escape) return null;
                if (input.Key == ConsoleKey.Enter) break;
                if (input.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0) password.Remove(password.Length - 1, 1);
                }
                else if (input.KeyChar != '\0') password.Append(input.KeyChar);
            }
            Console.WriteLine();
            system.CurrentUser = system.Users.Find(x => x.UserName == login);
            if (system.CurrentUser == null || password.ToString() != system.CurrentUser.Password)
            {
                system.ErrorMessage("Неправильный логин или пароль!");
                return this;
            }
            if (system.CurrentUser.Role == Role.Worker) return new WorkerWindow(system);
            if (system.CurrentUser.Role == Role.Administrator) return new AdministratorWindow(system);
            return this;
        }
    }
}
