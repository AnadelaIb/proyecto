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
    /// Lógica de interacción para w_Generar.xaml
    /// </summary>
    public partial class w_Generar : Window
    {
        NominaEntities datos;
        public w_Generar()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboLiquidacion.ItemsSource = datos.Liquidacion_Mensual.ToList();
            cboLiquidacion.DisplayMemberPath = "Mes"+"Anho";
            cboLiquidacion.SelectedValuePath = "Id_Liquidacion";
            

            var Liquidacion = datos.Liquidacion_Mensual.ToList();
            
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
