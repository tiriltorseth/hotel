namespace Arbeidskrav2;

public class Booking
{
    private static int bookingCounter = 0;
    private readonly string bookingID;

    public Room room { get; }
    public Guest guest { get; }
    
    public string BookingID => bookingID;

    private DateTime checkInDate;
    private DateTime checkOutDate;

    private IPayable paymentMethod;


    public DateTime CheckInDate
    {
        get { return checkInDate; }
        protected set
        {
            checkInDate = value;
        }
    }

    public DateTime CheckOutDate
    {
        get { return checkOutDate; }
        protected set
        {
            if (value <= checkInDate)
                throw new ArgumentException("Checkout date cannot be before checkin date!");
            
            checkOutDate = value;
        }
    }

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
            throw new ArgumentException("Payment failed!");
        }
    }
    
    public bool IsPaid { get; private set; } = false;
    
    public decimal CalculateTotalPrice()
    {
        if (CheckOutDate <= CheckInDate)
            throw new ArgumentException("Checkout date is before checkin date!");
        
        TimeSpan spentNights = CheckOutDate.Subtract(CheckInDate);

        if (spentNights.Days <= 0)
            throw new ArgumentException("Booking must be atleast one nights.");
        
        decimal basePrice = spentNights.Days * room.PricePerNight;

        decimal finalPrice = guest.GetDiscount(basePrice);

        return finalPrice;
    }

    public void CheckIn()
    {
        if (!IsPaid)
        {
            throw new ArgumentException($"{bookingID} has not been paid. Please pay before checking in.");
        }
        
        
        if (room.IsAvailable is false)
        {
            throw new InvalidOperationException($"Room {room.RoomID} is not available. Please choose another room.");
        }
        
        room.IsAvailable = false;
        Console.WriteLine($"Booking [{bookingID}] has been checked in!");
    }

    public void CheckOut()
    {
        if (room.IsAvailable)
        {
            throw new InvalidOperationException($"This room is already available. Cannot check out!");
            return;
        }
        
        room.IsAvailable = true;
        Console.WriteLine($"Booking [{bookingID}] has been checked out and the room is now available.");
    }

}