namespace Arbeidskrav2;

public class Booking
{
    private static int bookingCounter = 0;
    private readonly string bookingID;

    /// <summary>
    /// Rom objekt som tar inn rom
    /// </summary>
    public Room room { get; }
    
    /// <summary>
    /// Gjest objekt som tar inn gjest
    /// </summary>
    public Guest guest { get; }
    
    /// <summary>
    /// string som oppretter bookingID og henter data fra privat felt
    /// </summary>
    public string BookingID => bookingID;

    private DateTime checkInDate;
    private DateTime checkOutDate;

    private IPayable paymentMethod;

    /// <summary>
    /// Dato objekt for innsjekk
    /// </summary>
    public DateTime CheckInDate
    {
        get { return checkInDate; }
        protected set
        {
            checkInDate = value;
        }
    }

    /// <summary>
    /// Dato objekt for utsjekk
    /// </summary>
    public DateTime CheckOutDate
    {
        get { return checkOutDate; }
        protected set
        {
            if (value <= checkInDate)
                Console.WriteLine("Checkout date cannot be before checkin date!");
            
            checkOutDate = value;
        }
    }

    /// <summary>
    /// Oppretter booking og tar inn rom objekt, gjeste objekt, sjekk inn objekt, sjekkut objekt og IPayable betalingsmetode
    /// Sjekker at objektene ikke er null
    /// Setter BookingId til å autogenereres
    /// Kaller på processpayment for å kjøre simulert betaling
    /// </summary>
    public Booking(Room room, Guest guest, DateTime checkInDate, DateTime checkOutDate, IPayable payment)
    {
        if (room == null) throw new ArgumentNullException(nameof(room));
        if (guest == null) throw new ArgumentNullException(nameof(guest));
        if (payment == null) throw new ArgumentNullException(nameof(payment));

        bookingCounter++;
        bookingID = "BK" + bookingCounter.ToString("D3");
        this.room = room;
        this.guest = guest;
        
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        
        paymentMethod = payment;
        ProcessPayment();
    }

    
    private void ProcessPayment()
    {
        decimal amount = CalculateTotalPrice();

        if (paymentMethod.ProcessPayment(amount))
        {
            IsPaid = true;
        }
        else
        {
            Console.WriteLine("Payment failed!");
        }
    }
    
    /// <summary>
    /// Bool som sjekker om bookingen er betalt 
    /// </summary>
    public bool IsPaid { get; private set; } = false;
    
    /// <summary>
    /// Regner ut sum av netter og eventuell rabatt ved VipGjest
    /// Finner først antall dager og regner deretter ut prisen
    /// returnerer endelig pris
    /// </summary>
    public decimal CalculateTotalPrice()
    {
        if (CheckOutDate <= CheckInDate)
            Console.WriteLine("Checkout date is before checkin date!");
        
        TimeSpan spentNights = CheckOutDate.Subtract(CheckInDate);

        if (spentNights.Days <= 0)
            Console.WriteLine("Booking must be atleast one nights.");
        
        decimal basePrice = spentNights.Days * room.PricePerNight;

        decimal finalPrice = guest.GetDiscount(basePrice);

        return finalPrice;
    }

    /// <summary>
    /// Sjekker inn bookingen
    /// Sjekker om gjest har betalt og at rommet er ledig, før rommet blir satt til opptatt
    /// </summary>
    public void CheckIn()
    {
        
        if (!IsPaid)
        {
            Console.WriteLine($"{bookingID} has not been paid. Please pay before checking in.");
        }
        
        if (room.IsAvailable is false)
        {
            Console.WriteLine($"Room {room.RoomID} is not available. Please choose another room.");
        }
        
        room.IsAvailable = false;
        Console.WriteLine($"Booking [{bookingID}] has been checked in!");
    }
    
    /// <summary>
    /// Sjekker ut en gjest etter et opphold
    /// Sjekker først om rommet er ledig før rommet blir satt til ledig og gjesten er sjekket ut
    /// </summary>
    public void CheckOut()
    {
        if (room.IsAvailable)
        {
            Console.WriteLine($"This room is already available. Cannot check out!");
            return;
        }
        
        room.IsAvailable = true;
        Console.WriteLine($"Booking [{bookingID}] has been checked out and the room is now available.");
    }

}