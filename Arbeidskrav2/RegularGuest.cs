namespace Arbeidskrav2;

public class RegularGuest : Guest
{
    public override int MaxBookings => 3;


    public RegularGuest(string name, string email)
        : base(name, email)
    {
        
    }

    public override decimal GetDiscount(decimal basePrice)
    {
        return basePrice;
    }

    public override bool CanBook()
    {
        return ActiveBookings.Count < MaxBookings;
        
    }
    

}