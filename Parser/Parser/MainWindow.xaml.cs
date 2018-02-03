using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Parser
{
    public partial class MainWindow : Window
    {
        List<string> Country = new List<string>();

        ParserWorker worker = new ParserWorker();
        public MainWindow()
        {
            InitializeComponent();
            ListBox1.ItemsSource = IParse.product;
            ComboBox1.ItemsSource = IParse.catigoriesName;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            ParserWorker.count++;
            worker.Start(TextBox1.Text, ComboBox1, Label1);
            ComboBox1.Items.Refresh();
            ComboBox1.ItemsSource = IParse.catigoriesName;
            Label1.Content = IParse.result;
        }


        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (ComboBox1.SelectedIndex >= 0)
            {
                ParserWorker.count++;
                worker.Start(ComboBox1.SelectedIndex, ComboBox1, Label1);
            }
            

        }

        void OnComboboxTextChanged(object sender, RoutedEventArgs e)
        {
            ComboBox1.IsDropDownOpen = true;
            // убрать selection, если dropdown только открылся
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(ComboBox1.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(ComboBox1.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

    }

}
