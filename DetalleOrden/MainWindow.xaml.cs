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
using DetalleOrden.UI.RegistrarCliente;

namespace DetalleOrden
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

        private void RegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            RClientes registrarCliente = new RClientes();
            registrarCliente.Show();
        }

        private void RegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            RegistroProducto registrarProducto = new RegistroProducto();
            registrarProducto.Show();
        }

        private void RegistrarOrden_Click(object sender, RoutedEventArgs e)
        {
            ROrdenes registrarOrdenes = new ROrdenes();
            registrarOrdenes.Show();
        }
    }
}
