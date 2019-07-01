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
    /// Lógica de interacción para w_LiquidacionMensual.xaml
    /// </summary>
    public partial class w_LiquidacionMensual : Window
    {

        NominaEntities datos;
        int userID;
        public w_LiquidacionMensual(int userId)
        {
            InitializeComponent();
            datos = new NominaEntities();
            userID = userId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Text = userID.ToString();
            txtMes.Text = DateTime.Now.Month.ToString();
            txtAnho.Text = DateTime.Now.Year.ToString();
            txtUsuario.IsEnabled = false;
            CargarDatosGrilla();
        }
        private void CargarDatosGrilla()
        {
            try
            {

                dgLiquidaciones.ItemsSource = datos.Liquidacion_Mensual.ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Liquidacion_Mensual nuevo = new Liquidacion_Mensual();


                nuevo.Mes = short.Parse(txtMes.Text);
                nuevo.Anho = short.Parse(txtAnho.Text);

                nuevo.Fecha_Generacion = DateTime.Now;

                nuevo.Usuario_Id = Int32.Parse(txtUsuario.Text);
                if (rdbEstado.IsChecked == true)
                    nuevo.Estado = "A";


                datos.Liquidacion_Mensual.Add(nuevo);
                datos.SaveChanges();
                MessageBox.Show("Los datos se han guardado correctamente!");



                CargarDatosGrilla();




            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe cargar todos los datos!");
            }
        }
    }
}