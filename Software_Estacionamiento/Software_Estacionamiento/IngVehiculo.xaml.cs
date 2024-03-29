﻿using System;
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
            listarTipoVehiculo();

            DispatcherTimer dispathcer = new DispatcherTimer();

            
            dispathcer.Interval = new TimeSpan(0, 0, 1);

            
            dispathcer.Tick += (s, a) =>
            {
               
                txtFecha.Text = DateTime.Now.ToString("hh:mm tt");
            };
            dispathcer.Start();
            
            //FechaSys.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        public UserControl ParentControl { get; set; }

        
        private void BtnCerrarContol_Click(object sender, RoutedEventArgs e)
        {
            //PanelPrincipal.Children.Remove(this);
            
        }

        private void listarTipoVehiculo()
        {
            try
            {

                string query = "SELECT * FROM Est.Tipo_Vehiculo";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    DataTable tablaTipoVehiculo = new DataTable();
                    sqlDataAdapter.Fill(tablaTipoVehiculo);

                    cbTipoVehiculo.DisplayMemberPath = "tipo";
                    cbTipoVehiculo.SelectedValuePath = "id";
                    cbTipoVehiculo.ItemsSource = tablaTipoVehiculo.DefaultView;
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
         if(txtNumPlaca.Text != "")
            {

                if (cbTipoVehiculo.SelectedItem != null)
                {


                    if (txtNumPlaca.Text != IdentificarPlaca().ToString())
                    {
                        try
                        {
                            string query = "INSERT INTO Est.Vehiculo(tipo_Vehiculo,placa,estado,hora_Ingreso) VALUES(@tipo_Vehiculo,@placa,@estado,@hora_Ingreso)";
                            SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                            // Abrir la conexión
                            sqlconnection.Open();

                            sqlCommand.Parameters.AddWithValue("@tipo_Vehiculo", cbTipoVehiculo.SelectedValue);
                            sqlCommand.Parameters.AddWithValue("@placa", txtNumPlaca.Text);
                            sqlCommand.Parameters.AddWithValue("@estado", 1);
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
                            MessageBox.Show("Se Guardo con exito");
                        }
                    }


                    else
                    {
                        MessageBox.Show("Un Vehiculo con esa placa ya Ingreso");
                    }
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar un Tipo de Vehiculo");
                }
            }
            else
            {
                MessageBox.Show("EL Numero De Placa no puede estar vacio");
            }
        }


        private string IdentificarPlaca()
        {
            string tipoV = "";
            try
            {
                string query = "SELECT placa FROM Est.Vehiculo WHERE placa=@placa";

                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Reemplazar el parámetro con su valor respectivo
                sqlCommand.Parameters.AddWithValue("@placa", txtNumPlaca.Text);

                sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    tipoV = reader["placa"].ToString();

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

        private void TxtTipoVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
