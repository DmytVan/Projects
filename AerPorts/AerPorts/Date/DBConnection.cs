using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AerPorts.Date
{
    class DBConnection
    {
        public static ObservableCollection<Flight> flights = new ObservableCollection<Flight>();

        static SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Maestro\source\repos\AerPorts\AerPorts\Date\Database1.mdf;Integrated Security=True");
        static SqlCommand sqlCommand;

        public static ObservableCollection<Flight> Select()
        {
            
            SqlDataReader sqlDataReader = null;
           


            try
            {
                sqlCon.Open();
                sqlCommand = new SqlCommand("SELECT * from [Flights]", sqlCon);
                flights.Clear();
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    flights.Add(new Flight(Convert.ToInt32(sqlDataReader["Id"]), Convert.ToInt32(sqlDataReader["flightNumber"]), Convert.ToString(sqlDataReader["cityOfDeparture"]), Convert.ToString(sqlDataReader["countryOfDeparture"]),
                        Convert.ToString(sqlDataReader["cityOfArrival"]), Convert.ToString(sqlDataReader["countryOfArrival"]), Convert.ToString(sqlDataReader["departureDate"]), Convert.ToString(sqlDataReader["ArrivalDate"]),
                        Convert.ToInt32(sqlDataReader["price"]), Convert.ToInt32(sqlDataReader["priceVIP"])));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                sqlCon.Close();
            }

            return flights;
        }

        public static void Delete(Flight flight)
        {
            try
            {
                sqlCon.Open();
            sqlCommand = new SqlCommand("DELETE FROM [Flights] WHERE [Id]=" + flight.id, sqlCon);

            sqlCommand.ExecuteNonQuery();
            sqlCon.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении записи");
            }

        }
        
        public static void Insert(Flight flight)
        {
            try
            {
                sqlCon.Open();
                sqlCommand = new SqlCommand("insert into [Flights] (flightNumber, cityOfDeparture, countryOfDeparture, cityOfArrival, countryOfArrival, departureDate, ArrivalDate, price, priceVIP) values (" + flight.flightNumber + ", N'" + flight.cityOfDeparture +
                    "',N'" + flight.countryOfDeparture + "',N'" + flight.cityOfArrival + "', N'" + flight.countryOfArrival + "', '" + flight.monthD + "-" + flight.dayD + "-" + flight.yearD + " " + flight.hourD + ":" + flight.minD +
                    "','" + flight.monthA + "-" + flight.dayA + "-" + flight.yearA + " " + flight.hourA + ":" + flight.minA + "', " + flight.price + ", " + flight.priceVIP + ")", sqlCon);

                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch
            {
                     MessageBox.Show("Ошибка при добавлении записи");
            }
        }

        public static void Update(Flight flight)
        {
            try
            {
                sqlCon.Open();

            sqlCommand = new SqlCommand("UPDATE [Flights] SET [flightNumber] = " + flight.flightNumber + ", [cityOfDeparture] = N'" + flight.cityOfDeparture +
                    "', [countryOfDeparture] = N'" + flight.countryOfDeparture + "', [cityOfArrival] = N'" + flight.cityOfArrival + "', [countryOfArrival] = N'" + flight.countryOfArrival + "', [departureDate] = '" + flight.monthD + "-" + flight.dayD + "-" + flight.yearD + " " + flight.hourD + ":" + flight.minD +
                    "', [ArrivalDate] = '" + flight.monthA + "-" + flight.dayA + "-" + flight.yearA + " " + flight.hourA + ":" + flight.minA + "', [price] = " + flight.price + ", [priceVIP] = " + flight.priceVIP + " WHERE [id] =" + flight.id, sqlCon);

            sqlCommand.ExecuteNonQuery();
            sqlCon.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при редактировании записи");
            }
        }

        // insert into [Flights] (flightNumber, cityOfDeparture, countryOfDeparture, cityOfArrival, countryOfArrival, departureDate, ArrivalDate, price, priceVIP) values (1,'asfa','qwer','sd', 'weqr', '12-06-13 10:34','12-06-13 10:34', 12, 26)

        public static ObservableCollection<Flight> getFlight()
        {
            return flights;
        }
    }
}
