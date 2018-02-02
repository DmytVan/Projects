using AerPorts.ViewModel;
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

namespace AerPorts.View
{
    /// <summary>
    /// Логика взаимодействия для AddView.xaml
    /// </summary>
    public partial class AddView : Window
    {
        public AddView()
        {
            InitializeComponent();
            Loaded += AddView_Loaded;
        }

        private void AddView_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // this.DialogResult = true;
        }


        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
        }

    }
}
