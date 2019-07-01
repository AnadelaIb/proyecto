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
    /// Lógica de interacción para w_LiquidacionMensualDetalle.xaml
    /// </summary>
    public partial class w_LiquidacionMensualDetalle : Window
    {
        NominaEntities datos;
        public w_LiquidacionMensualDetalle()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void CargarDatosGrilla()
        {
            try
            {
                //Con una sola linea de código, cargamos la grilla 
                dgLiquidaciones.ItemsSource = datos.Liquidacion_Mensual.ToList();
                dgLiquidacionesDetalle.ItemsSource = datos.Liquidacion_Mensual_Detalle.ToList();
                cboConcepto.ItemsSource = datos.Concepto.ToList();
                cboConcepto.DisplayMemberPath = "Tipo";
                cboConcepto.SelectedValuePath = "Id_Concepto";
                cboEmpleado.ItemsSource = datos.Empleado.ToList();
                cboEmpleado.DisplayMemberPath = "Nombres";
                cboEmpleado.SelectedValuePath = "Id_Empleado";

                var Concepto = datos.Concepto.ToList();
                var Empleado = datos.Empleado.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
            txtMsg.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (dgLiquidaciones.SelectedItem != null)
            {
             
                Liquidacion_Mensual_Detalle nuevo = new Liquidacion_Mensual_Detalle();
                Liquidacion_Mensual liqui = (Liquidacion_Mensual)dgLiquidaciones.SelectedItem;
                nuevo.Liquidacion_Id = liqui.Id_Liquidacion;

                nuevo.Empleado = (Empleado)cboEmpleado.SelectedItem;
                nuevo.Concepto = (Concepto)cboConcepto.SelectedItem;

                if (txtMonto.Text.Equals(0))
                {
                    MessageBox.Show("Debe ser mayor a O!");
                }
                else {
                   
                    //FALTA HACER ESTO

                if (cboConcepto.SelectedItem.Equals('+'))
                {
                    txtMsg.Text = "es positivo";
                    nuevo.Monto = Int32.Parse(txtMonto.Text);
                }
                    else { 
                    txtMsg.Text = "es negativo";
                    nuevo.Monto = (Int32.Parse(txtMonto.Text))*-1;
                    }
              
              

                datos.Liquidacion_Mensual_Detalle.Add(nuevo);
                datos.SaveChanges();
                MessageBox.Show("Los datos se han guardado correctamente!");

                CargarDatosGrilla();
                }
            }
            else
                MessageBox.Show("Debe seleccionar una Liquidacion de la grilla para Agregar");


        }
    }
}
