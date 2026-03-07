namespace Arbeidskrav2;

public abstract class Room
{
    private static int roomCounter = 0;
    private readonly string roomID;
    
    public string RoomID => roomID;

    private string roomType;
    private decimal pricePerNight;
    private bool isAvailable;
    private int maxGuests;

    public string RoomType
    {
        get { return roomType; }
        protected set { roomType = value; }
    }

    public decimal PricePerNight
    {
        get { return pricePerNight; }
        protected set
        {
            if (value <= 0)
                throw new ArgumentException("Price per night must be greater than zero");
            pricePerNight = value;
        }
    }
    
    public bool IsAvailable
    {
        get { return isAvailable; }
        protected set { isAvailable = value; }
    }

    
    public int MaxGuests
    {
        get { return maxGuests; }
        protected set { maxGuests = value; }
    }


    protected Room(string RoomType, decimal PricePerNight, int MaxGuests)
    {
        roomCounter++;
        roomID = "R" +  roomCounter.ToString("D3");
        this.RoomType = RoomType;
        this.PricePerNight = PricePerNight;
        IsAvailable = true;
        this.MaxGuests = MaxGuests;
    }

    public abstract void DisplayRoomInfo();
    
}