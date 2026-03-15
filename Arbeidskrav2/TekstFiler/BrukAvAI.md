# Bruk av AI i Arbeidskrav 2:

**Prompt:**
Hva er galt med denne? d står at den skal være abstrakt public abstract decimal GetDiscount(decimal basePrice) { return basePrice; }

**Svar:**
En abstract metode kan ikke ha implementasjon.

Du har skrevet:

        public abstract decimal GetDiscount(decimal basePrice)
        {
        return basePrice;
        }

Her gjør du to ting samtidig som ikke går:

abstract → betyr ingen kode

        { return basePrice; } → er kode

C# tillater ikke begge.

Riktig måte

En abstrakt metode skal bare deklareres, ikke implementeres.

Så i Guest skal det være:

public abstract decimal GetDiscount(decimal basePrice);

Legg merke til:

ingen {}

ingen return

Hvorfor?

Fordi subklassene må implementere metoden selv.

Du får da:

RegularGuest

        public override decimal GetDiscount(decimal basePrice)
        {
        return basePrice;
        }

Ingen rabatt.

**Prompt:** Hva er m i 0.85?

**Svar:** m betyr at tallet er en decimal literal i C#.

I C# har tall uten noe suffix som standard typen double.

Så:

0.85

er egentlig:

    double

Men metoden din bruker:

    decimal basePrice

Da må også tallet du ganger med være decimal, ellers får du type-konflikt.

Derfor skriver man
    
    0.85m

m betyr:

decimal
Eksempel

Dette gir feil:

    decimal price = basePrice * 0.85;

Feil fordi:

    decimal * double

ikke er lov.

Dette fungerer:

    decimal price = basePrice * 0.85m;

fordi det blir:

    decimal * decimal

**Prompt:** Feilsøk:         
    
    var showCardNumber = cardNumber.Substring(Length - 4, 4);

**Svar:**
Problemet er denne delen:

Length - 4

Length alene finnes ikke her. Length må tilhøre en string. Du må derfor bruke cardNumber.Length.

Riktig kode

    var showCardNumber = cardNumber.Substring(cardNumber.Length - 4, 4);

**Prompt:** I en feilsøk av VippsPayment: 

**Svar:**
GetPaymentInfo() bør ikke skrive til konsollen

Metoden i interfacet skal returnere en beskrivelse, ikke skrive den ut.
Oppgaven sier at den skal returnere info om betalingsmetoden.

Arbeidskrav2_HotellSystem

Du har nå:

    Console.WriteLine($"Customer paid with Vipps: {PhoneNumber}");
    return PhoneNumber;

Dette blander to ting:

logging (Console.WriteLine)

informasjon (return)

Bedre:

    public string GetPaymentInfo()
    {
    return $"Vipps {PhoneNumber}";
    }

Hvis du vil skrive til konsollen kan det gjøres et annet sted i programmet.

**Prompt:** I samme feilsøk av VippsPayment:

**Svar:**
Liten forbedring (unngå null-crash)

Hvis value er null kan Regex.IsMatch krasje. En trygg variant er:

    if (value == null || !Regex.IsMatch(value, pattern))
    throw new ArgumentException("Invalid phone number. Please insert in this format ## ## ## ##");


**Prompt:** Sitter litt fast med CalculatePrice() metoden, får den liksom ikke helt riktig, kan du skrive pseudokode uten kode, men med logikk så jeg får litt tips?


**Svar:**

Her er pseudokoden for logikken, uten C#-syntaks:

METHOD CalculateTotalPrice

    IF CheckOutDate <= CheckInDate
        THROW error "Invalid dates"

    nights = difference between CheckOutDate and CheckInDate (in days)

    basePrice = nights * Room.PricePerNight

    finalPrice = Guest.GetDiscount(basePrice)

    RETURN finalPrice

END METHOD

