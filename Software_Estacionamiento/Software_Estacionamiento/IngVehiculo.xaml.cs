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
    /// Lógica de interacción para IngVehiculo.xaml
    /// </summary>
    public partial class IngVehiculo : UserControl
    {
        SqlConnection sqlconnection;
        public IngVehiculo()
        {
            InitializeComponent();
            // Estacionamiento ConnectionString
            string connectionString = ConfigurationManager.ConnectionStrings["Software_Estacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);

            txtFecha.Text = DateTime.Now.ToString("hh:mm tt");
            //FechaSys.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        public UserControl ParentControl { get; set; }

        
        private void BtnCerrarContol_Click(object sender, RoutedEventArgs e)
        {
            //PanelPrincipal.Children.Remove(this);
            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO Est.Vehiculo(tipo_Vehiculo,placa,estado,hora_Ingreso) VALUES(@tipo_Vehiculo,@placa,@estado,@hora_Ingreso)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                // Abrir la conexión
                sqlconnection.Open();

                sqlCommand.Parameters.AddWithValue("@tipo_Vehiculo",cbTipoVehiculo.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@placa", cbTipoVehiculo.Text);
                sqlCommand.Parameters.AddWithValue("@estado", 1);
                sqlCommand.Parameters.AddWithValue("@placa", cbTipoVehiculo.Text);
                sqlCommand.Parameters.AddWithValue("@hora_Ingreso", DateTime.Now.ToString("hh:mm tt"));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void TxtTipoVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
