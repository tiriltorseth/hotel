using System.Reflection.Metadata.Ecma335;

namespace Arbeidskrav2;

public class Booking
{
    private static int bookingCounter = 0;
    private readonly string bookingID;

    public Room room { get; }
    public Guest guest { get; }

    private DateTime checkIn;
    private DateTime checkOut;

    private IPayable payable;

    public DateTime CheckIn
    {
        get { return checkIn; }
        protected set
        {
            if (DateTime.Today <= checkIn)
                throw new ArgumentException("Checkin cannot be before checkin date!");
            checkIn = value;
        }
    }

    public DateTime CheckOut
    {
        get { return checkOut; }
        protected set
        {
            if (checkOut <= checkIn)
                throw new ArgumentException("Checkout date cannot be before checkin date!");
            
            checkOut = value;
        }
    }

    public Booking(Room room, Guest guest, DateTime checkIn, DateTime checkOut)
    {
        bookingCounter++;
        bookingID = "BK" + bookingCounter.ToString("D3");
        this.room = room;
        this.guest = guest;
        this.checkIn = checkIn;
        this.checkOut = checkOut;
    }

    public bool IsPaid()
    {
        if (!(payable == null))
        {
            Console.WriteLine($"Hotel has been paid!");
            return true;
        }

        return false;
    }

    public decimal CalculateTotalPrice()
    {
        if (checkOut <= checkIn)
            throw new ArgumentException("Checkout date is before checkin date!");
        
        TimeSpan spentNights = checkOut.Subtract(checkIn);

        decimal basePrice = spentNights.Days * room.PricePerNight;

        decimal finalPrice = guest.GetDiscount(basePrice);

        return finalPrice;
    }

    public void CheckIn()
    {
        if (IsPaid() is false)
        {
            throw new ArgumentException($"{bookingID} has not been paid. Please pay before checking in.");
        }

        if (room.IsAvailable is false)
        {
            throw new InvalidOperationException($"Room {room.RoomID} is not available. Please choose another room.");
        }
        
        // Sjekk om sjekkinndato er samme dag eller etter satt booking
        
        

        room.IsAvailable = false;
        
        

        /*
        
           Marker rommet som opptatt

           Når innsjekk skjer, settes rommets tilgjengelighet til ikke tilgjengelig.

           Logg innsjekken

           Skriv en melding i konsollen eller loggen om at gjesten har sjekket inn på rommet.
         */
    }

    public CheckOut()
    {
        
    }

}