using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace GotoHealth10.Controls
{
    public sealed partial class NumPickerControl : UserControl
    {
        public double EPSILON { get; private set; }

        public NumPickerControl()
        {
            InitializeComponent();
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumPickerControl), new PropertyMetadata(0));

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumPickerControl), new PropertyMetadata(0));


        public double Weight
        {
            get { return (double)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Weight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeightProperty =
            DependencyProperty.Register("Weight", typeof(double), typeof(NumPickerControl), new PropertyMetadata(0.0));

        //private static void WEIGHTcHANGED(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var instance = d as NumPickerControl;
            
        //}

        private void Reduce_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var currentValue = double.Parse(content.Text);

            if (Math.Abs(currentValue) < EPSILON)
            {
                return;
            }
            content.Text = (currentValue - 0.1).ToString();

            SliderNumPicker.Value = double.Parse(content.Text);
        }

        private void Increase_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var currentValue = double.Parse(content.Text);
            content.Text = (currentValue + 0.1).ToString();

            SliderNumPicker.Value = double.Parse(content.Text);
        }

        private void Reduce_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var currentValue = double.Parse(content.Text);

            if (Math.Abs(currentValue) < EPSILON)
            {
                return;
            }
            content.Text = (currentValue - 0.1).ToString();

            SliderNumPicker.Value = double.Parse(content.Text);
        }

        private void Increase_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var currentValue = double.Parse(content.Text);
            content.Text = (currentValue + 0.1).ToString();

            SliderNumPicker.Value = double.Parse(content.Text);
        }

        private void Value_Slider(object sender, RangeBaseValueChangedEventArgs e)
        {
            content.Text = SliderNumPicker.Value.ToString("N1");
        }
    }
}

