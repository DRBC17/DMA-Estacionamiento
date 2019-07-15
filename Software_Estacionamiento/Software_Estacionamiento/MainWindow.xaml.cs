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

namespace Software_Estacionamiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IngVehiculo _IngVehiculo;
        private ListarSalEnt _ListarSalEnt;
        private RealizarPago _RealizarPago;
        private RealizarReporte _RealizarReporte;
        public MainWindow()
        {
            InitializeComponent();
            _IngVehiculo = new IngVehiculo();
            _RealizarPago = new RealizarPago();
            _ListarSalEnt = new ListarSalEnt();
            _RealizarReporte = new RealizarReporte();
        }

        private void BarraSuperior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BarraSuperior2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnIngVehiculo_Click(object sender, RoutedEventArgs e)
        {
            IngVehiculo ventana = new IngVehiculo();

            ventana.txtFecha.Text = DateTime.Now.ToString("hh:mm tt");
            try
            {
                PanelPrincipal.Children.Add(_IngVehiculo);
                PanelPrincipal.Children.Remove(_RealizarPago);
                PanelPrincipal.Children.Remove(_RealizarReporte);
                PanelPrincipal.Children.Remove(_ListarSalEnt);

               
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
        }

        private void BtnRealizarPago_Click(object sender, RoutedEventArgs e)
        {
            try { 
            PanelPrincipal.Children.Remove(_IngVehiculo);
            PanelPrincipal.Children.Add(_RealizarPago);
            PanelPrincipal.Children.Remove(_RealizarReporte);
            PanelPrincipal.Children.Remove(_ListarSalEnt);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        private void BtnListarSalEnt_Click(object sender, RoutedEventArgs e)
        {
            try { 
            PanelPrincipal.Children.Remove(_IngVehiculo);
            PanelPrincipal.Children.Remove(_RealizarPago);
            PanelPrincipal.Children.Remove(_RealizarReporte);
            PanelPrincipal.Children.Add(_ListarSalEnt);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        private void BtnRealizarReporte_Click(object sender, RoutedEventArgs e)
        {
            try { 
            PanelPrincipal.Children.Remove(_IngVehiculo);
            PanelPrincipal.Children.Remove(_RealizarPago);
            PanelPrincipal.Children.Add(_RealizarReporte);
            PanelPrincipal.Children.Remove(_ListarSalEnt);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnRest_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRest.Visibility = Visibility.Collapsed;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRest.Visibility = Visibility.Visible;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
