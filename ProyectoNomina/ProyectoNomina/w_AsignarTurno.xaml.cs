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
    /// Lógica de interacción para w_AsignarTurno.xaml
    /// </summary>
    /// 

    public partial class w_AsignarTurno : Window
    {

        NominaEntities datos;
        public w_AsignarTurno()
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
                //Con una sola linea de código, cargamos la grilla 
                dgEmpleadoT.ItemsSource = datos.Empleado.ToList();
                dgTurno.ItemsSource = datos.Turno.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void dgTurno_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            try
            {
                if (dgTurno.SelectedItem != null)
                {
                    Turno a = (Turno)dgTurno.SelectedItem;
                    txtTurnoInicio.Text = a.Hora_Entrada;
                    txtTurnoFin.Text = a.Hora_Salida;
                    txtObservaciones.Text = a.Observaciones;

                }
            }
            catch
            {
                MessageBox.Show("Debe seleccionar un registro válido");
            }

        }

        private void dgEmpleadoT_SelectedCellsChanged_1(object sender, SelectedCellsChangedEventArgs e)
        {
            try { 
                if(dgEmpleadoT.SelectedItem != null) { 

                    Empleado a = (Empleado)dgEmpleadoT.SelectedItem;
                    txtEmpleadoNombre.Text = a.Nombres+" "+a.Apellidos;
                    txtNroDocumento.Text = a.Nro_Documento;
                    String stringPath = a.Imagen_Perfil;
                    Uri imageUri = new Uri(stringPath);
                    BitmapImage imageBitmap = new BitmapImage(imageUri);
                    ImgEmpleado.Source = imageBitmap;
                }
            }catch
            {
                MessageBox.Show("Debe seleccionar un registro válido");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtEmpleadoNombre.Text = string.Empty;
            txtNroDocumento.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtTurnoInicio.Text = string.Empty;
            txtTurnoFin.Text = string.Empty;
            ImgEmpleado.Source = null;

        }

        private void btnLlamaTurno_Click(object sender, RoutedEventArgs e)
        {
            w_Turnos w_Turnos = new w_Turnos();
            w_Turnos.ShowDialog();
        }

        private void btnRecargar_Click(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }
    }
}
