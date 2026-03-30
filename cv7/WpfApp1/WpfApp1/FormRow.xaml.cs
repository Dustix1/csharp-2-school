using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for FormRow.xaml
    /// </summary>
    public partial class FormRow : UserControl
    {
        public static DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(string), typeof(FormRow), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });
        public string Label { get; set; }
        public string Value {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public FormRow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
