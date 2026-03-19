using System.Text.RegularExpressions;

namespace Arbeidskrav2;

/// <summary>
/// Abstrakt klasse for objektet gjest
/// </summary>
public abstract class Guest
{
    private static int guestCounter = 0;
    private readonly string guestID;
    
    /// <summary>
    /// Property som setter og returnerer gjesteID
    /// </summary>
    public string GuestID{ get { return guestID; } }

    private string name;
    private string email;

    /// <summary>
    /// Property for navn på gjesten
    /// Sjekker at navn ikke kan være null og lenger enn 2 karakterer
    /// Setter verdien til det private feltet name
    /// </summary>
    public string Name
    {
        get { return name; }
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                Console.WriteLine("Name can not be empty.");
            
            if (value.Length < 2)
            {
               Console.WriteLine("Name has to be more than two letters.");
            }
            name = value;
        }
    }
    
    /// <summary>
    /// Property for mailadressen til gjesten
    /// Bruker regex for å sørge for at mail har bla. @ og . i stringen
    /// Sjekker at det ikke kan være null
    /// Setter verdien til den private stringen email
    /// </summary>
    public string Email
    {
        get { return email; }
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                Console.WriteLine("Email can not be empty.");
            
            string checkEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            
            if (!(Regex.IsMatch(value, checkEmail)))
                Console.WriteLine("Invalid email!");
            
            email = value;
        }
    }
    /// <summary>
    /// Abstrakt property for max antall bookinger for hver gjest
    /// Settes av typen gjest i subklasser
    /// </summary>
    public abstract int MaxBookings { get; }
    
    /// <summary>
    /// Property for liste som lagrer alle aktive bookings for en gjest
    /// </summary>
    public List<Booking> ActiveBookings { get; }

    /// <summary>
    /// Oppretter ny gjest med navn, mail og gjeste ID
    /// ID blir autogenerert og har formatet G###
    /// Oppretter nytt listeobjekt ActiveBookings
    /// </summary>
    protected Guest(string name, string email)
    {
        guestCounter++;
        guestID = "G" + guestCounter.ToString("D3");
        this.Name = name;
        this.Email = email;
        ActiveBookings = new List<Booking>(); 
    }
    
    /// <summary>
    /// Abstrakt metode som skal regne ut eventuelle rabatter
    /// Tar inn basePrice, som er prisen per natt for et rom
    /// </summary>
    public abstract decimal GetDiscount(decimal basePrice);
    
    /// <summary>
    /// Frivillig lagt inn abstract bool som sjekker om gjesten kan booke fler rom eller ikke
    /// </summary>
    public abstract bool CanBook();

}