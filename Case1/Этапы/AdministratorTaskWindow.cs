using Case1;
using Case1.Этапы;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Case1
{
    internal class AdministratorTaskWindow : SystemPart, IAppStep
    {
        public AdministratorTaskWindow(ProjectSystem s) : base(s)
        {
        }

        public IAppStep Run()
        {

            Console.Clear();
            Console.WriteLine("Здравствуйте, {0}! Вы {1}.", system.CurrentUser.UserName, system.CurrentUser.Role);
            Console.WriteLine("Список всех задач:");
            for (int i = 0; i < system.Tasks.Count; i++)
            {
                Console.WriteLine("Номер: {0} {1}", i, system.Tasks[i]);
            }
            Console.WriteLine("Чтобы выбрать задачу, на которую вы хотите назначить сотрудника введите ее номер");
            Console.WriteLine("Чтобы добавить новую задачу введите C/c");
            Console.WriteLine("Чтобы вернуться введите B/b");
            Console.WriteLine("Чтобы посмотреть сотрудников введите W/w");
            Console.WriteLine("Чтобы закончить работу введите Q/q");
            try
            {
                string input = Console.ReadLine();
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) return new Register(system);
                if (input.Equals("B", StringComparison.OrdinalIgnoreCase)) return new AdministratorWindow(system);
                if (input.Equals("W", StringComparison.OrdinalIgnoreCase)) return new AdministratorUsersWindow(system);
                if (input.Equals("C", StringComparison.OrdinalIgnoreCase))
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
                int tIndex;
                if (int.TryParse(input, out tIndex))
                {
                    if ((system.Tasks.Count <= tIndex || tIndex < 0)) throw new Exception("Ошибка индекса! ");
                    system.Tasks[0] = new Task();
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