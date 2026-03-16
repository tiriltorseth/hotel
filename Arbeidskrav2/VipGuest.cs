namespace Arbeidskrav2;

public class VipGuest : Guest
{
    public override int MaxBookings => 10;

    private int loyaltyPoints;

    public int LoyaltyPoints
    {
        get { return loyaltyPoints; }
        protected set { loyaltyPoints = value; }
    }

    public VipGuest(string name, string email)
        : base(name, email)
    {

    }

    public override decimal GetDiscount(decimal basePrice)
    {
        return basePrice * 0.85m;
    }
    
    public override bool CanBook()
    {
        return ActiveBookings.Count < MaxBookings;
    }

}