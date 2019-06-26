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
    /// 
    
    public partial class w_SalarioHistorico : Window
    {
        NominaEntities datos;
        int userID;
        public w_SalarioHistorico(int userId)
        {
            InitializeComponent();
            datos = new NominaEntities();
            userID = userId;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuId.Text = userID.ToString();

            CargarDatosGrilla();

        }

        private void CargarDatosGrilla()
        {
            try
            {
                 
                dgEmpleados.ItemsSource = datos.Empleado.ToList();
                var turno = datos.Turno.ToList();
                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgEmpleados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Empleado a = (Empleado)dgEmpleados.SelectedItem;


            txtEmpleado.Text = a.Id_Empleado.ToString();
            txtSalarioAnterior.Text = a.Salario_Basico.ToString();
            
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Empleado_Salario_Historico  salario = new Empleado_Salario_Historico();
               

                salario.Empleado_Id = Int32.Parse(txtEmpleado.Text);
                salario.Salario_Basico_Anterior = Int32.Parse(txtSalarioAnterior .Text);
                salario.Salario_Basico_Nuevo = Int32.Parse(txtSalarioNuevo .Text);
                salario.Fecha_Hora = DateTime.Now;

                salario.Usuario_Id = Int32.Parse(txtUsuId.Text);


                datos.Empleado_Salario_Historico.Add(salario);
                    datos.SaveChanges();
                    MessageBox.Show("Tus datos se han guardado correctamente!");


                Empleado emple = (Empleado)dgEmpleados.SelectedItem;
                emple.Salario_Basico = Int32.Parse(txtSalarioNuevo.Text);
                datos.Entry(emple).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();




            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe cargar todos los datos!");
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtEmpleado.Text = " ";
            txtSalarioAnterior.Text  = " ";
            txtSalarioNuevo.Text  = " ";
        }
    }
}
