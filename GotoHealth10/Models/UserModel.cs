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
        public short Age { get; set; }
        public string Gender { get; set; }
        public double InitialWeigth { get; set; }
        public double Height { get; set; }
        public double TargetWeight { get; set; }
        public DateTime TargetDate { get; set; }
    }
}