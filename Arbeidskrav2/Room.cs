using System.Text.RegularExpressions;

namespace Arbeidskrav2;

public abstract class Room
{
    private readonly string roomID;
    
    private string roomType;
    private decimal pricePerNight;
    private bool isAvailable;
    private int maxGuests;
    
    public string RoomID
    {
        get { return roomID; }
        private set
        {
            var pattern = @"^\d{3}$";
            if (value == null || !(Regex.IsMatch(value, pattern)))
                throw new ArgumentException("Room ID cannot be null");
        }
    }

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


    protected Room(string roomID, string RoomType, decimal PricePerNight, int MaxGuests)
    {
       //Fikse på romnummer identifikator
        this.RoomID = roomID;
        this.RoomType = RoomType;
        this.PricePerNight = PricePerNight;
        IsAvailable = true;
        this.MaxGuests = MaxGuests;
    }

    public abstract void DisplayRoomInfo();
    
}