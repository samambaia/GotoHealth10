using System;
using System.Globalization;

namespace GotoHealth10.Models
{
    public class WeighingModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Weight { get; set; }
        public string UpDown { get; set; }
        public string Difference { get; set; }
        public string IMC { get; set; }
    }
}
