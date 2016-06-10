using System;
using System.Globalization;

namespace GotoHealth10.Models
{
    public class WeighingModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public string UpDown { get; set; }
        public string Difference { get; set; }
        public double IMC { get; set; }
    }
}
