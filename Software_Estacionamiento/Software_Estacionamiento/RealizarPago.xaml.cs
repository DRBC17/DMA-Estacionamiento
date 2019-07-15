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
        int horasTT=0;

        public RealizarPago()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Software_Estacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);

            InitializeComponent();
            ListarEntradas();
            txtHorasT.Text = "0";
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
            try
            {
                string query = "INSERT INTO Est.Pago_Vehiculo(vehiculo,fechaHoraEntrada,fechaHoraEntrada) VALUES(@Vehiculo,@Entrada,@Salida)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                sqlCommand.Parameters.AddWithValue("@Vehiculo", Id_Vehiculo().ToString());
                sqlCommand.Parameters.AddWithValue("@Entrada", hora_entrada_C().ToString());
                sqlCommand.Parameters.AddWithValue("@Salida", DateTime.Now.ToString("hh:mm tt"));
                // Abrir la conexión
                sqlconnection.Open();

               
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
                Actualizar_Vehiculo();
                lvPanelVehiculos.SelectedItem = null;
                txtTipoVehiculo.Text = string.Empty;
                txtHorasT.Text = string.Format("0");
                txtTotal.Text = string.Format("00.00");
                ListarEntradas();

            }
        }
        private void Actualizar_Vehiculo()
        {
            try
            {
                string query = "UPDATE Est.Vehiculo SET estad = @estado WHERE id = @id";

                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                sqlCommand.Parameters.AddWithValue("@id", Id_Vehiculo());
                sqlCommand.Parameters.AddWithValue("@estado", 0);
                sqlconnection.Open();

             
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

        private void LvPanelVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvPanelVehiculos.SelectedItem != null)
            {


                String TipoVehiculo = lvPanelVehiculos.SelectedValue.ToString();
                String Dato = "";


                switch (IdentificarTipo())
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
                imprimirHora();
                txtTotal.Text = string.Format("LPS {0:0.0}", Cobro());


            }

        }
      

        private int Horas()
        {
            int calculo;
            int horaentrada = int.Parse(hora_entrada())*60+int.Parse(Minutos_entrada());
            int minutosSalida = int.Parse(DateTime.Now.ToString("mm"));
            int horasalida = int.Parse(DateTime.Now.ToString("HH"))*60+minutosSalida;
         
            calculo = horasalida - horaentrada;




            return calculo;
        }
        private void imprimirHora()
        {
            int minutos = Horas();
            
            string w, horastotal;

            decimal HorasC;


            HorasC = minutos / 60;
            if (HorasC > 240) {
            horastotal = HorasC.ToString();
            horastotal = horastotal.Substring(0, 2);
            w = HorasC.ToString();
            w = w.Substring(2, 2);
            txtHorasT.Text = (int.Parse(horastotal)).ToString();
            if (int.Parse(w) > 0)
            {
                txtHorasT.Text = txtHorasT.Text + 1;
                }
                else
                {
                    txtHorasT.Text = txtHorasT.Text;
                }
            }
            else
            {
                txtHorasT.Text = (HorasC.ToString());
            }

        }
        private double Cobro( )
        {
            Double x=0;
            int minutos = Horas();
            if (int.Parse(IdentificarTipo()) >=0 && int.Parse(IdentificarTipo()) <= 3)
            {
                if (minutos > 0 && minutos < 60)
                {
                    x = 20;

                }
                else if (minutos > 60 && minutos < 120)
                {
                    x = 30;
                }
                else if (minutos > 120 && minutos < 240)
                {
                    x = 70;
                }
                else if (minutos > 240)
                {
                    string w,horastotal;
                   
                    decimal Horas;
                   
                    Horas = minutos / 60;
                    horastotal = Horas.ToString();
                    horastotal = horastotal.Substring(0, 2);
                    w = Horas.ToString();
                    w = w.Substring(2, 2);

                    if (int.Parse(w) > 0)
                    {
                        x = (int.Parse(horastotal) + 1)*15;
                    }


                   
                        



                }
            }else if (int.Parse(IdentificarTipo()) >= 4 && int.Parse(IdentificarTipo()) <= 6)
            {
                if (minutos > 0 && minutos < 60)
                {
                    x = 20*2;

                }
                else if (minutos > 60 && minutos < 120)
                {
                    x = 30*2;
                }
                else if (minutos > 120 && minutos < 240)
                {
                    x = 70*2;
                }
                else if (minutos > 240)
                {
                    string w, horastotal;
                 
                    decimal Horas;

                    Horas = minutos / 60;
                    horastotal = Horas.ToString();
                    horastotal = horastotal.Substring(0, 2);
                    w = Horas.ToString();
                    w = w.Substring(2, 2);

                    if (int.Parse(w) > 0)
                    {
                        x = (int.Parse(horastotal) + 1) * 15;
                        x = x * 2;
                    }



                }
            }else if (int.Parse(IdentificarTipo()) ==6)
            {
                if (minutos > 0 && minutos < 60)
                {
                    x = 20*0.5;

                }
                else if (minutos > 60 && minutos < 120)
                {
                    x = 30*0.5;
                }
                else if (minutos > 120 && minutos < 240)
                {
                    x = 70*0.5;
                }
                else if (minutos > 240)
                {
                    string w, horastotal;
               
                    decimal Horas;

                    Horas = minutos / 60;
                    horastotal = Horas.ToString();
                    horastotal = horastotal.Substring(0, 2);
                    w = Horas.ToString();
                    w = w.Substring(2, 2);

                    if (int.Parse(w) > 0)
                    {
                        x = (int.Parse(horastotal) + 1) * 15;
                        x = x * 0.5;
                    }



                }
            }


            return x;
        }

        private String hora_entrada()
        {
            string hE = "";
            try
            {
                string query = "SELECT hora_Ingreso FROM Est.Vehiculo WHERE placa=@placa";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@placa", lvPanelVehiculos.SelectedValue);

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    hE = reader["hora_Ingreso"].ToString();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }


            hE = hE.Substring(0, 2);
            return hE;
        }
        private String hora_entrada_C()
        {
            string hE ="";
            try{
                string query = "SELECT hora_Ingreso FROM Est.Vehiculo WHERE placa=@placa";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@placa", lvPanelVehiculos.SelectedValue);

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    hE =  reader["hora_Ingreso"].ToString();

                }
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }

         
         
            return hE;
        }

        private int Id_Vehiculo()
        {
            int id = 0;

            try
            {
                string query = "SELECT id FROM Est.Vehiculo WHERE placa=@placa";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@placa", lvPanelVehiculos.SelectedValue);

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    string x;
                    x = reader["id"].ToString();
                    id = int.Parse(x);

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }


            
            return id;
        }

        private String Minutos_entrada()
        {
            string hE = "";
            try
            {
                string query = "SELECT hora_Ingreso FROM Est.Vehiculo WHERE placa=@placa";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@placa", lvPanelVehiculos.SelectedValue);

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    hE = reader["hora_Ingreso"].ToString();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }


            hE = hE.Substring(3, 2);
            return hE;
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
                    lvPanelVehiculos.SelectedValuePath = "placa";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    lvPanelVehiculos.ItemsSource = tablaVehiculo.DefaultView;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
        }

        private string IdentificarTipo()
        {
            string tipoV = "";
            try
            {
                string query = "SELECT tipo_Vehiculo FROM Est.Vehiculo WHERE placa=@placa";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@placa", lvPanelVehiculos.SelectedValue);

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    tipoV = reader["tipo_Vehiculo"].ToString();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }

            return tipoV;
        }

        private void BtnActualizarLista_Click(object sender, RoutedEventArgs e)
        {
            lvPanelVehiculos.SelectedItem = null;
            txtTipoVehiculo.Text = string.Empty;
            txtHorasT.Text = string.Format("0");
            txtTotal.Text = string.Format("00.00");
            ListarEntradas();
        }
    }
}
