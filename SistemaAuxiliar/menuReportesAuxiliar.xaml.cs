using GestorInventario.ReporteVista;
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

namespace GestorInventario.SistemaAuxiliar
{
    /// <summary>
    /// Lógica de interacción para menuReportesAuxiliar.xaml
    /// </summary>
    public partial class menuReportesAuxiliar : Window
    {
        public menuReportesAuxiliar()
        {
            InitializeComponent();
        }


        #region Colores Botones
        private void btnRegresar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegresar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnRegresar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegresar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnReporteProductosAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnReporteProductosAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnReporteProductosAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnReporteProductosAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion


        #region Boton Regresar
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar al menú del Auxiliar?", "ATLAS CORP | REGRESAR AL MENÚ PRINCIPAL", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAuxiliar frmInicioAuxiliar = new frmInicioAuxiliar();
                this.Hide();
                frmInicioAuxiliar.Show();
            }
        }
        #endregion


        #region Boton para Reporte
        private void btnReporteProductosAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            ProductosReporteAdmin formProductosRpt = new ProductosReporteAdmin();
            formProductosRpt.Show();
        }
        #endregion


    }
}