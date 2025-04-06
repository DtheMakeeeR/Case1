using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Case1
{
    internal class ProjectSystem
    {
        private List<Task> tasksList = null;
        private List<User> usersList = null;
        private string userPath;
        private string taskPath;

        public User CurrentUser { get; set; }
        public List<User> Users { get => usersList; }
        public List<Task> Tasks { get => tasksList; }
        public ProjectSystem(string uPath, string tPath)
        {
            userPath = uPath;
            taskPath = tPath;
            LoadUsers();
            LoadTasks();
        }
        public void SaveUsers()
        {
            string tempPath = Path.GetTempFileName();
            XmlSerializer serializer = new XmlSerializer(typeof(UserList));
            try
            {
                using (FileStream stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                {
                    UserList uList = new UserList { Users = Users };
                    serializer.Serialize(stream, uList);
                }
                File.Copy(tempPath, userPath, true);
            }
            catch
            {
                ErrorMessage($"Нет файла {userPath}. ");
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }
        public void SaveTasks()
        {
            string tempPath = Path.GetTempFileName();
            XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
            try
            {
                using (FileStream stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                {
                    TaskList taskList = new TaskList { Tasks = Tasks };
                    serializer.Serialize(stream, taskList);
                }
                File.Copy(tempPath, taskPath, true);
            }
            catch
            {
                ErrorMessage($"Нет файла {taskPath}. ");
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }
        public void LoadUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserList));
            try
            {
                using (FileStream stream = new FileStream(userPath, FileMode.Open))
                {
                    usersList = ((UserList)serializer.Deserialize(stream)).Users;
                }
            }
            catch 
            {
                ErrorMessage($"Нет файла {userPath}. ");
            }
        }
        public void LoadTasks()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
            try
            {
                using (FileStream stream = new FileStream(taskPath, FileMode.Open))
                {
                    tasksList = ((TaskList)serializer.Deserialize(stream)).Tasks;
                }
            }
            catch
            {
                ErrorMessage($"Нет файла {taskPath}. ");
            }
        }
        public void AssingUser(int index, string uName)
        {
            if (!((from u in Users where u.Role == Role.Worker select u.UserName).Contains(uName))) throw new Exception("Сотрудника(с ролью Worker) с таким ID нет! ");
            Tasks[index].AssignedUsers.Add(uName);
            SaveTasks();
        }
        public void AddTask(Task t)
        {
            Tasks.Add(t);
            SaveTasks() ;
        }
        public void AddUser(User u)
        {
            Users.Add(u);
            SaveUsers() ;
        }
        public void ErrorMessage(string message)
        {
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message + "Для продолжения нажмите Enter.");
            Console.ForegroundColor = consoleColor;
            while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }

        public void Start()
        {
            if (usersList == null || tasksList == null)
            {
                ErrorMessage("Завершение работы");
                return;
            }
            IAppStep currentStep = new Register(this);
            while (currentStep != null)
            {
                currentStep = currentStep.Run();
            }
        }

        internal List<Task> CurrentWorkerTasks()
        {
            return (from t in Tasks where t.AssignedUsers.Contains(CurrentUser.UserName) select t).ToList();
        }

        internal bool IsUniqueName(string uName)
        {
            return !(from u in Users select u.UserName).Contains(uName);
        }

        internal bool IsUniqueTask(string id, string name)
        {
            return (from t in Tasks where t.Name == name && t.ProjectId == id select t).Any();
        }
    }
}
