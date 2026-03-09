namespace Arbeidskrav2;

public class RegularGuest : Guest
{
    public override int MaxBookings => 3;


    public RegularGuest(string Name, string Email)
        : base(Name, Email)
    {
        
    }

    public override decimal GetDiscount(decimal basePrice)
    {
        return basePrice;
    }

}