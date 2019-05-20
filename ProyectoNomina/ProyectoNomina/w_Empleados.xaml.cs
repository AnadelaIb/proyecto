using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ProyectoNomina
{
    /// <summary>
    /// Lógica de interacción para w_Empleados.xaml
    /// </summary>
    public partial class w_Empleados : Window
    {
        NominaEntities datos;
        public w_Empleados()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }
        private void CargarDatosGrilla()
        {
            try
            {
                //Con una sola linea de código, cargamos la grilla con las Artesanias
                dgArtesanias.ItemsSource = datos.Empleado.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Elegir una imagen";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            Empleado emple = new Empleado();
            
            emple.Nombres = txtNombre.Text;
            emple.Apellidos = txtApellido.Text;
            emple.Nro_Documento = txtDocumento.Text;
            emple.Direccion = txtDireccion.Text;
            emple.Nro_Telefono = txtNroTelefono.Text;
            //emple.Fecha_Nacimiento = txtFechNacimiento.Text;
            //emple.Fecha_Incorporacion = txtFechIngreso.Text;
            emple.Imagen_Perfil = imgPhoto.Source.ToString();
            //emple.Salario_Basico = txtSalarioBasico.Text;
            //Guardamos la artesania
            datos.Empleado.Add(emple);
            datos.SaveChanges();

            
            
        }

        private void dgArtesanias_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