**Prompt:**
kan du feilsøke hele booking klassen min for finner ikke ut av feilen med is paid selvom det er nok der feilen er, tror jeg har mange følgefeil. 


    namespace Arbeidskrav2;
    
    public class Booking
    {
    private static int bookingCounter = 0;
    private readonly string bookingID;

    public Room room { get; }
    public Guest guest { get; }

    private DateTime checkInDate;
    private DateTime checkOutDate;

    private IPayable payable;

    public DateTime CheckInDate
    {
        get { return checkInDate; }
        protected set
        {
            
            checkInDate = value;
        }
    }

    public DateTime CheckOutDate
    {
        get { return checkOutDate; }
        protected set
        {
            if (checkOutDate <= checkInDate)
                throw new ArgumentException("Checkout date cannot be before checkin date!");
            
            checkOutDate = value;
        }
    }

    public Booking(Room room, Guest guest, DateTime checkIn, DateTime checkOut)
    {
        bookingCounter++;
        bookingID = "BK" + bookingCounter.ToString("D3");
        this.room = room;
        this.guest = guest;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }

    public bool IsPaid()
    {
        if (!(payable == null))
        {
            Console.WriteLine($"Hotel has been paid!");
            return true;
        }

        return false;
    }

    public decimal CalculateTotalPrice()
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Checkout date is before checkin date!");
        
        TimeSpan spentNights = checkOutDate.Subtract(checkInDate);

        decimal basePrice = spentNights.Days * room.PricePerNight;

        decimal finalPrice = guest.GetDiscount(basePrice);

        return finalPrice;
    }

    public void CheckIn()
    {
        if (!IsPaid())
        {
            throw new ArgumentException($"{bookingID} has not been paid. Please pay before checking in.");
        }

        if (DateTime.Today < checkInDate)
        {
            throw new ArgumentException("Cannot check in before the checkin date");
        }
        
        if (room.IsAvailable is false)
        {
            throw new InvalidOperationException($"Room {room.RoomID} is not available. Please choose another room.");
        }
        
        room.IsAvailable = false;
        Console.WriteLine($"[{bookingID}] has been checked in!");
        
    }

    public void CheckOut()
    {
        if (!room.IsAvailable)
        {
            Console.WriteLine($"This room is already available!");
        }
        
        room.IsAvailable = true;
        Console.WriteLine($"Booking [{bookingID}] has been checked out!");
    }

}

**Svar:**

Ja — her er flere konkrete feil i klassen din. Noen av dem gjør at IsPaid aldri kan fungere riktig, og noen lager følgefeil andre steder. Jeg går gjennom dem i rekkefølge så du kan fikse dem systematisk.

1. Største bug: constructor setter aldri datoene

I konstruktøren gjør du dette:

    CheckInDate = checkInDate;
    CheckOutDate = checkOutDate;

Men checkInDate og checkOutDate her er feltene dine, ikke parameterne.

Parameterne heter:

    DateTime checkIn
    DateTime checkOut

Så akkurat nå gjør koden egentlig dette:

    checkInDate = checkInDate

Altså: ingen verdi settes.

Riktig logikk:

    CheckInDate = checkIn
    CheckOutDate = checkOut

Dette er en klassisk bug i C# når felter og parametre har nesten samme navn.

2. Feil validering i CheckOutDate

Her:

    if (checkOutDate <= checkInDate)

Problemet er at du sjekker feltet, ikke den nye verdien (value).

Når en property settes må du validere value.

Riktig logikk:

    if (value <= checkInDate)
3. Betalingen din blir aldri satt

Du har:

private IPayable payable;

Men du setter den aldri.

Dermed vil denne alltid være null.

Derfor vil:

    IsPaid()

alltid returnere false.

I oppgaven står det også at booking skal ha:

    PaymentMethod (IPayable)
    IsPaid (bool)

Så egentlig skal flyten være:

Booking opprettes

PaymentMethod settes

ProcessPayment() kjøres

    IsPaid = true

Akkurat nå mangler hele dette steget.

4. IsPaid()-metoden er feil design

