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
using ExamenParcial1.Entidades;
using ExamenParcial1.BLL;

namespace ExamenParcial1.UI
{
    /// <summary>
    /// Interaction logic for RegistroProductos.xaml
    /// </summary>
    public partial class RegistroProductos : Window
    {
        public RegistroProductos()
        {
            InitializeComponent();
            ProductoIDTextBox.Text = "0";
            ValorInventarioTextBox.Text = "0";
        }

        private void Limpiar()
        {
            ProductoIDTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            ExistenciaTextBox.Text = string.Empty;
            CostoTextBox.Text = string.Empty;
            ValorInventarioTextBox.Text = string.Empty;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private Productos LlenarClase()
        {
            Productos producto = new Productos();
            producto.ProductoId = Convert.ToInt32(ProductoIDTextBox.Text);
            producto.Descripcion = DescripcionTextBox.Text;
            producto.Existencia = Convert.ToInt32(ExistenciaTextBox.Text);
            producto.Costo = Convert.ToDecimal(CostoTextBox.Text);
            producto.ValorInventario = (producto.Existencia*producto.Costo);

            return producto;
        }

        private void LlenarCampo(Productos producto)
        {
            ProductoIDTextBox.Text = Convert.ToString(producto.ProductoId);
            DescripcionTextBox.Text = producto.Descripcion;
            ExistenciaTextBox.Text= Convert.ToString(producto.Existencia);
            CostoTextBox.Text= Convert.ToString(producto.Costo);
            ValorInventarioTextBox.Text= Convert.ToString(producto.ValorInventario);
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(ProductoIDTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de producto ID vacio");
                ProductoIDTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Descripción Vacio");
                DescripcionTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(ExistenciaTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Existencia vacio");
                ExistenciaTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(CostoTextBox.Text))
            {
                MessageBox.Show("No puede dejar el campo de Costo vacio");
                CostoTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Productos producto = ProductosBLL.Buscar(Convert.ToInt32(ProductoIDTextBox.Text));

            return (producto != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos producto = new Productos();
            bool paso = false;

            if (!Validar())
                return;

            producto = LlenarClase();

            //determinar si es guardar o modificar
            if (ProductoIDTextBox.Text == "0")
                paso = ProductosBLL.Guardar(producto);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede Modificar una persona que no existe");
                    return;
                }
                paso = ProductosBLL.Modificar(producto);
            }

            //informar resurtado
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
                MessageBox.Show("No se pudo Guardar");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Productos producto = new Productos();
            int.TryParse(ProductoIDTextBox.Text, out id);

            Limpiar();

            producto = ProductosBLL.Buscar(id);

            if (producto != null)
            {
                MessageBox.Show("Producto Encontrado");
                LlenarCampo(producto);
            }
            else
                MessageBox.Show("Producto no Encontrado");
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(ProductoIDTextBox.Text, out id);

            Limpiar();

            if (ProductosBLL.Eliminar(id))
            {
                MessageBox.Show("Producto Eliminado");
            }
            else
                MessageBox.Show("No se puede Eliminar un producto que no existe");
        }

       
    }
}
