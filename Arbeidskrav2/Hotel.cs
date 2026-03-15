using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Arbeidskrav2;

public class Hotel
{
    private string hotelName;

    public List<Room> RoomRegister = new List<Room>();
    public List<Guest> GuestRegister = new List<Guest>();
    public List<Booking> BookingHistoryRegister = new List<Booking>();

    /// <summary>
    /// Hotell navn, sjekker at det er mer enn 3 karakterer og ikke null
    /// </summary>
    public string HotelName
    {
        get { return hotelName; }
        private set
        {
            if (value.Length < 3 )
                throw new ArgumentException("Hotel name must be at least 3 characters long.");
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Hotelname cannot be empty.");
            hotelName = value;
        }
    }

    /// <summary>
    /// Registrerer ny gjest
    /// </summary>
    public Guest RegisterGuest(Guest guest)
    {
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
        foreach (var room in RoomRegister)
        {
            if (room.IsAvailable)
            {
                continue;
            }
            else
            {
                room.DisplayRoomInfo();
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
        }
        else
        {
            throw new ArgumentException("Payment failed. Booking is not paid.");
            return null;
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
        
        Console.WriteLine($"Booking [{bookingID}] has been cancelled.");
        return booking;
        
    }


    public GetGuestBookings(string guestID)
    {
        
    }

}