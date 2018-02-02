using AerPorts.Date;
using AerPorts.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AerPorts.VievModel
{
    class FlightsViewModel : DependencyObject
    {

        List<Flight> flightDell = new List<Flight>();

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(FlightsViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as FlightsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterFlight;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(ICollectionView), typeof(FlightsViewModel), new PropertyMetadata(null));

        public FlightsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DBConnection.Select());
            Items.Filter = FilterFlight;
            SelectFlight = null;
        }

        private bool FilterFlight (object obj)
        {
            bool result = true;
            Flight current = obj as Flight;
            if (!string.IsNullOrWhiteSpace(FilterText) && current != null && !current.cityOfArrival.ToLower().Contains(FilterText.ToLower()) && !current.departureDate.Contains(FilterText))
                result = false;
            return result;
        }



        public Flight SelectFlight
        {
            get { return (Flight)GetValue(SelectFlightProperty); }
            set { SetValue(SelectFlightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectFlight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectFlightProperty =
            DependencyProperty.Register("SelectFlight", typeof(Flight), typeof(FlightsViewModel), new PropertyMetadata(null));

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {

                        Flight phone = new Flight(4, "","","ww", "4", "","",5,7);
                        DBConnection.flights.Add(phone);
                    }));
            }
        }

        private RelayCommand restore;
        public RelayCommand Restore
        {
            get
            {
                return restore ??
                    (restore = new RelayCommand(obj =>
                    {
                        if (flightDell.Count > 0)
                        {
                            Adress = "";
                            Split(flightDell[flightDell.Count - 1]);
                            DBConnection.Insert(flightDell[flightDell.Count - 1]);
                            flightDell.RemoveAt(flightDell.Count - 1); 
                            DBConnection.Select();
                        }
                        else
                        {
                            Adress = "Восстановление записей невозможно!";
                        }
                    }));
            }
        }


        MessageBoxButton buttons = MessageBoxButton.YesNo;
        public static AddView myWindow;
        public static Edit myWindowEd;

        private RelayCommand dellCommand;
        public RelayCommand DellCommand
        {
            get
            {
                return dellCommand ??
                    (dellCommand = new RelayCommand(obj =>
                    {
                        Flight flight = obj as Flight;
                        Adress = "";
                        if (flight != null)
                        {
                            var res = MessageBox.Show("Удалить запись?", "",buttons,
            MessageBoxImage.Question);
                            if (res == MessageBoxResult.Yes)
                            {
                                flightDell.Add(flight);
                                DBConnection.Delete(flight);
                                DBConnection.Select();
                            }
                        }
                        else
                        {
                            Adress = "Выберите запись для удаления";
                        }
                    },
                    (obj) => DBConnection.flights.Count > 0));
            }
        }

        public static Flight selectFlight;


        private RelayCommand opnW;
        public RelayCommand OpnW
        {
            get
            {
                return opnW ??
                    (opnW = new RelayCommand(obj =>
                    {
                        myWindow = new AddView(); 
                        myWindow.ShowDialog();                 
                        
                    }));
            }
        }

        private RelayCommand edWind;
        public RelayCommand EdWind
        {
            get
            {
                return edWind ??
                    (edWind = new RelayCommand(obj =>
                    {
                        Flight flight = obj as Flight;
                        Adress = "";
                        if (flight != null)
                        {
                            Split(flight);
                            selectFlight = flight;
                            myWindowEd = new Edit();
                            myWindowEd.ShowDialog();
                        }
                        else
                        {
                            Adress = "Выберите запись для редактирования";
                        }
                    }));
            }
        }



        public string Adress
        {
            get { return (string)GetValue(AdressProperty); }
            set { SetValue(AdressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Adress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdressProperty =
            DependencyProperty.Register("Adress", typeof(string), typeof(FlightsViewModel), new PropertyMetadata(""));


        string[] line = new string[6];
        public void Split(Flight flight)
        {
            line = flight.departureDate.Split(' ', '.', ':');
            flight.dayD = Convert.ToInt32(line[0]);
            flight.monthD = Convert.ToInt32(line[1]);
            flight.yearD = Convert.ToInt32(line[2]);
            flight.hourD = Convert.ToInt32(line[3]);
            flight.minD = Convert.ToInt32(line[4]);
            line = flight.arrivalDate.Split(' ', '.', ':');
            flight.dayA = Convert.ToInt32(line[0]);
            flight.monthA = Convert.ToInt32(line[1]);
            flight.yearA = Convert.ToInt32(line[2]);
            flight.hourA = Convert.ToInt32(line[3]);
            flight.minA = Convert.ToInt32(line[4]);
        }

    }
}
