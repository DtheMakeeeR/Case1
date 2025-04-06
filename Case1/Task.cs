using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Case1
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
    [XmlRoot("TaskList")]
    public class TaskList
    {
        [XmlElement("Task")]
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
    public class Task
    {

        [XmlAttribute("ProjectId")]
        public string ProjectId { get; set; } = string.Empty;
        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;
        [XmlArray("AssignedUsers")]
        [XmlArrayItem("User")]
        public List<string> AssignedUsers { get; set; } = new List<string>();
        [XmlElement("Description")]

        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public override string ToString()
        {
            string res = "ProjectId: " + ProjectId +  " Name: " + Name + " AssignedUsers: ";
            foreach (string userId in AssignedUsers) res += $"{userId} ";
            res += "Description: " + Description + " Status: " + Status;
            return res;
        }
    }
}
