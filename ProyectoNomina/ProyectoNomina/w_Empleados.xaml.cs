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
            cboTurno.ItemsSource = datos.Turno.ToList();
            cboTurno.DisplayMemberPath = "Hora_Entrada";
            cboTurno.SelectedValuePath = "Id_Turno";
        }
        private void CargarDatosGrilla()
        {
            try
            {
                //Con una sola linea de código, cargamos la grilla 
                dgEmpleados.ItemsSource = datos.Empleado.ToList();
                var turno = datos.Turno.ToList();
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
            try
            { 
                Empleado emple = new Empleado();
            
            emple.Nombres = txtNombre.Text;
            emple.Apellidos = txtApellido.Text;
            emple.Nro_Documento = txtDocumento.Text;
            emple.Direccion = txtDireccion.Text;
            emple.Nro_Telefono = txtNroTelefono.Text;
           // DateTime? selectedDate = dpFechaNac.SelectedDate;
            //if (selectedDate.HasValue)
            //{
            //    // String formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    emple.Fecha_Nacimiento = selectedDate.Value;
            //}
            //DateTime? selectedDate2 = dpFechIngreso.SelectedDate;
            //if (selectedDate.HasValue)
            //{
            //    // String formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //    emple.Fecha_Incorporacion = selectedDate2.Value;
            //}
            emple.Fecha_Nacimiento = Convert.ToDateTime(dpFechaNac.Text);
            emple.Fecha_Incorporacion = Convert.ToDateTime(dpFechIngreso.Text);


            emple.Imagen_Perfil = imgPhoto.Source.ToString();
                try
                {
                    emple.Salario_Basico = Int32.Parse(txtSalarioBasico.Text);
               
                emple.Turno = (Turno)cboTurno.SelectedItem;
                 

            datos.Empleado.Add(emple);
            datos.SaveChanges();
            MessageBox.Show("Tus datos se han guardado correctamente!");
            CargarDatosGrilla();
            Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Debe ingresar valores menores a 2 000 000 000!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe cargar todos los datos!");
            }
        }

      private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtNroTelefono.Text = string.Empty;
            txtSalarioBasico.Text = string.Empty;
            dpFechaNac.Text = string.Empty;
            dpFechIngreso.Text = string.Empty;
            imgPhoto.Source = null;
            cboTurno.SelectedValue = null;


        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmpleados.SelectedItem != null)
            {

                Empleado emple = (Empleado)dgEmpleados.SelectedItem;

                emple.Nombres = txtNombre.Text;
                emple.Apellidos = txtApellido.Text;
                emple.Nro_Documento = txtDocumento.Text;
                emple.Direccion = txtDireccion.Text;
                emple.Nro_Telefono = txtNroTelefono.Text;

                emple.Fecha_Nacimiento = Convert.ToDateTime(dpFechaNac.Text);
                emple.Fecha_Incorporacion = Convert.ToDateTime(dpFechIngreso.Text);


                emple.Imagen_Perfil = imgPhoto.Source.ToString();

                emple.Salario_Basico = Int32.Parse(txtSalarioBasico.Text);

                emple.Turno = (Turno)cboTurno.SelectedItem;
                datos.Entry(emple).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();

                MessageBox.Show("Tus datos se han modificado correctamente!");
                CargarDatosGrilla();
                Limpiar();
            }
            else
                MessageBox.Show("Debe seleccionar un Empleado de la grilla para modificar!");
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (dgEmpleados.SelectedItem != null)
            {
                Empleado emple = (Empleado)dgEmpleados.SelectedItem;

                

                datos.Empleado.Remove(emple);
                datos.SaveChanges();
                MessageBox.Show("Empleado eliminado correctamente!");
                CargarDatosGrilla();
                Limpiar();
            }
            else
                MessageBox.Show("Debe seleccionar un empleado de la grilla para eliminar!");

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
             Limpiar();

        }

        private void dgEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

                Empleado a = (Empleado)dgEmpleados.SelectedItem;

                
                txtNombre.Text = a.Nombres;
                txtApellido.Text = a.Apellidos;
                txtDireccion.Text = a.Direccion;
                txtDocumento.Text = a.Nro_Documento ;
                txtNroTelefono.Text = a.Nro_Telefono;
                txtSalarioBasico.Text = a.Salario_Basico.ToString();
                String stringPath = a.Imagen_Perfil;
                Uri imageUri = new Uri(stringPath);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                imgPhoto.Source = imageBitmap;

                dpFechaNac.Text = a.Fecha_Nacimiento.ToString();
                dpFechIngreso.Text = a.Fecha_Incorporacion.ToString();
            cboTurno.SelectedValue = a.Turno_Id.ToString();

          
        }
    }
}
