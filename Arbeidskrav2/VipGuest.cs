namespace Arbeidskrav2;

/// <summary>
/// Klasse for vip gjester, arver fra klassen gjest
/// </summary>
public class VipGuest : Guest
{
    /// <summary>
    /// Kjører over property fra gjest og setter max bookings til 10 for vip
    /// </summary>
    public override int MaxBookings => 10;

    private int loyaltyPoints;

    /// <summary>
    /// Property for lojalitetspoeng
    /// Public set da hotel klassen må ha tilgang til den for å øke poeng ved booking
    /// </summary>
    public int LoyaltyPoints
    {
        get { return loyaltyPoints; }
        set { loyaltyPoints = value; }
    }

    /// <summary>
    /// Oppretter objektet vipgjest og tar inn navn og mail
    /// GjesteID setter automatisk av baseklassen
    /// </summary>
    public VipGuest(string name, string email)
        : base(name, email)
    {

    }
    
    /// <summary>
    /// Kjører over metoden fra guest
    /// Ganger basePrice med 0.85 for å gi 15% rabatt til vipgjester
    /// </summary>
    public override decimal GetDiscount(decimal basePrice)
    {
        return basePrice * 0.85m;
    }
    
    /// <summary>
    /// Kjører over metoden fra guest
    /// Returnerer en sjekk om antall bookings for den gjesten er mindre enn max bookings for vanlig gjest

    /// </summary>
    public override bool CanBook()
    {
        return ActiveBookings.Count < MaxBookings;
    }
}