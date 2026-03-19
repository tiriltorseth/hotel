namespace Arbeidskrav2;

/// <summary>
/// Klasse for hotel objekt
/// </summary>
public class Hotel
{
    private string hotelName;
    
    /// <summary>
    /// Propety for liste RoomRegister og oppretter nytt objekt i property
    /// Privat ettersom ingen andre enn hotelklassen skal ha tilgang til listene
    /// </summary>
    public List<Room> RoomRegister { get; private set; } = new List<Room>();
    
    /// <summary>
    /// Propety for liste GuestRegister og oppretter nytt objekt i property
    /// Privat ettersom ingen andre enn hotelklassen skal ha tilgang til listene
    /// </summary>
    public List<Guest> GuestRegister { get; private set; } = new List<Guest>();
    
    /// <summary>
    /// Propety for liste BookingHistoryRegister og oppretter nytt objekt i property
    /// Privat ettersom ingen andre enn hotelklassen skal ha tilgang til listene
    /// </summary>
    public List<Booking> BookingHistoryRegister { get; private set; } = new List<Booking>();

    /// <summary>
    /// Hotell navn, sjekker at det er mer enn 3 karakterer og ikke null
    /// </summary>
    public string HotelName
    {
        get { return hotelName; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || (value.Length < 3) )
                Console.WriteLine("Hotel name must be at least 3 characters long.");
            hotelName = value;
        }
    }

    /// <summary>
    /// Registrerer ny gjest, tar inn objeket gjest
    /// Sjekker at feltet ikke er null og finner gjesten i registeret
    /// Legger til gjesten i GuestRegister og skriver ut bekreftelse til terminalen
    /// Returnerer gjest
    /// </summary>
    public Guest RegisterGuest(Guest guest)
    {
        if (guest == null)
            Console.WriteLine("Guest cannot be null.");
        
        if (GuestRegister.Any(g => g.Email == guest.Email))
        {
            return null;
        }
        
        GuestRegister.Add(guest);
        Console.WriteLine($"[{guest.GuestID}] {guest.Name} has been registered.");
        return guest;
    }


    /// <summary>
    /// Henter ut tilgjengelige rom innenfor et datointervall
    /// Sjekker om det eksisterer noen bookinger innenfor intervallet.
    /// Hvis nei, skriv ut alle rom via DisplayRoomInfo
    /// Setter en teller (numAvailableRooms) for antall rom som finnes i neste sjekk
    /// Looper gjennom alle rom og antar at rommet er ledig, går deretter gjennom alle bookinger og sjekker
    /// datooverlapp. Setter isBooked til true
    /// Sjekker alle rom som ikke har isBooked til true og viser rommet, og øker teller
    /// Skriver ut null rom ledige dersom teller er mer en 0
    /// </summary>
    public void GetAvailableRooms(DateTime checkIn, DateTime checkOut)
    {
        if (BookingHistoryRegister.Count == 0)
        {
            foreach (var room in RoomRegister)
            {
                room.DisplayRoomInfo();
            }
            return;
        }
        
        int numAvailableRooms = 0;
        
        foreach (var room in RoomRegister)
        {
            bool isBooked = false;

            foreach (var booking in BookingHistoryRegister)
            {
                if (booking.room.RoomID == room.RoomID)
                {
                    // riktig overlapp-sjekk
                    if (checkIn < booking.CheckOutDate && checkOut > booking.CheckInDate)
                    {
                        isBooked = true;
                        break;
                    }
                }
            }

            if (!isBooked)
            {
                room.DisplayRoomInfo();
                numAvailableRooms++;
            }
        }

        if (numAvailableRooms == 0)
        {
            Console.WriteLine("There are no available rooms this time period.");
        }
       
    }

    /// <summary>
    /// Metode som lager booking og tar inn guestid, roomId, checkin, checkout og Ipayable
    /// Finner gjesten og sjekker om den er null og kan booke
    /// Finner rommet og sjekker om den er null og om den er tilgjengelig
    /// Setter bool og finner booking i bookinghistoryregister
    /// Oppretter ny booking
    /// Sjekker om booking er betalt før den legges til i bookinghistoryregister og activebookings
    /// Skriver ut feilmld dersom gjest ikke har betalt
    /// Returnerer booking
    /// </summary>
    public Booking? CreateBooking(string guestID, string roomID, DateTime checkIn,
        DateTime checkOut, IPayable payable)
    {
        var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

        if (guest == null)
        {
            Console.WriteLine($"Guest [{guestID}] does not exist.");
            return null;
        }

        if (!guest.CanBook())
        {
            Console.WriteLine($"Guest [{guest.Email}] can not book anymore visits");
            return null;
        }

        var room = RoomRegister.FirstOrDefault(r => r.RoomID == roomID);

        if (room == null)
        {
            Console.WriteLine($"Room {roomID} does not exist.");
            return null;
        }

        if (!room.IsAvailable)
        {
            Console.WriteLine($"Room [{roomID}] is not available");
            return null;
        }
        
        bool isRoomBooked = BookingHistoryRegister.Any(b =>
            b.room.RoomID == roomID &&
            b.CheckOutDate > checkIn &&
            b.CheckInDate < checkOut
        );
       
        if (isRoomBooked)
        {
            Console.WriteLine($"Room [{roomID}] is already booked in this period.");
            return null;
        }
        
        var booking = new Booking(room, guest, checkIn, checkOut, payable);
        
        if (booking.IsPaid)
        {
            guest.ActiveBookings.Add(booking);
            BookingHistoryRegister.Add(booking);
            
            if (guest is VipGuest vipGuest)
            {
                vipGuest.LoyaltyPoints += 10;
            }
        }
        else
        {
            Console.WriteLine("\nPayment failed. Booking is not paid.");
            
        }
        return booking;
        
    }

    /// <summary>
    /// Metode som lar bruker kansellere booking og tar inn bookingId
    /// Finner først booking og sjekker om den eksisterer
    /// Fjerner booking fra lister og setter rom som tilgjengelig i den perioden igjen
    /// Skriver ut bekreftelse og returnerer booking
    /// </summary>
    public Booking? CancelBooking(string bookingID)
    {
        var booking = BookingHistoryRegister.FirstOrDefault(b => b.BookingID == bookingID);

        if (booking == null)
        {
            Console.WriteLine($"Booking [{bookingID}] does not exist.");
            return null;
        }
        
        booking.guest.ActiveBookings.Remove(booking);
        BookingHistoryRegister.Remove(booking);
        booking.room.IsAvailable = true;

        
        Console.WriteLine($"Booking [{bookingID}] has been cancelled.");
        return booking;
        
    }

    /// <summary>
    /// Metode som henter ut alle bookinger for en gjest
    /// Finner gjesten og sjekker om den er null og om den har noen bookinger
    /// Looper gjennom ActiveBookings og skriver ut alle bookinger for funnet gjest
    /// </summary>
    public void GetGuestBookings(string guestID)
    {
        var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

        if (guest == null)
        {
            Console.WriteLine($"Guest [{guestID}] does not exist.");
            return;
        }

        if (!guest.ActiveBookings.Any())
        {
            Console.WriteLine($"Guest [{guestID}] does not have any active bookings.");
            return;
        }
    
        Console.WriteLine($"\nBookings for guest {guest.Name} [{guest.GuestID}]:");
        Console.WriteLine("--------------------------------------------------------");
        
        foreach (var booking in guest.ActiveBookings)
        {
            Console.WriteLine($"\nBooking ID: {booking.BookingID}" +
                              $"\nRoom ID: {booking.room.RoomID}" +
                              $"\nRoom type: {booking.room.RoomType}" +
                              $"\nCheck in: {booking.CheckInDate:dd.MM.yyyy}" +
                              $"\nCheck out: {booking.CheckOutDate:dd.MM.yyyy}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()}" +
                              $"\n");
            
        }
    }

}