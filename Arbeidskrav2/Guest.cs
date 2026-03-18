using System.Text.RegularExpressions;

namespace Arbeidskrav2;

public abstract class Guest
{
    private static int guestCounter = 0;
    private readonly string guestID;
    
    public string GuestID{ get { return guestID; } }

    private string name;
    private string email;

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
    
    public abstract int MaxBookings { get; }
    
    public List<Booking> ActiveBookings { get; }

    protected Guest(string name, string email)
    {
        guestCounter++;
        guestID = "G" + guestCounter.ToString("D3");
        this.Name = name;
        this.Email = email;
        ActiveBookings = new List<Booking>(); 
    }
    

    public abstract decimal GetDiscount(decimal basePrice);
    
    public abstract bool CanBook();

}