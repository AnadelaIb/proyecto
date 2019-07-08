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
    /// Lógica de interacción para w_MostrarLiquidacion.xaml
    /// </summary>
    public partial class w_MostrarLiquidacion : Window
    {
        NominaEntities datos;
        int idLiquidacion;
        public w_MostrarLiquidacion()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void CargarDatosGrilla()
        {
            try
            {

                dgLiquidacion.ItemsSource = datos.Liquidacion_Mensual.ToList();
          
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
           
            txtId.IsEnabled = false;
        }

        private void btnVisualizar_Click(object sender, RoutedEventArgs e)
        {
           
            SalarioBasico mostrar = new SalarioBasico();
            var salarios = datos.SalarioBasico.ToList();
            int empleado = (int)cboEmpleado.SelectedValue;
            var b = 0;
            foreach(var i in salarios)
            {
                if(i.IdLiquidacion==idLiquidacion && i.IdEmpleado == empleado)
                {
                    mostrar = i;
                    b = 1;

                }

            }
            if (b == 0)
            {
                MessageBox.Show("No existe liquidación para este empleado");
            }
            else
            {
                var emple = datos.Empleado.ToList();
                foreach(var p in emple)
                {
                    if (p.Id_Empleado == empleado)
                    {
                        lblNombre.Content = p.Nombres;
                        lblApellido.Content = p.Apellidos;
                    }
                }
                lblSalario.Content = mostrar.Salario;
                lblPositivo.Content = mostrar.ConceptosPositivos;
                lblNegativo.Content = mostrar.ConceptosNegativos;
                lblIPS.Content = mostrar.IPS;
                lblAnticipo.Content = mostrar.Anticipo;
                lblTotal.Content = mostrar.TotalPercibir;
               
            }
            Limpiar();

        }
        private void Limpiar()
        {
            txtId.Text = string.Empty;
            cboEmpleado.SelectedValue = null;
           
            //lbl.Content = string.Empty;


        }
      
        private void button_Click(object sender, RoutedEventArgs e)
        {
            lblTotal.Content = string.Empty;
            lblSalario.Content = string.Empty;
            lblPositivo.Content = string.Empty;
            lblNegativo.Content = string.Empty;
            lblNombre.Content = string.Empty;
            lblApellido.Content = string.Empty;
            lblAnticipo.Content = string.Empty;
            lblIPS.Content = string.Empty;

        }

        private void dgLiquidacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgLiquidacion.SelectedItem != null)
                {
                    Liquidacion_Mensual a = (Liquidacion_Mensual)dgLiquidacion.SelectedItem;

                    txtId.Text = a.Id_Liquidacion.ToString();
                    idLiquidacion = a.Id_Liquidacion;

                }
            }
            catch
            {
                MessageBox.Show("Debe seleccionar un registro válido");
            }
        }
    }
}
