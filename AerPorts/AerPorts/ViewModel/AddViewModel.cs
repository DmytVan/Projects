using AerPorts.Date;
using AerPorts.VievModel;
using AerPorts.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AerPorts.ViewModel
{
    
    public class AddViewModel : DependencyObject
    {
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {

                        
                        if (flight != null && Validation()) {

                            
                            FlightsViewModel.myWindow.Close();
                            //flight = new Flight(4, "", "", "ww", "4", "", "", 5, 7);
                           // DBConnection.flights.Add(flight);
                            DBConnection.Insert(flight);
                            DBConnection.Select();
                        }
                    }));
            }
        }


        public AddViewModel()
        {
            flight = new Flight();
        }


        public Flight flight
        {
            get { return (Flight)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(Flight), typeof(AddViewModel), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string error
        {
            get { return (string)GetValue(errorProperty); }
            set { SetValue(errorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Adress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty errorProperty =
            DependencyProperty.Register("error", typeof(string), typeof(AddViewModel), new PropertyMetadata(""));

        MessageBoxButton buttons = MessageBoxButton.YesNo;
        DateTime D, A;

        public bool Validation()
        {
            error = "";
            if (!(flight.minA < 60) || !(flight.minA >= 0) || !(flight.minD < 60) || !(flight.minD >= 0))
            {

                error = "Минуты в пределах от 0 до 60";
                return false;
            }
            if (!(flight.hourA < 24) || !(flight.hourA >= 0) || !(flight.hourD < 24) || !(flight.hourD >= 0))
            {

                error = "Часы в пределах от 0 до 24";
                return false;
            }

            if (!(flight.dayA < 32) || !(flight.dayA >= 1) || !(flight.dayD < 32) || !(flight.dayD >= 1))
            {
                error = "Дни в пределах от 1 до 31";
                return false;
            }
            if (!(flight.monthA < 13) || !(flight.monthA >= 1) || !(flight.monthD < 13) || !(flight.monthD >= 1))
            {
                error = "Месяц в пределах от 1 до 12";
                return false;
            }
            if ((flight.monthA == 6|| flight.monthA == 4 || flight.monthA == 9 || flight.monthA == 11) && flight.dayA >30)
            {
                error = "Нет такой даты";
                return false;
            }
            if (flight.monthA == 2 && flight.dayA > 29)
            {
                error = "Нет такой даты";
                return false;
            }
            if ((flight.monthD == 6 || flight.monthD == 4 || flight.monthD == 9 || flight.monthD == 11) && flight.dayD > 30)
            {
                error = "Нет такой даты";
                return false;
            }
            if (flight.monthD == 2 && flight.dayD > 29)
            {
                error = "Нет такой даты";
                return false;
            }
            if (flight.flightNumber<0)
            {
                error = "Номер рейса должен быть не отрицательный";
                return false;
            }
            if (flight.cityOfArrival == null || flight.countryOfArrival == null || flight.cityOfDeparture == null || flight.countryOfDeparture == null)
            {
                error = "Заполните все поля";
                return false;
            }
            if (flight.price < 0 || flight.priceVIP < 0)
            {
                error = "Цена дольжна быть неотрицательной";
                return false;
            }
            if (flight.price > flight.priceVIP)
            {
                var res = MessageBox.Show("Цена эконом-класса выше бизнес-класса.\nВсё равно добавить?", "", buttons,
            MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                {
                    return false;
                }
            }
            D = new DateTime(flight.yearD, flight.monthD, flight.dayD, flight.hourD, flight.minD, 0);
            A = new DateTime(flight.yearA, flight.monthA, flight.dayA, flight.hourA, flight.minA, 0);
            if (A < D)
            {
                error = "Дата прибывания наступает раньше даты отправлнеия";
                return false;
            }
            if ((A - D).Days > 30)
            {
                var res = MessageBox.Show("Полёт составит " + (A - D).Days + " дней. Всё равно добавить?", "", buttons,
            MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
