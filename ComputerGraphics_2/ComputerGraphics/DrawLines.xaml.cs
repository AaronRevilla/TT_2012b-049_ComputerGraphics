using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerGraphics
{
    /// <summary>
    /// Lógica de interacción para DrawLines.xaml
    /// </summary>
    public partial class DrawLines : Window
    {

        LeerArchivo archivo = null;
        public DrawLines()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == false) return;
            nombreArchivo.Text = ofd.FileName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            archivo = new LeerArchivo(nombreArchivo.Text);
            cajaTexto.Text = archivo.getAllDocument();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            cajaTexto.Text = "";
            nombreArchivo.Text = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string textoLinea = archivo.getLine(Convert.ToInt32(numLinea.Text));
            if (textoLinea != null)
            {
                cajaTexto.Text = textoLinea;
            }
            else 
            {
                cajaTexto.Text = "Linea no encontrada";
            }
            
        }

    }
}
