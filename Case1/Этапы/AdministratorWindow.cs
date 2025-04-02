using Case1.Этапы;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case1
{
    internal class AdministratorWindow : SystemPart, IAppStep
    {
        public AdministratorWindow(ProjectSystem s) : base(s) { }

        public IAppStep Run()
        {
            Console.Clear();
            Console.WriteLine("Здравствуйте, {0}! Вы {1}.", system.CurrentUser.UserName, system.CurrentUser.Role);
            Console.WriteLine("1 Посмотреть список задач");
            Console.WriteLine("2 Посмотреть список сотрудников");
            Console.WriteLine("Выберете операцию");
            Console.WriteLine("Чтобы закончить работу введите Q/q");
            try
            {
                string input = Console.ReadLine();
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) return new Register(system);
                switch (input)
                {
                    case "1":
                        return new AdministratorTaskWindow(system);
                    case "2":
                        return new AdministratorUsersWindow(system);
                    default:
                        throw new Exception("Некорректный ввод! ");
                }
            }
            catch (Exception ex)
            {
                system.ErrorMessage(ex.Message);
                return this;
            }
        }
    }
}
