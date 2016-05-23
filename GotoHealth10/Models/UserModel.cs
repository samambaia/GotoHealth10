using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotoHealth10.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Logged { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string InitialWeigth { get; set; }
        public string Height { get; set; }
        public string TargetWeight { get; set; }
    }
}