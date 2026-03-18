namespace Arbeidskrav2;

public class Hotel
{
    private string hotelName;
    
    public List<Room> RoomRegister { get; private set; } = new List<Room>();
    public List<Guest> GuestRegister { get; private set; } = new List<Guest>();
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
    /// Registrerer ny gjest
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
    
        Console.WriteLine($"Bookings for Guest {guest.Name} [{guest.GuestID}]:");
        Console.WriteLine("--------------------------------------------------------");
        
        foreach (var booking in guest.ActiveBookings)
        {
            Console.WriteLine($"\nBooking ID: {booking.BookingID}" +
                              $"\nRoom ID: {booking.room.RoomID}" +
                              $"\nRoom type: {booking.room.RoomType}" +
                              $"\nCheck in: {booking.CheckInDate:dd.mm.yyyy}" +
                              $"\nCheck out: {booking.CheckOutDate:dd.mm.yyyy}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()}" +
                              $"\n");
            
        }
    }

}