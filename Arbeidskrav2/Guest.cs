using System.Text.RegularExpressions;

namespace Arbeidskrav2;

public abstract class Guest
{
    private static int guestCounter = 0;
    private readonly string guestID;

    private string name;
    private string email;

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name can not be empty.");
            
            if (value.Length < 2)
            {
                throw new ArgumentException("Name has to be more than two letters.");
            }
            name = value;
        }
    }
    public string Email
    {
        get { return email; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email can not be empty.");
            
            string checkEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

            if (!(Regex.IsMatch(value, checkEmail) || value.Contains(".")))
            {
                Console.WriteLine("Invalid email!");
            }
        }
    }
    
    public List<Booking> ActiveBookings { get; }

    protected Guest(string Name, string Email)
    {
        guestCounter++;
        guestID = "G" + guestCounter.ToString("D3");
        this.Name = Name;
        this.Email = Email;
        ActiveBookings = new List<Booking>(); 
    }

    public abstract decimal GetDiscount(decimal basePrice);

}