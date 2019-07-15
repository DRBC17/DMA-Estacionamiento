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
    /// Lógica de interacción para ListarSalEnt.xaml
    /// </summary>
    public partial class ListarSalEnt : UserControl
    {
        SqlConnection sqlconnection;
        public ListarSalEnt()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Software_Estacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);
           

        }

        private void ListarEntradas()
        {

            try
            {

                string query = "SELECT * FROM Est.Vehiculo WHERE estado = 1";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    DataTable tablaEntradas = new DataTable();
                    sqlDataAdapter.Fill(tablaEntradas);

                    lvListar.DisplayMemberPath = "placa";
                    
                    lvListar.SelectedValuePath = "id";
                    lvListar.ItemsSource = tablaEntradas.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void ListarSalida()
        {
            try
            {

                string query = "SELECT * FROM Est.Vehiculo WHERE estado = 0";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    DataTable tablaSalidas = new DataTable();
                    sqlDataAdapter.Fill(tablaSalidas);

                    cbListar.DisplayMemberPath = "placa";
                    cbListar.SelectedValuePath = "id";
                    cbListar.ItemsSource = tablaSalidas.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        private void CbListar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //String Dato = cbListar.SelectedItem.ToString();
            //MessageBox.Show(Dato);
            //if (Dato=="Entrada")
            //{
                ListarEntradas();
            //}
            //else if(Dato=="Salida")
            //{
            //    ListarSalida();
            //}
            //else
            //{
            //    MessageBox.Show("Holis");
            //}

        }
    }
}
