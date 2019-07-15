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
using System.Windows.Threading;

namespace Software_Estacionamiento
{
    /// <summary>
    /// Lógica de interacción para RealizarPago.xaml
    /// </summary>
    public partial class RealizarPago : UserControl
    {
        SqlConnection sqlconnection;
        public RealizarPago()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Software_Estacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);

            InitializeComponent();
            ListarEntradas();
            DispatcherTimer dispathcer = new DispatcherTimer();


            dispathcer.Interval = new TimeSpan(0, 0, 1);


            dispathcer.Tick += (s, a) =>
            {

                txtFecha.Text = DateTime.Now.ToString("hh:mm tt");
            };
            dispathcer.Start();
        }

        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LvPanelVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPanelVehiculos.SelectedItem != null)
            {


                String TipoVehiculo = lvPanelVehiculos.SelectedValue.ToString();
                String Dato = "";

                switch (TipoVehiculo)
                {
                    case "1":
                        Dato = "Turismo";
                        break;
                    case "2":
                        Dato = "Pick-Up";
                        break;
                    case "3":
                        Dato = "Camioneta";
                        break;
                    case "4":
                        Dato = "Camion";
                        break;
                    case "5":
                        Dato = "Autobus";
                        break;
                    case "6":
                        Dato = "Rastra";
                        break;
                    case "7":
                        Dato = "Motocicleta";
                        break;
                    default:
                        break;

                }
                txtTipoVehiculo.Text = Dato;
            }
        }

        public void ListarEntradas()
        {
            try
            {
                string query = "SELECT * FROM Est.Vehiculo WHERE estado=1";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {


                    DataTable tablaVehiculo = new DataTable();


                    sqlDataAdapter.Fill(tablaVehiculo);

                    // ¿Cuál información de la tabla en el DataTable debería se desplegada en nuestro ListBox?
                    lvPanelVehiculos.DisplayMemberPath = "placa";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    lvPanelVehiculos.SelectedValuePath = "tipo_Vehiculo";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    lvPanelVehiculos.ItemsSource = tablaVehiculo.DefaultView;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void BtnActualizarLista_Click(object sender, RoutedEventArgs e)
        {
            lvPanelVehiculos.SelectedItem = null;
            ListarEntradas();
        }
    }
}
