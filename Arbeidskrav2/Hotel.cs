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
                throw new ArgumentException("Hotel name must be at least 3 characters long.");
            hotelName = value;
        }
    }

    /// <summary>
    /// Registrerer ny gjest
    /// </summary>
    public Guest RegisterGuest(Guest guest)
    {
        if (guest == null)
            throw new ArgumentException("Guest cannot be null.");
        
        if (GuestRegister.Any(g => g.Email == guest.Email))
        {
            return null;
        }
        
        GuestRegister.Add(guest);
        return guest;
    }

    
    // I denne må det legges til checkin og checkout så kun tilgjengelige rom vises innenfor en viss dato
    public void GetAvailableRooms(DateTime checkIn, DateTime checkOut)
    {
        if (BookingHistoryRegister.Count == 0)
        {
            foreach (var room in RoomRegister)
            {
                room.DisplayRoomInfo();
            }
        }
        int numAvailableRooms = 0;
        foreach (var booking in BookingHistoryRegister)
        {
            if (booking.CheckInDate >= checkIn && booking.CheckOutDate <= checkOut)
            {
                if (booking.room.IsAvailable)
                {
                    booking.room.DisplayRoomInfo();
                    numAvailableRooms++;
                }
            }
            else
            {
                if (numAvailableRooms == 0)
                {
                    Console.WriteLine($"There are no available this time period.");
                }
            }
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
            throw new ArgumentException("\nPayment failed. Booking is not paid.");
            
        }

        if (guest is RegularGuest regularGuest)
        {
            Console.WriteLine($"\nBooking {booking.BookingID} created for {guest.Name}" +
                              $"\n{room.RoomType} {room.RoomID} -- {booking.CheckInDate} to {booking.CheckOutDate}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()} kr" +
                              $"\nPayment approved with {payable.GetPaymentInfo()}");
            
        }
        else if (guest is VipGuest vipGuest)
            Console.WriteLine($"\nBooking {booking.BookingID} created for {guest.Name}" +
                              $"\n{room.RoomType} {room.RoomID} -- {booking.CheckInDate} to {booking.CheckOutDate}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()} kr" +
                              $"\nPayment approved with {payable.GetPaymentInfo()}" +
                              $"\nLoyalty Points [{vipGuest.LoyaltyPoints}]");
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
    
        Console.WriteLine($"Bookings for Guest [{guest.Name}]:");
        foreach (var booking in guest.ActiveBookings)
        {
            Console.WriteLine($"\nBooking ID: {booking.BookingID}" +
                              $"\nTotal Price: {booking.CalculateTotalPrice()}");
        }
    }

}