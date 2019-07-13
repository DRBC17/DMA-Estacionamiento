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
    /// Lógica de interacción para IngVehiculo.xaml
    /// </summary>
    public partial class IngVehiculo : UserControl
    {

        public IngVehiculo()
        {
            InitializeComponent();

            txtFecha.Text = DateTime.Now.ToString("hh:mm tt");
            //FechaSys.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        public UserControl ParentControl { get; set; }

        
        private void BtnCerrarContol_Click(object sender, RoutedEventArgs e)
        {
            //PanelPrincipal.Children.Remove(this);
            
        }
    }
}
