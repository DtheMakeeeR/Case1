using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Case1
{
    public enum Role
    {
        Worker,
        Administrator
    }
    [XmlRoot("UserList")]
    public class UserList
    {
        [XmlElement("User")]
        public List<User> Users { get; set; } = new List<User>();
    }
    public class User
    {
        //[XmlAttribute("Id")]
        //public string Id { get; set; }
        [XmlElement("Role")]
        public Role Role { get; set; }
        [XmlElement("UserName")]
        public string UserName { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }
        public override string ToString()
        {
            string res = $"UserName: {UserName}, Role: {Role}";
            return res;
        }
    }
}
