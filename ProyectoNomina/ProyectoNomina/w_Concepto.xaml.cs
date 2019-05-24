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
    /// Lógica de interacción para w_Concepto.xaml
    /// </summary>
    public partial class w_Concepto : Window
    {

        NominaEntities datos;
        public w_Concepto()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }
        private void CargarDatosGrilla()
        {
            try
            {

                dgConcepto.ItemsSource = datos.Concepto.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Concepto datoconcepto = new Concepto();

            datoconcepto.Descripcion = txtParame.Text;

            datoconcepto.Tipo = txtTipo.Text;


            datos.Concepto.Add(datoconcepto);
            datos.SaveChanges();
            MessageBox.Show("Tus datos se guardaron correctamente!");
            CargarDatosGrilla();

        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = string.Empty;
            txtParame.Text = string.Empty;

            txtTipo.Text = string.Empty;


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgConcepto.SelectedItem != null)
            {
                Concepto a = (Concepto)dgConcepto.SelectedItem;
                datos.Concepto.Remove(a);
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Concepto de la grilla para eliminar!");
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgConcepto.SelectedItem != null)
            {
                Concepto a = (Concepto)dgConcepto.SelectedItem;

                a.Descripcion = txtParame.Text;
                a.Tipo = txtTipo.Text;




                datos.Entry(a).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                MessageBox.Show("Tus datos se modificaron correctamente!");
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Concepto de la grilla para modificar!");
        }

        private void dgConcepto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgConcepto.SelectedItem != null)
            {
                Concepto a = (Concepto)dgConcepto.SelectedItem;

                txtId.Text = a.Id_Concepto.ToString();
                txtParame.Text = a.Descripcion;
                txtTipo.Text = a.Tipo;


            }
        }
    }
}
