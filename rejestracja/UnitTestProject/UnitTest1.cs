using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rejsetracja;

namespace UnitTestProject
{
    /// <summary>
    /// Klasa zawierająca testy dla klasy RoomChecker
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test poprawności działania matody sprawdzającej poprawność danych dla błędnych danych
        /// </summary>
        [TestMethod]
        public void TestReservationImpossible()
        {
            List<Reservation> reservations = new List<Reservation>();
            Reservation res = new Reservation();
            res.customerID = 0;
            res.name = "Paweł";
            res.surname = "RAk";
            res.mobileNumber = "12341234";
            res.bookingFrom = DateTime.Parse("2020-09-09");
            res.bookingUntil = DateTime.Parse("2020-09-19");
            res.roomNumber = "20";
            reservations.Add(res);

            var result = RoomChecker.isRoomAvailable(DateTime.Parse("2020-09-09"), DateTime.Parse("2020-09-10"), reservations);
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Test poprawności działania matody sprawdzającej poprawność danych dla poprawnych danych
        /// </summary>
        [TestMethod]
        public void TestReservationPossible()
        {
            List<Reservation> reservations = new List<Reservation>();
            Reservation res = new Reservation();
            res.customerID = 0;
            res.name = "Dorota";
            res.surname = "Plot";
            res.mobileNumber = "12341234";
            res.bookingFrom = DateTime.Parse("2020-09-09");
            res.bookingUntil = DateTime.Parse("2020-09-14");
            res.roomNumber = "20";
            reservations.Add(res);
            res.customerID = 1;
            res.name = "Ewa";
            res.surname = "Las";
            res.mobileNumber = "12341234";
            res.bookingFrom = DateTime.Parse("2020-09-20");
            res.bookingUntil = DateTime.Parse("2020-09-24");
            res.roomNumber = "20";
            reservations.Add(res);

            var result = RoomChecker.isRoomAvailable(DateTime.Parse("2020-09-15"), DateTime.Parse("2020-09-19"), reservations);
            Assert.IsTrue(result);
        }
    }
}
