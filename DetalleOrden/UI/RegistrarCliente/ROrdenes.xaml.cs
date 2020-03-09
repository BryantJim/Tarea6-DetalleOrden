using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DetalleOrden.Entidades;
using DetalleOrden.BLL;

namespace DetalleOrden.UI.RegistrarCliente
{
    /// <summary>
    /// Interaction logic for ROrdenes.xaml
    /// </summary>
    public partial class ROrdenes : Window
    {
        Ordenes orden = new Ordenes();

        public ROrdenes()
        {
            InitializeComponent();
            this.DataContext = orden;
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = "0";
            ProductoIdTextBox.Text = "0";
            CantidadTextBox.Text = "0";
            PrecioTextBox.Text = "0";
            MontoTextBox.Text = "0";
            MontoTotalTextBox.Text = "0";
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = orden;
        }

        private void Limpiar()
        {
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = "0";
            NombreClienteTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            ProductoIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            CantidadTextBox.Text = "0";
            PrecioTextBox.Text = "0";
            MontoTextBox.Text = "0";
            MontoTotalTextBox.Text = "0";

            OrdenDetalleDataGrid.ItemsSource = new List<OrdenDetalle>();
            Actualizar();
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            orden.ordenDetalle.Add(new OrdenDetalle(Convert.ToInt32(OrdenIdTextBox.Text), Convert.ToInt32(ProductoIdTextBox.Text),
                DescripcionTextBox.Text,Convert.ToDecimal(CantidadTextBox.Text), Convert.ToDecimal(PrecioTextBox.Text),
                Convert.ToDecimal(MontoTextBox.Text)));

            orden.MontoTotal += Convert.ToDecimal(MontoTextBox.Text);
            MontoTotalTextBox.Text = Convert.ToString(orden.MontoTotal);

            Actualizar();
            ProductoIdTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            CantidadTextBox.Clear();
            MontoTextBox.Clear();
            ProductoIdTextBox.Focus();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Ordenes OrdenAnterior = OrdenesBLL.Buscar(orden.OrdenId);

            return OrdenAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosClientes()
        {
            Clientes ClienteAnterior = ClientesBLL.Buscar(orden.ClienteId);

            return ClienteAnterior != null;
        }

        private bool ExisteEnLaBaseDeDatosProductos()
        {
            Productos ProductoAnterior = ProductosBLL.Buscar(Convert.ToInt32(ClienteIdTextBox.Text));

            return ProductoAnterior != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (OrdenIdTextBox.Text == "0")
                paso = OrdenesBLL.Guardar(orden);
            else
            {
                if (ExisteEnLaBaseDeDatos() && ExisteEnLaBaseDeDatosClientes() && ExisteEnLaBaseDeDatosProductos())
                {
                    paso = OrdenesBLL.Modificar(orden);
                }
                else
                {
                    MessageBox.Show("No se Puede Modificar una Orden que no existe, no exista un producto o cliente");
                    return;
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("La Orden No se Pudo Guardar");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesBLL.Eliminar(orden.OrdenId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se pudo eliminar una orden que no existe");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes OrdenAnterior = OrdenesBLL.Buscar(Convert.ToInt32(OrdenIdTextBox.Text));

            if (OrdenAnterior != null)
            {
                orden = OrdenAnterior;
                Actualizar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Orden,cliente o producto no encontrado");
            }
        }

        private void LlenaCampoCliente(Clientes cliente)
        {
            NombreClienteTextBox.Text = cliente.Nombre;
            MontoTotalTextBox.Text = Convert.ToString(orden.MontoTotal);
        }

        private void ClienteIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ClienteIdTextBox.Text))
            {
                int id;
                Clientes cliente = new Clientes();
                int.TryParse(ClienteIdTextBox.Text, out id);

                cliente = ClientesBLL.Buscar(id);
                if (cliente != null)
                {
                    LlenaCampoCliente(cliente);
                }
                else
                {
                    NombreClienteTextBox.Text = "No existe el Cliente";
                }
            }
        }

        private void LlenaCampoProducto(Productos producto)
        {
            DescripcionTextBox.Text = producto.NombreProducto;
            PrecioTextBox.Text = Convert.ToString(producto.Precio);
        }

        private void ProductoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProductoIdTextBox.Text))
            {
                int id;
                Productos producto = new Productos();
                int.TryParse(ProductoIdTextBox.Text, out id);

                producto = ProductosBLL.Buscar(id);
                if (producto != null)
                {
                    LlenaCampoProducto(producto);
                }
                else
                {
                    DescripcionTextBox.Text = "0";
                    PrecioTextBox.Text = "0";
                }
            }
        }

        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                decimal Monto, Precio = Convert.ToDecimal(PrecioTextBox.Text);
                decimal Cantidad = Convert.ToDecimal(CantidadTextBox.Text);

                Monto = Precio * Cantidad;
                MontoTextBox.Text = Convert.ToString(Monto);

            }            
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
