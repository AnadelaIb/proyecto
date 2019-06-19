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
using System.Xml.Linq;

namespace ProyectoNomina
{
    /// <summary>
    /// Lógica de interacción para w_Turnos.xaml
    /// </summary>
    public partial class w_Turnos : Window
    {
        NominaEntities datos;
        public w_Turnos()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }
        private void CargarDatosGrilla()
        {
            try
            {
                //Con una sola linea de código, cargamos la grilla 
                dgTurnos.ItemsSource = datos.Turno.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dgTurnos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTurnos.SelectedItem != null)
            {
                Turno t = (Turno)dgTurnos.SelectedItem;

                txtId.Text = t.Id_Turno.ToString();
                txtHorarioEntrada.Text = t.Hora_Entrada;
                txtHorarioSalida.Text = t.Hora_Salida;
                txtObservacion.Text = t.Observaciones;


            }
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
           
                Turno turno = new Turno();

            turno.Hora_Entrada = txtHorarioEntrada.Text;
            turno.Hora_Salida = txtHorarioSalida.Text;
            turno.Observaciones = txtObservacion.Text;
            

            datos.Turno.Add(turno);
           

            datos.SaveChanges();
            MessageBox.Show("Tus datos se han guardado correctamente!");
            CargarDatosGrilla();
                Limpiar();
           

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgTurnos.SelectedItem != null)
            {
                Turno a = (Turno)dgTurnos.SelectedItem;

                a.Hora_Entrada = txtHorarioEntrada.Text;
                a.Hora_Salida = txtHorarioSalida.Text;
                a.Observaciones = txtObservacion.Text;



                //Le ponemos una banderita de que se modicaron datos en la entidad..
                datos.Entry(a).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
                Limpiar();

                MessageBox.Show("Tus datos fueron modificados correctamente!");

            }
            else
                MessageBox.Show("Debes seleccionar un Turno de la grilla para modificar!");
        }
        private void Limpiar()
        {

            txtId.Text = string.Empty;
            txtHorarioEntrada.Text = string.Empty;
            txtHorarioSalida.Text = string.Empty;
            txtObservacion.Text = string.Empty;
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgTurnos.SelectedItem != null)
            {
                Turno a = (Turno)dgTurnos.SelectedItem;
                datos.Turno.Remove(a);
                datos.SaveChanges();
                CargarDatosGrilla();
                MessageBox.Show("Los datos seleccionados fueron eliminados!");
            }
            else
                MessageBox.Show("Debes seleccionar un turno de la grilla para eliminar!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }
    }
}
