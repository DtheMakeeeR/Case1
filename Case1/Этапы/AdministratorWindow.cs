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
            Console.WriteLine("Список всех сотрудников:");
            for (int i = 0; i < system.Users.Count; i++)
            {
                Console.WriteLine("{0} {1}", i, system.Users[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Список всех задач:");
            for (int i = 0; i < system.Tasks.Count; i++)
            {
                Console.WriteLine("Номер: {0} {1}", i, system.Tasks[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Чтобы выбрать задачу, на которую вы хотите назначить сотрудника введите ее номер");
            Console.WriteLine("Чтобы добавить новую задачу введите T/t");
            Console.WriteLine("Чтобы зарегистрировать новго сотрудника введите W/w");
            Console.WriteLine("Чтобы закончить работу введите Q/q");
            try
            {
                int tIndex;
                string input = Console.ReadLine();
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) return new Register(system);
                else if (input.Equals("W", StringComparison.OrdinalIgnoreCase))
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
                else if (input.Equals("T", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("ID проекта и название задачи должны быть уникальной парой");
                    Console.Write("ID проекта:");
                    string pId = Console.ReadLine();
                    Console.Write("\nНазвание задачи:");
                    string name = Console.ReadLine();
                    if (system.IsUniqueTask(pId, name))
                        throw new Exception("ID проекта и название задачи должны быть уникальной парой! ");
                    Console.Write("\nОписание задачи:");
                    string description = Console.ReadLine();
                    Console.Write("\n(ToDo, InProgress, Done)");
                    Console.Write("\nСтатус задачи:");
                    TaskStatus ts;
                    string status = Console.ReadLine();
                    if (!Enum.TryParse(status, out ts)) throw new Exception("Такого статуса нет! ");
                    system.AddTask(new Task { ProjectId = pId, Name = name, Description = description, Status = ts });
                }
                else if (int.TryParse(input, out tIndex))
                {
                    if ((system.Tasks.Count <= tIndex || tIndex < 0)) throw new Exception("Ошибка индекса! ");
                    Console.Write("Впишите логин сотрудника:");
                    input = Console.ReadLine();
                    system.AssingUser(tIndex, input);
                }

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
