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
using DetalleOrden.BLL;
using DetalleOrden.Entidades;

namespace DetalleOrden.UI.RegistrarCliente
{
    /// <summary>
    /// Interaction logic for RegistroProducto.xaml
    /// </summary>
    public partial class RegistroProducto : Window
    {
        Productos producto = new Productos();

        public RegistroProducto()
        {
            InitializeComponent();
            this.DataContext = producto;
            ProductoIdTextBox.Text = "0";
        }

        private void Limpiar()
        {
            ProductoIdTextBox.Text = "0";
            NombreProductoTextBox.Text = string.Empty;
            PrecioTextBox.Text = "0";
            Actualizar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBasedeDato()
        {
            Productos productos = ProductosBLL.Buscar(producto.ProductoId);
            return productos != null;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Convert.ToInt32(ProductoIdTextBox.Text) == 0)
            {
                paso = ProductosBLL.Guardar(producto);
                //producto.OrdenDetalle.Add(new Ordenes(Convert.ToInt32(ClienteIdTextBox.Text)));
            }
            else
            {
                if (!ExisteEnLaBasedeDato())
                {
                    MessageBox.Show("No se puede modificar un producto que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    paso = ProductosBLL.Modificar(producto);
                }
            }
            if (paso)
                Limpiar();

        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos productoAnterior = ProductosBLL.Buscar(Convert.ToInt32(ProductoIdTextBox.Text));

            if (productoAnterior != null)
            {
                producto = productoAnterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("producto no encontrado");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosBLL.Eliminar(Convert.ToInt32(ProductoIdTextBox.Text)))
            {
                MessageBox.Show("Producto eliminado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("no se puede eliminar un producto que no existe");
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = producto;
        }
    }
}
