using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case1.Этапы
{
    internal class WorkerWindow : SystemPart, IAppStep
    {
        public WorkerWindow(ProjectSystem s): base(s) {}
        public IAppStep Run()
        {
            Console.Clear();
            Console.WriteLine("Здравствуйте, {0}! Вы {1}.", system.CurrentUser.UserName, system.CurrentUser.Role);
            Console.WriteLine("Ваш список задач:");
            List<Task> workerTasks = system.CurrentWorkerTasks();
            for (int i = 0; i < workerTasks.Count; i++)
            {
                Console.WriteLine("{0} {1}", i, workerTasks[i]);
            }
            Console.WriteLine("Выберете задачу, статус которой Вы хотите изменить");
            Console.WriteLine("Чтобы закончить работу введите Q/q");
            try
            {
                string input = Console.ReadLine();
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) return new Register(system);
                int tIndex;
                if (int.TryParse(input, out tIndex))
                {

                    if ((system.Tasks.Count <= tIndex || tIndex < 0)) throw new Exception("Ошибка индекса! ");
                    Console.WriteLine("Выберете статус:");
                    Console.WriteLine("1 ToDo");
                    Console.WriteLine("2 InProgress");
                    Console.WriteLine("3 Done");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                        case "ToDo":
                            workerTasks[tIndex].Status = TaskStatus.ToDo;
                            system.SaveTasks();
                            break;

                        case "2":
                        case "InProgress":
                            workerTasks[tIndex].Status = TaskStatus.InProgress;
                            system.SaveTasks();
                            break;
                        case "3":
                        case "Done":
                            workerTasks[tIndex].Status = TaskStatus.Done;
                            system.SaveTasks();
                            break;
                        default:
                            throw new Exception("Некорректный ввод! ");
                    }
                }
                else throw new Exception("Некорректный ввод номера! ");
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
