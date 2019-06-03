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
    /// Lógica de interacción para Permiso.xaml
    /// </summary>
    public partial class w_Permiso : Window
    {
        NominaEntities datos;
        public w_Permiso()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void CargarDatosGrilla()
        {
            try
            {
                //Con una sola linea de código, cargamos la grilla 
                dgPermisos.ItemsSource = datos.Permisos.ToList();
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

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            if (dgPermisos.SelectedItem != null)
            {
                Permisos p = (Permisos)dgPermisos.SelectedItem;
                if (p.Estado.Equals("Pendiente"))
                {
                    p.Estado = "Rechazado";
                    datos.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();

                    CargarDatosGrilla();
                    MessageBox.Show("Se rechazó el permiso correctamente!");
                }
               
                else if (p.Estado.Equals("Rechazado"))
                {
                    MessageBox.Show("El permiso ya está rechazado!");
                }
                else if (p.Estado.Equals("Aprobado"))
                {
                    MessageBox.Show("No se puede rechazar un permiso aprobado!");
                }


            }
            else
                MessageBox.Show("Debe elegir una solicitud de permiso de la grilla!");
        }

        private void btnAprobar_Click(object sender, RoutedEventArgs e)
        {
            if (dgPermisos.SelectedItem != null)
            {
                Permisos p = (Permisos)dgPermisos.SelectedItem;
                if (p.Estado.Equals("Pendiente"))
                {
                    p.Estado = "Aprobado";
                    datos.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();

                    CargarDatosGrilla();
                    MessageBox.Show("Se aprobó el permiso correctamente!");
                }
                else if (p.Estado.Equals("Rechazado"))
                {
                    MessageBox.Show("No se puede aprobar un permiso que ya está rechazado!");
                }
                else if (p.Estado.Equals("Aprobado"))
                {
                    MessageBox.Show("El permiso ya está aprobado!");
                }


            }
            else
                MessageBox.Show("Debe elegir una solicitud de permiso de la grilla!");
        }
    }
}
