using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rejsetracja
{
    /// <summary>
    /// kalsa do sprawdznia daty rezerwacji pokoi
    /// </summary>
    public class RoomChecker
    {
        /// <summary>
        /// metoda sprawdzająca czy daty zarazerwonago pokoju się nie nakładają 
        /// </summary>
        /// <param name="from">zawiera date rozpoczęcia rezerwacji pokoju</param>
        /// <param name="until">zawiera date zakończenia rezerwacji pokoju</param>
        /// <param name="reservations">zawiera listę rezerwacji pokoi</param>
        /// <returns>zwracana wartość logiczna jeśli false to pokoj jest zajęty w tym terminie jeśli true to pokoj jest wolny</returns>
        static public bool isRoomAvailable(DateTime from, DateTime until, List<Reservation> reservations)
        {
            for(int i = 0; i < reservations.Count; i++)
            {
                if(isReserved(reservations[i].bookingFrom,reservations[i].bookingUntil, from, until)){
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// metoda sprawdza czy daty się nie nakaładają
        /// </summary>
        /// <param name="resStart">początkowa data dokonanej rezerwacji</param>
        /// <param name="resStop">końcowa data dokonanej rezerwacji</param>
        /// <param name="neededStart">początkowa data w której chcemy dokonać rezerwacji</param>
        /// <param name="neededStop">końcowa data w której chcemy dokonać rezerwacji</param>
        /// <returns> jeśli daty nachodzą się na siebie to zostanie zwrócona wartosć true w przeciwnym wypadku false </returns>
        static bool isReserved(DateTime resStart, DateTime resStop, DateTime neededStart, DateTime neededStop)
        {
            if (neededStop < resStart)
            {
                return false;
            }
            if (neededStart > resStop)
            {
                return false;
            }
            return true;
        }
    }
}
