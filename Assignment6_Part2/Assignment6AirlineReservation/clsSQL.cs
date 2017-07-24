﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Contains all of the sql queries for the program
    /// </summary>
    class clsSQL
    {
        #region display flight info
        /// <summary>
        /// Gets all flights from the database
        /// </summary>
        /// <returns>SQL string</returns>
        public String GetAllFlights()
        {
            String sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
            return sSQL;
        }

        /// <summary>
        /// Selects all passengers for a given flight
        /// </summary>
        /// <param name="flightID">The flights ID</param>
        /// <returns>SQL string</returns>
        public String GetAllPassengersForFlight(String flightID)
        {
            String sSQL = String.Format("SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                          "FROM Passenger, Flight_Passenger_Link FPL " +
                          "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                          "Flight_ID = {0}", flightID);
            return sSQL;
        }

        /// <summary>
        /// Selects the passenger who is in the seat on the flight selected
        /// </summary>
        /// <param name="flightID">The current flight</param>
        /// <param name="seatNum">The flight's seat number</param>
        /// <returns>SQL string</returns>
        public String GetPassengerFromSeat(String flightID, String seatNum)
        {
            String sSQL = String.Format("SELECT p.Passenger_ID, First_Name, Last_Name " +
                                        "FROM Passenger AS p " +
                                        "INNER JOIN Flight_Passenger_Link AS fpl " +
                                        "ON p.Passenger_ID = fpl.Passenger_ID " +
                                        "WHERE Flight_ID = {0} " +
                                        "AND Seat_Number = '{1}'", flightID, seatNum);
            return sSQL;
        }
        #endregion

        /// <summary>
        /// Updates the seat number for the passenger in the given flight
        /// </summary>
        /// <param name="seatNumber">The seat to be updated to</param>
        /// <param name="passengerID">The passenger's ID</param>
        /// <param name="flightID">The filght's ID</param>
        /// <returns>SQL string</returns>
        public String UpdatePassengerSeat(int seatNumber, int passengerID, String flightID)
        {
            String sSQL = String.Format("UPDATE Flight_Passenger_Link " +
                                        "SET Seat_Number = {0} " +
                                        "WHERE Passenger_ID = {1} " +
                                        "AND Flight_ID = {2}", seatNumber, passengerID, flightID);
            return sSQL;
        }

        /// <summary>
        /// Creates a passenger in the database
        /// </summary>
        /// <param name="passenger">The passenger object</param>
        /// <returns>SQL string</returns>
        public String CreatePassenger(clsPassenger passenger)
        {
            String sSQL = String.Format("INSERT INTO Passenger(First_Name, Last_Name) " +
                                        "VALUES('{0}', '{1}')", passenger.FirstName, passenger.LastName);
            return sSQL;
        }

        /// <summary>
        /// Creates a link between a passenger and a flight
        /// </summary>
        /// <param name="passenger">The passenger object</param>
        /// <param name="flight">The flight object</param>
        /// <param name="seatNumber">The seat assigned to the passenger</param>
        /// <returns>SQL string</returns>
        public String CreateFlightPassengerLink(clsFlight flight, clsPassenger passenger, int seatNumber)
        {
            String sSQL = String.Format("INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                                        "VALUES({0}, {1}, {2})", flight.FlightID, passenger.PassengerID, seatNumber);
            return sSQL;
        }

        /// <summary>
        /// Deletes the given passenger
        /// </summary>
        /// <param name="passenger">The passenger object</param>
        /// <returns>SQL string</returns>
        public String DeletePassenger(clsPassenger passenger)
        {
            String sSQL = String.Format("DELETE FROM Passenger " +
                                        "WHERE Passenger_ID = {0})", passenger.PassengerID);
            return sSQL;
        }

        /// <summary>
        /// Deletes the flight/passenger link between the two objects
        /// </summary>
        /// <param name="flight">The flight object</param>
        /// <param name="passenger">The passenger object</param>
        /// <returns></returns>
        public String DeleteFlightPassengerLink(clsFlight flight, clsPassenger passenger)
        {
            String sSQL = String.Format("DELETE FROM Flight_Passenger_Link " +
                                        "WHERE Flight_ID = {0} " +
                                        "AND Passenger_ID = {1}", flight.FlightID, passenger.PassengerID);
            return sSQL;
        }
    }
}
