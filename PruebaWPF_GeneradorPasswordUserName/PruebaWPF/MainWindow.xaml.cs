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

namespace PruebaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidoPaterno = txtApellidoPaterno.Text;
            string apellidoMaterno = txtapellidoMaterno.Text;
            string correo = txtCorreoElectronico.Text;
            GenerarPassword();
            GenerarUserName(nombre, apellidoPaterno, apellidoMaterno);
            MessageBox.Show(GenerarPassword() + " " + GenerarUserName(nombre, apellidoPaterno, apellidoMaterno));
        }

        public string GenerarPassword()
        {
            Random random = new Random();
            string caracter = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int cantidad = caracter.Length;
            char letra;
            int cantLetra = 8;
            string password = string.Empty;
            for (int i = 0; i < cantLetra; i++)
            {
                letra = caracter[random.Next(cantidad)];
                password += letra.ToString();
            }
            return password;
        }

        public string GenerarUserName(string nombre, string apellidoPaterno, string apellidoMaterno)
        {
            string userName = string.Empty;
            for (int i = 0; i < apellidoPaterno.Length; i++)
            {
                if (i == 0)
                {
                    userName += apellidoPaterno[0];
                }
            }

            for (int i = 0; i < apellidoMaterno.Length; i++)
            {
                if (i == 0)
                {
                    userName += apellidoMaterno[0];
                }
            }

            for (int i = 0; i < nombre.Length; i++)
            {
                if (i == 0)
                {
                    userName += nombre[0];
                }
            }
            Random num = new Random();
            for (int i = 0; i < 7; i++)
            {
                userName += num.Next(0, 9);
            }
            return userName;
        }
    }
}
