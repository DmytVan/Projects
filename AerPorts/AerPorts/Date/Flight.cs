using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AerPorts.Date
{
    public class Flight 
    {
        public Flight(int id, int flightNumber, string cityOfDeparture, string countryOfDeparture, string cityOfArrival,
            string countryOfArrival, string departureDate, string arrivalDate, int price, int priceVIP)
        {
            this.id = id;
            this.flightNumber = flightNumber;
            this.cityOfDeparture = cityOfDeparture;
            this.countryOfDeparture = countryOfDeparture;
            this.cityOfArrival = cityOfArrival;
            this.countryOfArrival = countryOfArrival;
            this.departureDate = departureDate;
            this.arrivalDate = arrivalDate;
            this.price = price;
            this.priceVIP = priceVIP;
        }

        public Flight(int flightNumber, string cityOfDeparture, string countryOfDeparture, string cityOfArrival,
            string countryOfArrival, string departureDate, string arrivalDate, int price, int priceVIP)
        {
            this.flightNumber = flightNumber;
            this.cityOfDeparture = cityOfDeparture;
            this.countryOfDeparture = countryOfDeparture;
            this.cityOfArrival = cityOfArrival;
            this.countryOfArrival = countryOfArrival;
            this.departureDate = departureDate;
            this.arrivalDate = arrivalDate;
            this.price = price;
            this.priceVIP = priceVIP;
        }
        public Flight() { }

        public Flight(int flightNumber, string cityOfDeparture, string countryOfDeparture, string cityOfArrival, string countryOfArrival, int price, int priceVIP, int dayD, int monthD, int yearD, int hourD, int minD, int dayA, int monthA, int yearA, int hourA, int minA)
        {
            this.flightNumber = flightNumber;
            this.cityOfDeparture = cityOfDeparture;
            this.countryOfDeparture = countryOfDeparture;
            this.cityOfArrival = cityOfArrival;
            this.countryOfArrival = countryOfArrival;
            this.price = price;
            this.priceVIP = priceVIP;
            this.dayD = dayD;
            this.monthD = monthD;
            this.yearD = yearD;
            this.hourD = hourD;
            this.minD = minD;
            this.dayA = dayA;
            this.monthA = monthA;
            this.yearA = yearA;
            this.hourA = hourA;
            this.minA = minA;
        }

        public int id { get; set; }
        public int flightNumber { get; set; }
        public string cityOfDeparture { get; set; }
        public string countryOfDeparture { get; set; }
        public string cityOfArrival { get; set; }
        public string countryOfArrival { get; set; }
        public string departureDate { get; set; }
        public string arrivalDate { get; set; }
        public int price { get; set; }
        public int priceVIP { get; set; }
        public int dayD { get; set; }
        public int monthD { get; set; }
        public int yearD { get; set; }
        public int hourD { get; set; }
        public int minD { get; set; }
        public int dayA { get; set; }
        public int monthA { get; set; }
        public int yearA { get; set; }
        public int hourA { get; set; }
        public int minA { get; set; }






    }
}
