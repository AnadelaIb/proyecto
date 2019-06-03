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
    /// Lógica de interacción para w_AnticipoSalarial.xaml
    /// </summary>
    public partial class w_AnticipoSalarial : Window
    {

        NominaEntities datos;
        public w_AnticipoSalarial()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void CargarDatosGrilla()
        {
            try
            {

                dgAntSalario.ItemsSource = datos.Anticipo.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }

        private void btnAprobar_Click(object sender, RoutedEventArgs e)
        {
            if (dgAntSalario.SelectedItem != null)
            {
                Anticipo a = (Anticipo)dgAntSalario.SelectedItem;
                if (a.Estado.Equals("Pendiente"))
                {
                    a.Estado = "Aprobado";
                    datos.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();

                    CargarDatosGrilla();
                    MessageBox.Show("Se aprobó el anticipo correctamente!");
                }
                else if (a.Estado.Equals("Rechazado"))
                {
                    MessageBox.Show("No se puede aprobar un anticipo que ya está rechazado!");
                }
                else if (a.Estado.Equals("Aprobado"))
                {
                    MessageBox.Show("El anticipo ya está aprobado!");
                }


            }
            else
                MessageBox.Show("Debe elegir una solicitud de anticipo de la grilla!");


            
        }

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            if (dgAntSalario.SelectedItem != null)
            {
                Anticipo a = (Anticipo)dgAntSalario.SelectedItem;
                if (a.Estado.Equals("Pendiente"))
                {
                    a.Estado = "Rechazado";
                    datos.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();

                    CargarDatosGrilla();
                    MessageBox.Show("Se rechazó el anticipo correctamente!");
                }

                else if (a.Estado.Equals("Rechazado"))
                {
                    MessageBox.Show("El anticipo ya está rechazado!");
                }
                else if (a.Estado.Equals("Aprobado"))
                {
                    MessageBox.Show("No se puede rechazar un anticipo aprobado!");
                }


            }
            else
                MessageBox.Show("Debe elegir una solicitud de anticipo de la grilla!");
        }
    }
}
