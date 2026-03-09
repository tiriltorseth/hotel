namespace Arbeidskrav2;

public class VipGuest : Guest
{
    public override int MaxBookings => 10;

    private int loyaltyPoints;

    public int LoyaltyPoints
    {
        get { return loyaltyPoints; }
        private set { loyaltyPoints = value; }
    }

    public VipGuest(string Name, string Email,  int LoyaltyPoints)
        : base(Name, Email)
    {
        loyaltyPoints = LoyaltyPoints;
    }

    public override decimal GetDiscount(decimal basePrice)
    {
        return basePrice * 0.85m;
    }

}