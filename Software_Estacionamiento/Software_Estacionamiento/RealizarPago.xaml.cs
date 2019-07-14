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
    /// Lógica de interacción para RealizarPago.xaml
    /// </summary>
    public partial class RealizarPago : UserControl
    {
        public RealizarPago()
        {
            InitializeComponent();
            txtFecha.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LvPanelVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
