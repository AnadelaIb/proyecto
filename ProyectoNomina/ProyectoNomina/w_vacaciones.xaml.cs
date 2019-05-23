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
    }
}
