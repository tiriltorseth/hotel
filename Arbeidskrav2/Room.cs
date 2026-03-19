using System.Text.RegularExpressions;

namespace Arbeidskrav2;

/// <summary>
/// Abstrakt klasse for objektet rom
/// </summary>
public abstract class Room
{
    private string roomID;
    
    private string roomType;
    private decimal pricePerNight;
    private bool isAvailable;
    private int maxGuests;
    
    /// <summary>
    /// Propery for romID
    /// Regex sørger for at id må være tre tall og ikke kan være null
    /// </summary>
    public string RoomID
    {
        get { return roomID; }
        private set
        {
            var pattern = @"^\d{3}$";
            if (value == null || !(Regex.IsMatch(value, pattern)))
                Console.WriteLine("Room ID must be a 3 digit number (e.g 101)");

            roomID = value;
        }
    }

    /// <summary>
    /// Propety for romtype
    /// </summary>
    public string RoomType
    {
        get { return roomType; }
        protected set { roomType = value; }
    }

    /// <summary>
    /// Pris per natt i en decimal
    /// Sjekker at prisen er mer enn null
    /// </summary>
    public decimal PricePerNight
    {
        get { return pricePerNight; }
        protected set
        {
            if (value <= 0)
               Console.WriteLine("Price per night must be greater than zero");
            pricePerNight = value;
        }
    }
    
    /// <summary>
    /// Bool som sjekker om rommet er tilgjengelig eller ikke
    /// </summary>
    public bool IsAvailable
    {
        get { return isAvailable; }
        set { isAvailable = value; }
    }

    
    /// <summary>
    /// Int som setter max antall gjester per rom
    /// </summary>
    public int MaxGuests
    {
        get { return maxGuests; }
        protected set { maxGuests = value; }
    }


    /// <summary>
    /// Oppretter objektet rom og tar inn roomID, romtype, pris per natt og max antall gjester
    /// Setter rommet til tilgjengelig når objektet opprettes
    /// </summary>
    protected Room(string roomID, string roomType, decimal pricePerNight, int maxGuests)
    {
        this.RoomID = roomID;
        this.RoomType = roomType;
        this.PricePerNight = pricePerNight;
        IsAvailable = true;
        this.MaxGuests = maxGuests;
    }

    /// <summary>
    /// Abstrakt metode for å skrive ut rominfo
    /// </summary>
    public abstract void DisplayRoomInfo();
    
}