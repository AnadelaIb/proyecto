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
        int idLiquidacion;
        int mesLiquidacion;
        int anhoLiquidacion;
        public w_Generar()
        {
            InitializeComponent();
            datos = new NominaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dgLiquidaciones.ItemsSource = datos.Liquidacion_Mensual.ToList();
          

        }

        private bool ConsultaLiquidacion(int idLiquidacion)
        {
            var liquidacion = datos.SalarioBasico.ToList();
            foreach(var q in liquidacion)
            {
                if(q.IdLiquidacion == idLiquidacion)
                {
                    return false;
                }
               
            }
            return true;
        }
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (ConsultaLiquidacion(idLiquidacion))
            {
              
          
            var listaEmpleados = datos.Empleado.ToList();
            var concepto = datos.Concepto.ToList();
            var cantEmpleados = listaEmpleados.Count();
            int[] conceptosPositivos = new int[cantEmpleados];
            int[] conceptosNegativos = new int[cantEmpleados];
            int[] Anticipo = new int[cantEmpleados];
           double[] IPS = new double[cantEmpleados];
            int[] totalCobrar = new int[cantEmpleados];
            var DetalleLiquidacion = datos.Liquidacion_Mensual_Detalle.ToList();

            var anticipo = datos.Anticipo.ToList();

            for (var p = 0; p < cantEmpleados; p++)
            {
                conceptosPositivos[p] = 0;
                conceptosNegativos[p] = 0;
                IPS[p] = 0;
                totalCobrar[p] = 0;
                Anticipo[p] = 0;
            }

      List <Liquidacion_Mensual_Detalle> listaDetalles = new List<Liquidacion_Mensual_Detalle>();
            List<Anticipo> listaAnticipo = new List<Anticipo>();
            foreach (var i in DetalleLiquidacion)
            {
                if (i.Liquidacion_Id==idLiquidacion)
                {
                    listaDetalles.Add(i);
                }
            }
            foreach (var i in anticipo)
            {
                DateTime fecha = Convert.ToDateTime(i.Fecha_Definicion);
                var mes = fecha.Month;
                var anho = fecha.Year;
                if (mesLiquidacion== mes && anhoLiquidacion== anho )
                {
                    listaAnticipo.Add(i);
                }
            }

            foreach (var i in listaEmpleados)
                
            {
                int a=0;
                for(var j = 0; j < listaDetalles.Count(); j++)
                {
                    if (i.Id_Empleado == listaDetalles[j].Empleado_Id)
                    {
                        if (listaDetalles[j].Monto > 0)
                        {
                            conceptosPositivos[a] = conceptosPositivos[a] + listaDetalles[j].Monto;
                        }
                        else
                        {
                            conceptosNegativos[a] = conceptosNegativos[a] + listaDetalles[j].Monto;
                        }
                    
                    }
            }
                for (var j = 0; j < listaAnticipo.Count(); j++)
                {
                    if (i.Id_Empleado == listaAnticipo[j].Empleado_Id)
                    {
                        if (listaAnticipo[j].Estado.Equals("Aprobado"))
                        {
                            Anticipo[a] = Anticipo[a] + listaAnticipo[j].Monto_Aprobado;
                        }
                    }
                }
                    IPS[a] = Math.Round((i.Salario_Basico + conceptosPositivos[a]) * 0.09);
                  //  IPS[a] = (i.Salario_Basico + conceptosPositivos[a]) * 0.09;

                    Liquidacion_Mensual_Detalle nw = new Liquidacion_Mensual_Detalle();
                nw.Liquidacion_Id = idLiquidacion;
                nw.Empleado_Id = i.Id_Empleado;
                foreach(var k in concepto)
                {
                    if (k.Descripcion.Equals("IPS"))
                        nw.Concepto_Id = k.Id_Concepto;
                   
                }
                nw.Monto = (int)IPS[a];
                datos.Liquidacion_Mensual_Detalle.Add(nw);
                datos.SaveChanges();

                totalCobrar[a] = ((i.Salario_Basico + conceptosPositivos[a]) - ((int)IPS[a]) - Anticipo[a] + conceptosNegativos[a]);

                SalarioBasico salario = new SalarioBasico();
                salario.IdLiquidacion = idLiquidacion;
                salario.IdEmpleado = i.Id_Empleado;
                salario.Salario = i.Salario_Basico;
                salario.ConceptosPositivos = conceptosPositivos[a];
                salario.ConceptosNegativos = conceptosNegativos[a];
                salario.Anticipo = Anticipo[a];
                salario.IPS = (int)IPS[a];
                salario.TotalPercibir = totalCobrar[a];
                datos.SalarioBasico.Add(salario);
                datos.SaveChanges();

                a++;

            }

                MessageBox.Show("Liquidación generada con éxito!!!");
            }
            else
            {
                MessageBox.Show("ATENCION!! Ya existe Liquidación para este mes!!");
            }
        }



        private void dgLiquidaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgLiquidaciones.SelectedItem != null)
                {
                    Liquidacion_Mensual a = (Liquidacion_Mensual)dgLiquidaciones.SelectedItem;

                    idLiquidacion= a.Id_Liquidacion;
                    mesLiquidacion = a.Mes;
                    anhoLiquidacion = a.Anho;


                }
            }
            catch
            {
                MessageBox.Show("Debe seleccionar un registro válido");
            }
        }
    }
}