Du gjør:

    if (!(payable == null))

Dette betyr egentlig bare:

hvis betalingsmetode finnes

Men det betyr ikke at betaling er gjennomført.

Oppgaven sier også eksplisitt:

IsPaid (bool) – settes etter vellykket betaling

Arbeidskrav2_HotellSystem

Så IsPaid burde være en bool property, ikke en metode.

Eksempel på riktig logikk:

bool isPaid

og når betaling skjer:

    isPaid = payment.ProcessPayment(amount)
5. Bug i CheckOut()

Her:

    if (!room.IsAvailable)
    {
    Console.WriteLine($"This room is already available!");
    }

Dette er bakvendt.

!room.IsAvailable betyr:

rommet er opptatt

Men du skriver:

rommet er allerede ledig

Så logikken er motsatt.

6. CheckOut burde også sjekke dato

Akkurat nå kan man checke ut når som helst.

Bedre logikk:

    if (DateTime.Today < checkOutDate)
    throw error
7. Unødvendig using

Denne kan fjernes:

    using System.Reflection.Metadata.Ecma335;

Den brukes ikke.

8. CalculateTotalPrice er egentlig ganske bra

Denne delen er faktisk riktig:

netter * pris
→ send til Guest.GetDiscount()
→ returner

Det er helt i tråd med oppgaven.

9. Designproblem: CheckIn burde også hindre dobbel innsjekk

Akkurat nå kan man checke inn flere ganger.

Du burde også sjekke:

    if (!room.IsAvailable)
    throw

før du setter:

    room.IsAvailable = false

10. PaymentMethod lagt inn riktig 

        private void ProcessPayment()
        {
        decimal amount = CalculateTotalPrice();

        if (paymentMethod.ProcessPayment(amount))
        {
            IsPaid = true;
            Console.WriteLine($"Payment successful using {paymentMethod.GetPaymentInfo()}");
        }
        else
        {
            throw new Exception("Payment failed!");
        }
    }

**Prompt:** Kan du hjelpe meg med selve betalingsdelen i CreateBooking? Dette er det jeg har hittil og jeg har ikke peiling
på neste steg: decimal PriceForStay = Booking.CalculateTotalPrice();,


**Svar:**Logikken i kodeform (den lille delen du mangler)

Dette er vanlig måte å gjøre det på:
    
    decimal priceForStay = booking.CalculateTotalPrice();
    
    bool paymentSuccess = payment.ProcessPayment(priceForStay);
    
    if (!paymentSuccess)
    {
    throw new Exception("Payment failed");
    }

**Prompt:**

hvorfor er det feil o guest her public Booking? CancelBooking(string bookingID)

    {
        var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

        var booking = BookingHistoryRegister.FirstOrDefault(b => b.BookingID == bookingID);

        if (booking == null)
        {
            Console.WriteLine($"Booking [{bookingID}] does not exist.");
            return null;
        }
        
        guest.ActiveBookings.Remove(booking);
        BookingHistoryRegister.Remove(booking);
        
        Console.WriteLine($"Booking [{bookingID}] has been cancelled.");
        return booking;
        
    }

**Svar:**

Feilen med guest her skyldes at du bruker en variabel som ikke finnes i metoden.

Se på denne linjen:

    var guest = GuestRegister.FirstOrDefault(g => g.GuestID == guestID);

Problemet er guestID.

I metodesignaturen din har du bare:

    CancelBooking(string bookingID)

Det betyr at metoden ikke får noe guestID som parameter. Derfor vet ikke kompilatoren hva guestID er.

Hvorfor du egentlig ikke trenger å finne gjesten her

Du har allerede funnet:

    var booking = BookingHistoryRegister.FirstOrDefault(b => b.BookingID == bookingID);

Og i Booking har du en referanse til gjesten:

booking.Guest

Booking-klassen inneholder jo:

rom

gjest

datoer

Så du kan hente gjesten direkte fra bookingen.

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**

**Prompt:**

**Svar:**







