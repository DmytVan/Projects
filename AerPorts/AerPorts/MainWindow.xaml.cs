using AerPorts.Date;
using AerPorts.VievModel;
using AerPorts.View;
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

namespace AerPorts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddView addView;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;        

            //sqlCommand = new SqlCommand("UPDATE [Table] SET [Name] = @Name WHERE [id] = 1", sqlCon);

            //sqlCommand.Parameters.AddWithValue("Name", "sssss");
            //sqlCommand.ExecuteNonQuery();
        }

        


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new FlightsViewModel();
        }



    }


}
