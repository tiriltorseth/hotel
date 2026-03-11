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

    /*
    public GetAvailableRooms(DateTime checkIn, DateTime checkOut)
    {
        //viser alle ledige rom i perioden
    }

    public CreateBooking(string guestID, string roomID, DateTime checkIn,
        DateTime checkOut, IPayable payable)
    {
        //sjekker at gjesten kan booke, at rommet er
        //ledig, oppretter booking og gjennomfører betaling
    }

    public CancelBooking(string bookingID)
    {
        // kansellerer booking og setter rommet som ledig igjen
    }

    public GetGuestBookings(string guestID)
    {
        // viser alle aktive bookinger med totalpriser
        
        // Systemet skal håndtere feil på en god måte, for eksempel hvis gjesten prøver å booke et opptatt
        // rom, eller hvis en RegularGuest allerede har 3 aktive bookinger.
    }
    */

}