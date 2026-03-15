namespace Arbeidskrav2;

public class Booking
{
    private static int bookingCounter = 0;
    private readonly string bookingID;

    public Room room { get; }
    public Guest guest { get; }

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
            Console.WriteLine($"Payment successful using {paymentMethod.GetPaymentInfo()}");
        }
        else
        {
            throw new Exception("Payment failed!");
        }
    }
    
    public bool IsPaid { get; private set; } = false;
    
    public decimal CalculateTotalPrice()
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Checkout date is before checkin date!");
        
        TimeSpan spentNights = checkOutDate.Subtract(checkInDate);

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

        if (DateTime.Today < checkInDate)
        {
            throw new ArgumentException("Cannot check in before the checkin date");
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
            Console.WriteLine($"This room is already available!");
        }
        
        room.IsAvailable = true;
        Console.WriteLine($"Booking [{bookingID}] has been checked out!");
    }

}