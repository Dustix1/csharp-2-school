using System.Collections.ObjectModel;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Customer> Customers { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            //this.Content = new Button() { Content = "Ahoj! " }; // přes kód
            //btn.Content = "Pokus"; // přes xaml <Button Content="Ahoj" Name="btn" />
            // přes xaml to dělat je better ig

            Customers = new ObservableCollection<Customer>()
            {
                new Customer() { Id = 1, FirstName = "Jan", LastName = "ZIPBOMBAAA", Age = 20 }
            };
            
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            Customers.Add(new Customer()
            {
                Id = 2,
                FirstName = "Pepa",
                LastName = "Černý",
                Age = 67
            });
        }

        private void RemoveCustomer(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Customer customer = (Customer)btn.DataContext;

            this.Customers.Remove(customer);
        }

        private void AnonymizeCustomer(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Customer customer = (Customer)btn.DataContext;
            customer.FirstName = "***";
            customer.LastName = "***";
        }

        private void EditCustomer(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Customer customer = (Customer)btn.DataContext;

            SkibiditoiletDialog dialog = new SkibiditoiletDialog(customer);
            dialog.ShowDialog();
        }
    }
}