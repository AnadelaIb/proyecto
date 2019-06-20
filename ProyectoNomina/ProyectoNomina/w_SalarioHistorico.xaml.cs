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
    /// Lógica de interacción para w_SalarioHistorico.xaml
    /// </summary>
    public partial class w_SalarioHistorico : Window
    {
        NominaEntities datos;
        public w_SalarioHistorico()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboUsuario.ItemsSource = datos.Usuario.ToList();
            cboUsuario.DisplayMemberPath = "Usuario";
            cboUsuario.SelectedValuePath = "Id_Usuario";
          
        }

        private void cboEmpleado_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
