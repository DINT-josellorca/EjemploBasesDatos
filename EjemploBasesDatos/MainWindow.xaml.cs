using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace EjemploBasesDatos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CLIENTE cliente;
        private Informes contexto;
        public MainWindow()
        {
            InitializeComponent();
            cliente = new CLIENTE();
            InsertarStackPanel.DataContext = cliente;
            contexto = new Informes();
            contexto.CLIENTES.Load();
            //ListaListBox.DataContext = tema_6Entities.CLIENTES.Local;
            var consulta = from n in contexto.CLIENTES
                           where n.genero == "Male"
                           orderby n.nombre
                           select n;
            ListaListBox.DataContext = contexto.CLIENTES.Local;
            IdComboBox.DataContext = contexto.CLIENTES.Local;
            IdModificarComboBox.DataContext = contexto.CLIENTES.Local;

        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                contexto.CLIENTES.Add(cliente);
                contexto.SaveChanges();
            }catch(Exception)
            {
                MessageBox.Show("No se puede introducir un elemento con el mismo id", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                contexto.CLIENTES.Remove(cliente);
            }
            finally
            {
                cliente = new CLIENTE();
                InsertarStackPanel.DataContext = cliente;
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            CLIENTE cliente2 = (CLIENTE)IdComboBox.SelectedItem;
            contexto.CLIENTES.Remove(cliente2);
            contexto.SaveChanges();
        }

        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            CLIENTE cliente2 = (CLIENTE)IdModificarComboBox.SelectedItem;
            contexto.CLIENTES.Remove(cliente2);
            contexto.SaveChanges();
            cliente2.nombre = NombreTextBox.Text;
            cliente2.apellido = ApellidoTextBox.Text;
            contexto.CLIENTES.Add(cliente2);
            contexto.SaveChanges();
        }
    }
}
