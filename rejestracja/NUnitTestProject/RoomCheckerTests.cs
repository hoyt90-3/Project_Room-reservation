using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using rejsetracja;


namespace NUnitTestProject
{
    public class RoomCheckerTests
    {

        [Test]
        public void TestReservationImpossible()
        {
            List<Reservation> reservations = new List<Reservation>();
            Reservation res = new Reservation();
            res.customerID = 0;
            res.name = "asdfadf";
            res.surname = "asdfasd";
            res.mobileNumber = "12341234";
            res.bookingFrom = DateTime.Parse("2020-09-09");
            res.bookingUntil = DateTime.Parse("2020-09-19");
            res.roomNumber = "20";
            reservations.Add(res);

            var result = RoomChecker.isRoomAvailable(DateTime.Parse("2020-09-09"), DateTime.Parse("2020-09-10"), reservations);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestReservationPossible()
        {
            List<Reservation> reservations = new List<Reservation>();
            Reservation res = new Reservation();
            res.customerID = 0;
            res.name = "asdfadf";
            res.surname = "asdfasd";
            res.mobileNumber = "12341234";
            res.bookingFrom = DateTime.Parse("2020-09-09");
            res.bookingUntil = DateTime.Parse("2020-09-14");
            res.roomNumber = "20";
            reservations.Add(res);
            res.customerID = 1;
            res.name = "asdfadf";
            res.surname = "asdfasd";
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
