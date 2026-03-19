namespace Arbeidskrav2;

/// <summary>
/// Klasse for en vanlig gjest, arver av baseklassen guest
/// </summary>
public class RegularGuest : Guest
{
    /// <summary>
    /// Kjører over abstrakt property fra gjest og setter til max 3 aktive bookinger
    /// </summary>
    public override int MaxBookings => 3;

    /// <summary>
    /// Oppretter vanlig gjest som tar inn navn og email
    /// setter automatisk gjesteID i baseklassen
    /// </summary>
    public RegularGuest(string name, string email)
        : base(name, email)
    {
        
    }

    /// <summary>
    /// Kjører over metoden fra Guest og returnerer basePrice, da vanlig gjest ikke har noen rabatter
    /// </summary>
    public override decimal GetDiscount(decimal basePrice)
    {
        return basePrice;
    }

    /// <summary>
    /// Kjører over metode fra gjest
    /// Returnerer en sjekk om antall bookings for den gjesten er mindre enn max bookings for vanlig gjest
    /// </summary>
    public override bool CanBook()
    {
        return ActiveBookings.Count < MaxBookings;
        
    }
}