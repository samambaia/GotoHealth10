using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GotoHealth10.Controls
{
    public sealed partial class AgePickControl : UserControl
    {
        public AgePickControl()
        {
            this.InitializeComponent();

            AgePick.Items.Clear();
            for (int i = 13; i < 111; i++)
            {
                AgePick.Items.Add(i.ToString());
            }
        }

        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Age.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register("Age", typeof(int), typeof(AgePickControl), new PropertyMetadata(0));
    }
}
