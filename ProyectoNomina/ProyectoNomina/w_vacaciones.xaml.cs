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
    /// Lógica de interacción para w_vacaciones.xaml
    /// </summary>
    public partial class w_vacaciones : Window
    {
        NominaEntities datos;
        public w_vacaciones()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }
        private void CargarDatosGrilla()
        {
            try
            {
                
                dgVacaciones.ItemsSource = datos.Vacaciones.ToList();
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
            if (dgVacaciones.SelectedItem != null)
            {
                Vacaciones v = (Vacaciones)dgVacaciones.SelectedItem;
                if (v.Estado.Equals("Pendiente"))
                {
                    v.Estado = "Aprobado";

                    datos.Entry(v).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();
                    MessageBox.Show("Se ha aprobado la solicitud de vacaciones!");
                    CargarDatosGrilla();
                }
                else
                {
                    if (v.Estado.Equals("Rechazado"))
                    {
                        MessageBox.Show("No puede aprobar una solicitud previamente rechazada!");
                    }
                    else {
                        MessageBox.Show("La solicitud ya fue aprobada previamente!");
                    }

                }

            }
            else
                MessageBox.Show("Debe seleccionar un item!");
        }

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            if (dgVacaciones.SelectedItem != null)
            {
                Vacaciones a = (Vacaciones)dgVacaciones.SelectedItem;
                if (a.Estado.Equals("Pendiente"))
                {
                    a.Estado = "Rechazado";
                   
                    datos.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    datos.SaveChanges();
                    MessageBox.Show("Se ha rechazado la solicitud de vacaciones!");
                    CargarDatosGrilla();
                }
                else
                {
                    if (a.Estado.Equals("Aprobado"))
                    {
                        MessageBox.Show("No puede rechazar una solicitud previamente aprobada!");
                    }
                    else{
                        MessageBox.Show("La solicitud ya fue rechazada previamente!");
                    }

                }
    
            }
            else
                MessageBox.Show("Debe seleccionar un item!");
        }
    }
    }

