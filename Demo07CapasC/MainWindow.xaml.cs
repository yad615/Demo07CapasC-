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
using Business;
namespace Demo07CapasC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var business = new BProduct();
            dataGrid.ItemsSource = business.Read();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var business = new BProduct();
                business.Create(new Entity.Product
                {
                    Name = txtName.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Stock = Convert.ToInt32(txtStock.Text),
                });
                MessageBox.Show("Registro Exitoso");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
    }
}