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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Software_Estacionamiento
{
    /// <summary>
    /// Lógica de interacción para RealizarReporte.xaml
    /// </summary>
    public partial class RealizarReporte : UserControl
    {
        SqlConnection sqlconnection;
        
        public RealizarReporte()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Software_Estacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);
            MostrarReportes();

        }

        private void MostrarReportes()
        {

            try
            {
            


                String query = "SELECT v.tipo_Vehiculo AS Tipo,v.placa AS Placa,p.fechaHoraEntrada AS Entrada,p.fechaHoraSalida AS Salida,p.total AS TOTAL FROM Est.Vehiculo v INNER JOIN Est.Pago_Vehiculo p ON v.id = p.vehiculo WHERE v.estado =0";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query,sqlconnection);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DGRealizarReporte.ItemsSource = ds.Tables[0].DefaultView;

               


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }


        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            MostrarReportes();
        }
    }

   
}
