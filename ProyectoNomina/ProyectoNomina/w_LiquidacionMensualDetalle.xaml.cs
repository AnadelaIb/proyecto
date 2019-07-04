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
       string estado;
        public w_LiquidacionMensualDetalle()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void CargarDatosGrilla()
        {
            try
            {
               
                dgLiquidaciones.ItemsSource = datos.Liquidacion_Mensual.ToList();
                dgLiquidacionesDetalle.ItemsSource = datos.Liquidacion_Mensual_Detalle.ToList();
                cboConcepto.ItemsSource = datos.Concepto.ToList();
                cboConcepto.DisplayMemberPath = "Descripcion";
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
            //txtMsg.IsEnabled = false;
            txtId.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            char ch1 = 'A';
         

            if (estado.Equals(ch1.ToString()))
            {
                Liquidacion_Mensual_Detalle nuevo = new Liquidacion_Mensual_Detalle();

                if (Int32.Parse(txtMonto.Text) > 0)
                {
                    nuevo.Liquidacion_Id = (Int32.Parse(txtId.Text));
                    nuevo.Empleado = (Empleado)cboEmpleado.SelectedItem;
                    nuevo.Concepto = (Concepto)cboConcepto.SelectedItem;
                    if (nuevo.Concepto.Tipo.Equals('+'.ToString()))
                    {
                        nuevo.Monto = Int32.Parse(txtMonto.Text);
                        //txtMsg.Text = "El concepto es positivo";
                        lblMesg.Content = "El concepto es positivo";
                    }
                    else {
                        nuevo.Monto = (Int32.Parse(txtMonto.Text)) * -1;
                        //txtMsg.Text = "El concepto es negativo";
                        lblMesg.Content = "El concepto es negativo";
                    }
                    datos.Liquidacion_Mensual_Detalle.Add(nuevo);
                    datos.SaveChanges();
                    MessageBox.Show("Los datos se han guardado correctamente!");

                    CargarDatosGrilla();

                    Limpiar();
                }
                else {
                  
                    MessageBox.Show("El monto debe ser mayor a O!");
                    Limpiar();
                }
               
            }
            else
            {
                MessageBox.Show("El estado de la Liquidación no es Abierto!");
                Limpiar();
            }

        }


        private void Limpiar()
        {
            txtId.Text = string.Empty;
            txtMonto.Text = string.Empty;
            //txtMsg.Text = string.Empty;
            cboConcepto.SelectedValue = null;
            cboEmpleado.SelectedValue = null;
            estado = string.Empty;
            lblMesg.Content = string.Empty;


        }

        private void dgLiquidaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgLiquidaciones.SelectedItem != null)
                {
                    Liquidacion_Mensual a = (Liquidacion_Mensual)dgLiquidaciones.SelectedItem;

                    txtId.Text = a.Id_Liquidacion.ToString();
                    estado = a.Estado.ToString();
                 
                }
            }
            catch
            {
                MessageBox.Show("Debe seleccionar un registro válido");
            }
        }

        private void dgLiquidacionesDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgLiquidacionesDetalle.SelectedItem != null)
            {
               Liquidacion_Mensual_Detalle liquidacion = (Liquidacion_Mensual_Detalle)dgLiquidacionesDetalle.SelectedItem;
                datos.Liquidacion_Mensual_Detalle.Remove(liquidacion);
                datos.SaveChanges();
                MessageBox.Show("Liquidacion detalle eliminado correctamente!");
                CargarDatosGrilla();
              
            }
            else
                MessageBox.Show("Debe seleccionar un detalle de liquidación de la grilla para eliminar!");
        }
    }
}
