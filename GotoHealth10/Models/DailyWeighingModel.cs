using System;
using System.Globalization;

namespace GotoHealth10.Models
{
    public class DailyWeighingModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Weight { get; set; }
        public string UpDown { get; set; }
        public string Difference { get; set; }
    }
}
