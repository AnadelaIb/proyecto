using Microsoft.Win32;
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
    /// Lógica de interacción para w_Empleados.xaml
    /// </summary>
    public partial class w_Empleados : Window
    {
        NominaEntities datos;
        public w_Empleados()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }
        private void CargarDatosGrilla()
        {
            try
            {
                //Con una sola linea de código, cargamos la grilla con las Artesanias
                dgArtesanias.ItemsSource = datos.Empleado.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Elegir una imagen";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
           
    //        Empleado emple = new Empleado();
    //        //arte.Id_Artesania = ? Es autogenerado! ish!
    //        arte.Descripcion = txtDescripcion.Text;
    //        arte.Material = (Material)cboMaterialPrincipal.SelectedItem;

    //        if (rdbNacional.IsChecked == true)
    //            arte.Procedencia = rdbNacional.Content.ToString();
    //        else
    //            arte.Procedencia = rdbImportado.Content.ToString();

    //        arte.PathImagen = imgPhoto.Source.ToString();

    //        //Guardamos la artesania
    //        datos.Artesania.Add(arte);
    //        datos.SaveChanges();

    //        //Como Artesania/Categoria se guarda en una tabla aparte (correlación), entonces a continuación hacemos eso...            
    //        //Que categorias checkeó?
    //        foreach (var chk in chksCategorias.Children.OfType<CheckBox>())
    //        {
    //            //Obtenemos la categoria de acuerdo al id, guardado en el tag:
    //            if (chk.IsChecked == true)
    //            {
    //                int idCategoria = int.Parse(chk.Tag.ToString());
    //                Categoria c = datos.Categoria.Find(idCategoria);

    //                Artesania_Categoria ac = new Artesania_Categoria();
    //                ac.Artesania = arte;
    //                ac.Categoria = c;

    //                datos.Artesania_Categoria.Add(ac);
    //                datos.SaveChanges();
    //            }
    //        }
      }

        private void dgArtesanias_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
